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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int TILE_SIZE = 64, GRID_X = 30, GRID_Y = 25;
        public static bool _paintMode, _hideGrid;
        public static SpriteFont debugFont;

        Camera _camera = new Camera();
        KeyboardState kbState, kbState_Old;
        MouseState currMouse, oldMouse;
        Vector2 relativeMousePos;

        Dictionary<string, Texture2D> _textureLibrary;
        Texture2D singlePixTex;
        

        UserInterface _userInterface;

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

        protected override void Initialize()
        {
            _tiles = new List<Tile>();
            _textureLibrary = new Dictionary<string, Texture2D>();
            _paintMode = false;
            _hideGrid = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("DebugFont");
            singlePixTex = Content.Load<Texture2D>("SinglePix");

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

            LoadBuildGrid(30, 25);
            _userInterface = new UserInterface(singlePixTex, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, _textureLibrary);
        }

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

        void AssignTile(bool deleteTile)
        {
            foreach (var tile in _tiles)
            {
                if (tile.CollisionRect.Contains(relativeMousePos))
                {
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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update controllers
            kbState = Keyboard.GetState();
            currMouse = Mouse.GetState();
            relativeMousePos = Vector2.Transform(new Vector2 (currMouse.Position.X, currMouse.Position.Y), Matrix.Invert(_camera.Transform));

            // update camera
            _camera.Update(kbState, kbState_Old);

            // toggles
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

            // check for mouse click
            if (currMouse.LeftButton == ButtonState.Pressed)
            {
                // check UI or Level (screen or game co-ords)
                if (_userInterface.BackgroundRect.Contains(currMouse.Position) && oldMouse.LeftButton == ButtonState.Released)
                {
                    if (!_userInterface.Update(currMouse.Position, _tiles))
                    {
                        // exit program without warning, ADD WARNING!
                        Exit();
                    }
                }
                else 
                {
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

            if (currMouse.RightButton == ButtonState.Pressed)
            {
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
