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
    public class UserInterface
    {
        // Vars
        Rectangle _rect;
        Texture2D _tex;


        // Constructor
        public UserInterface(Texture2D tex, int screenWidth, int screenHeight)
        {
            _tex = tex;
            _rect = new Rectangle(screenWidth - screenWidth/5, 0, screenWidth/5, screenHeight);
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_tex, _rect, Color.DimGray * 0.85f);
        }
    }
}
