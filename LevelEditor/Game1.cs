using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace LevelEditor
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Camera _camera = new Camera();
        KeyboardState kbState, kbState_Old;
        MouseState currMouse, oldMouse;
        Vector2 relativeMousePos;

        Texture2D basicBlockTex, heavyBlockTex, brickBlockTex, brokenBlockTex, spikeTex;
        public static SpriteFont debugFont;

        List<Tile> tiles;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1800;       // 
            _graphics.PreferredBackBufferHeight = 900;      //
            //_graphics.IsFullScreen = true;                   //set screen dimensions and set full screen
            //_graphics.HardwareModeSwitch = false;            //set screen dimensions and set full screen
        }

        protected override void Initialize()
        {
            tiles = new List<Tile>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("DebugFont");
            basicBlockTex = Content.Load<Texture2D>("BasicBlockTex");
            heavyBlockTex = Content.Load<Texture2D>("HeavyBlockTex");
            brickBlockTex = Content.Load<Texture2D>("BrickBlockTex");
            brokenBlockTex = Content.Load<Texture2D>("CrackedBrickTex");
            spikeTex = Content.Load<Texture2D>("SpikeTex");

            LoadBuildGrid(25, 25);
        }

        void LoadBuildGrid(int width, int Height)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tiles.Add(new Tile(basicBlockTex, new Vector2(j * basicBlockTex.Width, i * basicBlockTex.Height)));
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

            // check for mouse click and update tiles
            if (currMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                foreach (var tile in tiles)
                {
                    if (tile.CollisionRect.Contains(relativeMousePos))
                    {
                        tile.Assign(brickBlockTex);
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

            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            foreach (var tile in tiles)
            {
                tile.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
