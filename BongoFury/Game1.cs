using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BongoFury
{
    /// <summary>
    /// List of Possible Game States
    /// </summary>
    public enum GameStates  
    {                
        MainMenu,       // entry point: options menu & game mode select
        Adventure,      // Story Mode: 1 or 2 players
        Arcade,         // Quick Play: 1 or 2 players
        DuellingBongos  // PVP Battle: 2 players
    }

    /// <summary>
    /// Possible controller configurations
    /// </summary>
    public enum ControllerMode
    {             
        OnePlayer,
        TwoPlayer, 
        PVP
    }                         

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static readonly Random RNG = new Random();
        public static GameStates currentGameState = GameStates.MainMenu;
        public static ControllerMode currentMode = ControllerMode.OnePlayer;
        //public static SpriteFont debugFont;

        private KeyboardState kbState, kbState_Old;
        private GamePadState curr_pad, old_Pad, curr_Pad2, old_Pad2;

        private Camera _camera;
        private Dictionary<string, Texture2D> _textureLibrary;
        private Dictionary<string, Texture2D> _menuTextureLibrary;
        private Texture2D singlePixTex;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _graphics.PreferredBackBufferWidth = 1920;       // 
            _graphics.PreferredBackBufferHeight = 1080;      //
            _graphics.IsFullScreen = true;                   //set screen dimensions and set full screen
            _graphics.HardwareModeSwitch = false;            //set screen dimensions and set full screen
        }

        protected override void Initialize()
        {
            _textureLibrary = new Dictionary<string, Texture2D>();
            _menuTextureLibrary = new Dictionary<string, Texture2D>();
            _camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // load general textures
            //debugFont = Content.Load<SpriteFont>("DebugFont");
            singlePixTex = Content.Load<Texture2D>("Textures/SinglePix");
            _textureLibrary.Add("BasicBlock", Content.Load<Texture2D>("Textures/Tiles/BasicBlockTex"));
            _textureLibrary.Add("HeavyBlock", Content.Load<Texture2D>("Textures/Tiles/HeavyBlockTex"));
            _textureLibrary.Add("BrickBlock", Content.Load<Texture2D>("Textures/Tiles/BrickBlockTex"));
            _textureLibrary.Add("CrackBlock", Content.Load<Texture2D>("Textures/Tiles/CrackedBrickTex"));
            _textureLibrary.Add("SpikeBlock", Content.Load<Texture2D>("Textures/Tiles/SpikeTex"));

            // load main menu textures when entering main menu state
            if (currentGameState == GameStates.MainMenu) 
            {
                _menuTextureLibrary.Add("AdventureText", Content.Load<Texture2D>("Textures/MainMenu/adventure"));
                _menuTextureLibrary.Add("AdventureEdgeText", Content.Load<Texture2D>("Textures/MainMenu/adventure-edge"));
                _menuTextureLibrary.Add("ArcadeText", Content.Load<Texture2D>("Textures/MainMenu/arcade"));
                _menuTextureLibrary.Add("ArcadeEdgeText", Content.Load<Texture2D>("Textures/MainMenu/arcade-edge"));
                _menuTextureLibrary.Add("QuitText", Content.Load<Texture2D>("Textures/MainMenu/quit"));
                _menuTextureLibrary.Add("QuitEdgeText", Content.Load<Texture2D>("Textures/MainMenu/quit-edge"));
                _menuTextureLibrary.Add("MenuBox", Content.Load<Texture2D>("Textures/MainMenu/menu"));
                _menuTextureLibrary.Add("TreeLine", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("SunBase", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("SunCore", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("SunRingsIn", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("SunRingsOut", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("SunBeams", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterB", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterF", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterG", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterN", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterO", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterR", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterU", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
                _menuTextureLibrary.Add("LetterY", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
            }

            // load specifics
            switch (currentGameState) 
            {
                case GameStates.MainMenu:

                    break;
                case GameStates.Adventure:

                    break;
                case GameStates.Arcade:

                    break;
                case GameStates.DuellingBongos:

                    break;
                default:
                    break; 

            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update camera
            _camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // world spritebatch
            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _spriteBatch.End();


            // UI Spritebatch
            _spriteBatch.Begin();
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
