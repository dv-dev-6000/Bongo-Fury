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
        public Rectangle BackgroundRect { get; }
        Texture2D _pixelTex;


        // Constructor
        public UserInterface(Texture2D pixelTex, int screenWidth, int screenHeight, Dictionary<string, Texture2D> uiTextures)
        {
            _pixelTex = pixelTex;
            BackgroundRect = new Rectangle(screenWidth - screenWidth/5, 0, screenWidth/5, screenHeight);
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_pixelTex, BackgroundRect, Color.DimGray * 0.85f);
        }
    }
}
