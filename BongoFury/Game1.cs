using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        OnePlayer,      // single player controls, invert bongo buttons on hit
        TwoPlayer,      // two players control one character, switch controls when hit
        PVP             // each player controls a character, single player controls x2
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
        private Dictionary<string, SoundEffect> _soundLibrary;
        private Texture2D singlePixTex;

        private StartMenu _startMenu;

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
            _soundLibrary = new Dictionary<string, SoundEffect>();
            _camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // load general textures
            //debugFont = Content.Load<SpriteFont>("DebugFont");
            //singlePixTex = Content.Load<Texture2D>("Textures/SinglePix");

            _textureLibrary.Add("BasicBlock", Content.Load<Texture2D>("Textures/Tiles/BasicBlockTex"));
            _textureLibrary.Add("HeavyBlock", Content.Load<Texture2D>("Textures/Tiles/HeavyBlockTex"));
            _textureLibrary.Add("BrickBlock", Content.Load<Texture2D>("Textures/Tiles/BrickBlockTex"));
            _textureLibrary.Add("CrackBlock", Content.Load<Texture2D>("Textures/Tiles/CrackedBrickTex"));
            _textureLibrary.Add("SpikeBlock", Content.Load<Texture2D>("Textures/Tiles/SpikeTex"));

            _menuTextureLibrary.Add("AdventureText", Content.Load<Texture2D>("Textures/MainMenu/adventure"));
            _menuTextureLibrary.Add("AdventureEdgeText", Content.Load<Texture2D>("Textures/MainMenu/adventure-edge"));
            _menuTextureLibrary.Add("ArcadeText", Content.Load<Texture2D>("Textures/MainMenu/arcade"));
            _menuTextureLibrary.Add("ArcadeEdgeText", Content.Load<Texture2D>("Textures/MainMenu/arcade-edge"));
            _menuTextureLibrary.Add("QuitText", Content.Load<Texture2D>("Textures/MainMenu/quit"));
            _menuTextureLibrary.Add("QuitEdgeText", Content.Load<Texture2D>("Textures/MainMenu/quit-edge"));
            _menuTextureLibrary.Add("MenuBox", Content.Load<Texture2D>("Textures/MainMenu/menu"));
            _menuTextureLibrary.Add("TreeLine", Content.Load<Texture2D>("Textures/MainMenu/TreeLine"));
            _menuTextureLibrary.Add("SunBase", Content.Load<Texture2D>("Textures/MainMenu/SunBase"));
            _menuTextureLibrary.Add("SunCore", Content.Load<Texture2D>("Textures/MainMenu/SunCore"));
            _menuTextureLibrary.Add("SunRingsIn", Content.Load<Texture2D>("Textures/MainMenu/SunInnerRings"));
            _menuTextureLibrary.Add("SunRingsOut", Content.Load<Texture2D>("Textures/MainMenu/SunOuterRings"));
            _menuTextureLibrary.Add("SunBeams", Content.Load<Texture2D>("Textures/MainMenu/SunBeams"));
            _menuTextureLibrary.Add("LetterB", Content.Load<Texture2D>("Textures/MainMenu/b"));
            _menuTextureLibrary.Add("LetterF", Content.Load<Texture2D>("Textures/MainMenu/f"));
            _menuTextureLibrary.Add("LetterG", Content.Load<Texture2D>("Textures/MainMenu/g"));
            _menuTextureLibrary.Add("LetterN", Content.Load<Texture2D>("Textures/MainMenu/n"));
            _menuTextureLibrary.Add("LetterO", Content.Load<Texture2D>("Textures/MainMenu/o"));
            _menuTextureLibrary.Add("LetterR", Content.Load<Texture2D>("Textures/MainMenu/r"));
            _menuTextureLibrary.Add("LetterU", Content.Load<Texture2D>("Textures/MainMenu/u"));
            _menuTextureLibrary.Add("LetterY", Content.Load<Texture2D>("Textures/MainMenu/y"));
            _menuTextureLibrary.Add("Selecta", Content.Load<Texture2D>("Textures/MainMenu/selecta"));

            // Load Sounds 
            _soundLibrary.Add("BongoHit1", Content.Load<SoundEffect>("Sounds/bongoHit-1"));
            _soundLibrary.Add("BongoHit2", Content.Load<SoundEffect>("Sounds/bongoHit-2"));
            _soundLibrary.Add("BongoHit3", Content.Load<SoundEffect>("Sounds/bongoHit-3"));
            _soundLibrary.Add("StompCrash", Content.Load<SoundEffect>("Sounds/StompCrash"));

            // load specifics
            switch (currentGameState) 
            {
                case GameStates.MainMenu:
                    _startMenu = new StartMenu(_menuTextureLibrary, _soundLibrary);
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
            
            // update controllers
            kbState = Keyboard.GetState();                  
            curr_pad = GamePad.GetState(PlayerIndex.One);  
            curr_Pad2 = GamePad.GetState(PlayerIndex.Two); 


            // update camera
            _camera.Update();
            // update menu
            _startMenu.Update(curr_pad, old_Pad);


            // update old controller variables for edge detection 
            kbState_Old = kbState;
            old_Pad = curr_pad;   
            old_Pad2 = curr_Pad2; 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);

            // world spritebatch
            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            
            _spriteBatch.End();


            // UI Spritebatch
            _spriteBatch.Begin();
            _startMenu.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
