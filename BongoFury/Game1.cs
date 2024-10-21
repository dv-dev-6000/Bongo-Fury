using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BongoFury
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //public static SpriteFont debugFont;

        Camera _camera;
        Dictionary<string, Texture2D> _textureLibrary;
        Texture2D singlePixTex;

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
            _textureLibrary = new Dictionary<string, Texture2D>();
            _camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //debugFont = Content.Load<SpriteFont>("DebugFont");

            singlePixTex = Content.Load<Texture2D>("Textures/SinglePix");

            _textureLibrary.Add("BasicBlock", Content.Load<Texture2D>("Textures/Tiles/BasicBlockTex"));
            _textureLibrary.Add("HeavyBlock", Content.Load<Texture2D>("Textures/Tiles/HeavyBlockTex"));
            _textureLibrary.Add("BrickBlock", Content.Load<Texture2D>("Textures/Tiles/BrickBlockTex"));
            _textureLibrary.Add("CrackBlock", Content.Load<Texture2D>("Textures/Tiles/CrackedBrickTex"));
            _textureLibrary.Add("SpikeBlock", Content.Load<Texture2D>("Textures/Tiles/SpikeTex"));
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
