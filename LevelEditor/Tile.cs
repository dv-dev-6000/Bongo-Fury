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
using System.Linq.Expressions;

namespace LevelEditor
{
    public class Tile
    {
        // Vars
        public Rectangle CollisionRect { get; set; }
        public bool Assigned { get; set; }
        public int ID { get; set; }
        public Vector2 ActualPos
        {
            get { return _actualPos; }
        }

        Rectangle _drawRect;
        Texture2D _tex;
        //Vector2 _gridPos;
        Vector2 _actualPos;
        Color _colour;

        // Constructor
        public Tile(Texture2D tex, Vector2 position)
        {
            Assigned = false; // CAN PROBABLY REMOVE (was used to toggle hide grid before ID)
            ID = -1;
            _tex = tex;
            _actualPos = position;
            _colour = Color.Red * 0.5f;

            CollisionRect = new Rectangle((int)_actualPos.X, (int)_actualPos.Y, Game1.TILE_SIZE, Game1.TILE_SIZE);
            _drawRect = new Rectangle((int)_actualPos.X + 1, (int)_actualPos.Y + 1, Game1.TILE_SIZE -2, Game1.TILE_SIZE -2);
        }

        public void Assign(Texture2D newTex, int id)
        {
            _tex = newTex;
            Assigned = true;
            ID = id;

            if (id >= 0)
            {
                _colour = Color.White;
            }
            else
            {
                _colour = Color.Red * 0.5f;
            }
        }

        public void Update() 
        {
            
        }

        public void Draw(SpriteBatch sb) 
        {
            sb.Draw(_tex,_drawRect, _colour);
        }

    }
}
