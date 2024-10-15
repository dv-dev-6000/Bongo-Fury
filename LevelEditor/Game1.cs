﻿using Microsoft.Xna.Framework;
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

        public static int TILE_SIZE = 64;

        Camera _camera = new Camera();
        KeyboardState kbState, kbState_Old;
        MouseState currMouse, oldMouse;
        Vector2 relativeMousePos;
        bool _paintMode;

        Dictionary<string, Texture2D> _textureLibrary;
        //Texture2D basicBlockTex, heavyBlockTex, brickBlockTex, brokenBlockTex, spikeTex;
        //Texture2D hideArrowsTex, closeButtonTex;
        Texture2D selectedTex, singlePixTex;
        public static SpriteFont debugFont;

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
            selectedTex = null;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("DebugFont");
            singlePixTex = Content.Load<Texture2D>("SinglePix");
            //basicBlockTex = Content.Load<Texture2D>("BasicBlockTex");
            //heavyBlockTex = Content.Load<Texture2D>("HeavyBlockTex");
            //brickBlockTex = Content.Load<Texture2D>("BrickBlockTex");
            //brokenBlockTex = Content.Load<Texture2D>("CrackedBrickTex");
            //spikeTex = Content.Load<Texture2D>("SpikeTex");
            //hideArrowsTex = Content.Load<Texture2D>("ArrowsTex");
            //closeButtonTex = Content.Load<Texture2D>("ExitTex");

            _textureLibrary.Add("BasicBlock", Content.Load<Texture2D>("BasicBlockTex"));
            _textureLibrary.Add("HeavyBlock", Content.Load<Texture2D>("HeavyBlockTex"));
            _textureLibrary.Add("BrickBlock", Content.Load<Texture2D>("BrickBlockTex"));
            _textureLibrary.Add("CrackBlock", Content.Load<Texture2D>("CrackedBrickTex"));
            _textureLibrary.Add("SpikeBlock", Content.Load<Texture2D>("SpikeTex"));
            _textureLibrary.Add("HideArrows", Content.Load<Texture2D>("ArrowsTex"));
            _textureLibrary.Add("ExitButton", Content.Load<Texture2D>("ExitTex"));

            LoadBuildGrid(30, 25);
            _userInterface = new UserInterface(singlePixTex, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, _textureLibrary);
            _textureLibrary.TryGetValue("BrickBlock", out selectedTex);
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

        void AssignTile()
        {
            foreach (var tile in _tiles)
            {
                if (tile.CollisionRect.Contains(relativeMousePos))
                {
                    tile.Assign(selectedTex);
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

            // check for mouse click
            if (currMouse.LeftButton == ButtonState.Pressed)
            {
                // check UI or Level (screen or game co-ords)
                if (_userInterface.BackgroundRect.Contains(currMouse.Position))
                {
                    
                }
                else 
                {
                    if (_paintMode)
                    {
                        AssignTile();
                    }
                    else if (oldMouse.LeftButton == ButtonState.Released)
                    {
                        AssignTile();
                    }
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

            foreach (var tile in _tiles)
            {
                tile.Draw(_spriteBatch);
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
