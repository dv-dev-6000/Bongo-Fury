using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BongoFury
{
    internal class Camera
    {
        public Vector2 Position { get; set; }
        public float Zoom { get; set; }
        public Matrix Transform { get; private set; }

        public Camera()
        {
            Position = Vector2.Zero;
            Zoom = 1.0f;
        }

        public void Update()
        {
            //logic to move the camera independently

            //if (kbState.IsKeyDown(Keys.W))
            //{
            //    Position = new Vector2(Position.X, Position.Y - 5);
            //}
            //if (kbState.IsKeyDown(Keys.S))
            //{
            //    Position = new Vector2(Position.X, Position.Y + 5);
            //}
            //if (kbState.IsKeyDown(Keys.A))
            //{
            //    Position = new Vector2(Position.X - 5, Position.Y);
            //}
            //if (kbState.IsKeyDown(Keys.D))
            //{
            //    Position = new Vector2(Position.X + 5, Position.Y);
            //}
            //
            //if (kbState.IsKeyDown(Keys.Down) || kbState.IsKeyDown(Keys.Q))
            //{
            //    Zoom = Zoom - 0.01f;
            //}
            //if (kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.E))
            //{
            //    Zoom = Zoom + 0.01f;
            //}

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) * Matrix.CreateScale(Zoom);
        }
    }
}
