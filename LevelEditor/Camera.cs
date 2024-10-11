using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace LevelEditor
{
    public class Camera
    {
        public Vector2 Position { get; set; }
        public float Zoom { get; set; } = 1.0f;
        public Matrix Transform { get; private set; }

        public Camera()
        {
            Position = Vector2.Zero;
        }

        public void Update(KeyboardState kbState, KeyboardState kbStateOld)
        {
            //logic to move the camera independently

            if (kbState.IsKeyDown(Keys.W))
            {
                Position = new Vector2 (Position.X, Position.Y - 5);
            }
            if (kbState.IsKeyDown(Keys.S))
            {
                Position = new Vector2(Position.X, Position.Y + 5);
            }
            if (kbState.IsKeyDown(Keys.A))
            {
                Position = new Vector2(Position.X - 5, Position.Y);
            }
            if (kbState.IsKeyDown(Keys.D))
            {
                Position = new Vector2(Position.X + 5, Position.Y);
            }

            if (kbState.IsKeyDown(Keys.Down))
            {
                Zoom = Zoom - 0.01f;
            }
            if (kbState.IsKeyDown(Keys.Up))
            {
                Zoom = Zoom + 0.01f;
            }

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) * Matrix.CreateScale(Zoom);
        }
    }
}
