using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace LevelEditor
{

    public class Game1 : Game
    {
        // Game Variables 
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int TILE_SIZE = 64, GRID_X = 30, GRID_Y = 25;
        public static bool _paintMode, _hideGrid;
        public static SpriteFont debugFont;

        // editor camera
        Camera _camera = new Camera();
        // keyboard and mouse input capture
        KeyboardState kbState, kbState_Old;
        MouseState currMouse, oldMouse;
        // used to translate mouse screen position to game position
        Vector2 relativeMousePos;

        // Data Structure to store textures alongside tags for simple retrieval 
        Dictionary<string, Texture2D> _textureLibrary;
        // single pixel texture for debugging
        Texture2D singlePixTex;
        // the collapsable UI
        UserInterface _userInterface;
        // the level data
        List<Tile> _tiles;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1920;       // 
            _graphics.PreferredBackBufferHeight = 1080;      //
            _graphics.IsFullScreen = true;                   //set screen dimensions and set full screen
            _graphics.HardwareModeSwitch = false;            //set screen dimensions and set full screen
        }

        /// <summary>
        /// initialize game variables 
        /// </summary>
        protected override void Initialize()
        {
            _tiles = new List<Tile>();
            _textureLibrary = new Dictionary<string, Texture2D>();
            _paintMode = false;
            _hideGrid = false;

            base.Initialize();
        }

        /// <summary>
        /// Load in content
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // lead debug content
            debugFont = Content.Load<SpriteFont>("DebugFont");
            singlePixTex = Content.Load<Texture2D>("SinglePix");

            // fill texture library 
            _textureLibrary.Add("BasicBlock", Content.Load<Texture2D>("BasicBlockTex"));
            _textureLibrary.Add("HeavyBlock", Content.Load<Texture2D>("HeavyBlockTex"));
            _textureLibrary.Add("BrickBlock", Content.Load<Texture2D>("BrickBlockTex"));
            _textureLibrary.Add("CrackBlock", Content.Load<Texture2D>("CrackedBrickTex"));
            _textureLibrary.Add("SpikeBlock", Content.Load<Texture2D>("SpikeTex"));
            _textureLibrary.Add("SpikeBlockUp", Content.Load<Texture2D>("SpikeTexUp"));
            _textureLibrary.Add("SpikeBlockL", Content.Load<Texture2D>("SpikeTexL"));
            _textureLibrary.Add("SpikeBlockR", Content.Load<Texture2D>("SpikeTexR"));
            _textureLibrary.Add("PlayerStart", Content.Load<Texture2D>("PlayerStartTex"));
            _textureLibrary.Add("Misc1", Content.Load<Texture2D>("Misc1Tex"));
            _textureLibrary.Add("Misc2", Content.Load<Texture2D>("Misc2Tex"));
            _textureLibrary.Add("Misc3", Content.Load<Texture2D>("Misc3Tex"));
            _textureLibrary.Add("HideArrows", Content.Load<Texture2D>("ArrowsTex"));
            _textureLibrary.Add("ExitButton", Content.Load<Texture2D>("ExitTex"));
            _textureLibrary.Add("ExportButton", Content.Load<Texture2D>("ExportTex"));

            // initiate the creation of the level grid
            LoadBuildGrid(GRID_X, GRID_Y);
            // initialize the UI
            _userInterface = new UserInterface(singlePixTex, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, _textureLibrary);
        }

        /// <summary>
        /// create a grid in which the level is  designed
        /// </summary>
        /// <param name="width"> level width in number of tiles</param>
        /// <param name="Height"> level height in number of tiles</param>
        void LoadBuildGrid(int width, int Height)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _tiles.Add(new Tile(singlePixTex, new Vector2(j * TILE_SIZE, i * TILE_SIZE)));
                }
            }
        }

        /// <summary>
        /// Assigns the type of a clicked tile
        /// </summary>
        /// <param name="deleteTile"> delete tile is true on right click or false on left click</param>
        void AssignTile(bool deleteTile)
        {
            foreach (var tile in _tiles)
            {
                if (tile.CollisionRect.Contains(relativeMousePos))
                {
                    // sets the tile to the current tile type on left click
                    // resets tile to empty on right click
                    if (!deleteTile)
                    {
                        tile.Assign(_userInterface.SelectedTex, _userInterface.CurrID);
                    }
                    else {
                        tile.Assign(singlePixTex, -1);
                    }
                }
            }
        }

        /// <summary>
        /// update method
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // exit program with esc key or select button
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update controllers
            kbState = Keyboard.GetState();
            currMouse = Mouse.GetState();
            relativeMousePos = Vector2.Transform(new Vector2 (currMouse.Position.X, currMouse.Position.Y), Matrix.Invert(_camera.Transform));

            // update camera
            _camera.Update(kbState, kbState_Old);

            // toggle paint mode
            if (kbState.IsKeyDown(Keys.D1) && kbState_Old.IsKeyUp(Keys.D1))
            {
                if (_paintMode)
                {
                    _paintMode = false;
                }
                else
                {
                    _paintMode = true;
                }
            }
            // toggle grid visibility
            if (kbState.IsKeyDown(Keys.D2) && kbState_Old.IsKeyUp(Keys.D2))
            {
                if (_hideGrid)
                {
                    _hideGrid = false;
                }
                else
                {
                    _hideGrid = true;
                }
            }

            // check for mouse left click
            if (currMouse.LeftButton == ButtonState.Pressed)
            {
                // check if click is UI or Editor
                if (_userInterface.BackgroundRect.Contains(currMouse.Position) && oldMouse.LeftButton == ButtonState.Released)
                {
                    // if UI clicked then fire UI update method
                    if (!_userInterface.Update(currMouse.Position, _tiles))
                    {
                        //  if UI update returns false then exit button was pressed (exits program without warning, ADD WARNING!)
                        Exit();
                    }
                }
                else 
                {
                    // if editor clicked check toggles and fire Assign method
                    if (_paintMode)
                    {
                        AssignTile(false);
                    }
                    else if (oldMouse.LeftButton == ButtonState.Released)
                    {
                        AssignTile(false);
                    }
                }

            }
             // check for mouse right click
            if (currMouse.RightButton == ButtonState.Pressed)
            {
                // check toggles and fire Assign method
                if (_paintMode)
                {
                    AssignTile(true);
                }
                else if (oldMouse.LeftButton == ButtonState.Released)
                {
                    AssignTile(true);
                }
            }


            // update old controller variables for edge detection 
            kbState_Old = kbState;
            oldMouse = currMouse;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // world spritebatch
            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            // draw tiles dependant on toggle
            foreach (var tile in _tiles)
            {
                if (!_hideGrid)
                {
                    tile.Draw(_spriteBatch);
                }
                else
                {
                    if (tile.ID > -1)
                    {
                        tile.Draw(_spriteBatch);
                    }
                }
            }

            _spriteBatch.End();

            // UI Spritebatch
            _spriteBatch.Begin();
            _userInterface.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
