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
    /// <summary>
    /// class defining the collapsable user interface 
    /// </summary>
    public class UserInterface
    {
        // Vars
        private Texture2D _pixelTex, _selectedTex, _exitTex, _hideTex, _exportTex;
        private Dictionary<string, Texture2D> _textureLib;
        private List<Rectangle> tileButtons;
        private Rectangle _exitButton, _hideButton, _exportButton, _backgroundRect;
        private bool _isHidden;
        private int _screenWidth, _screenHeight;
        private DataIO _dataIO;

        // Accessors
        public int CurrID { get; set; }
        public Texture2D SelectedTex
        {
            get { return _selectedTex; }
        }
        public Rectangle BackgroundRect
        {
            get { return _backgroundRect; }
        }

        // Constructor
        public UserInterface(Texture2D pixelTex, int screenWidth, int screenHeight, Dictionary<string, Texture2D> textures)
        {
            CurrID = 0;
            _isHidden = false;
            _textureLib = textures;
            _pixelTex = pixelTex;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _dataIO = new DataIO();
            textures.TryGetValue("ExitButton", out _exitTex);
            textures.TryGetValue("HideArrows", out _hideTex);
            textures.TryGetValue("ExportButton", out _exportTex);
            textures.TryGetValue("BasicBlock", out _selectedTex);
            _backgroundRect = new Rectangle(_screenWidth - _screenWidth/5, 0, _screenWidth/5, _screenHeight);
            ButtonSetUp();
        }

        /// <summary>
        /// Set up the position and size of the buttons within the user interface
        /// </summary>
        private void ButtonSetUp()
        {
            // set up the unique buttons
            tileButtons = new List<Rectangle>();
            _exitButton = new Rectangle(BackgroundRect.Center.X - 16, BackgroundRect.Bottom - 50, 32, 32 );
            _hideButton = new Rectangle(BackgroundRect.Left, BackgroundRect.Top, 32, 32);
            _exportButton = new Rectangle(BackgroundRect.Center.X - (_exportTex.Width / 2), BackgroundRect.Bottom - (100 + _exportTex.Height), _exportTex.Width, _exportTex.Height);

            // set up the grid of tile selection buttons
            for (int i = 0; i < 4; i++) 
            {
                tileButtons.Add(new Rectangle(BackgroundRect.Left + 25, (BackgroundRect.Top + 300) + (i * 100), Game1.TILE_SIZE, Game1.TILE_SIZE));
            }
            for (int i = 0; i < 4; i++)
            {
                tileButtons.Add(new Rectangle(BackgroundRect.Left + 50 + Game1.TILE_SIZE, (BackgroundRect.Top + 300) + (i * 100), Game1.TILE_SIZE, Game1.TILE_SIZE));
            }
            for (int i = 0; i < 4; i++)
            {
                tileButtons.Add(new Rectangle(BackgroundRect.Left + 75 + (Game1.TILE_SIZE*2), (BackgroundRect.Top + 300) + (i * 100), Game1.TILE_SIZE, Game1.TILE_SIZE));
            }
        }

        /// <summary>
        /// functionality for the hide/show ui button
        /// </summary>
        private void HideButtonPressed()
        {
            // sets the is hidden flag and repositions hide/show button position 
            if (!_isHidden)
            {
                _backgroundRect.X = _screenWidth - 32;
                _isHidden = true;
            }
            else
            {
                _backgroundRect.X = _screenWidth - _screenWidth / 5;
                _isHidden = false;
            }
            _hideButton.X = BackgroundRect.Left;
        }

        /// <summary>
        /// Handle UI interactions and button clicks
        /// </summary>
        /// <param name="mousePos"></param>
        /// <param name="tList"></param>
        /// <returns></returns>
        public bool Update(Point mousePos, List<Tile> tList)
        {
            if (_hideButton.Contains(mousePos))
            {
                // show/hide the UI
                HideButtonPressed();
            }
            else if (_exitButton.Contains(mousePos))
            {
                // Exit Program
                return false;
            }

            // change the selected tile
            for (int i = 0; i < tileButtons.Count; i++)
            {
                if (tileButtons[i].Contains(mousePos))
                {
                    _selectedTex = _textureLib.Values.ElementAt(i);
                    CurrID = i;
                }
            }

            // export level when clicked
            if (_exportButton.Contains(mousePos))
            {
                _dataIO.ExportLevel_CSV(tList);
            }

            return true;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_pixelTex, BackgroundRect, Color.DimGray * 0.95f);
            sb.Draw(_hideTex, _hideButton, Color.White);

            if (!_isHidden) 
            {
                sb.Draw(_exitTex, _exitButton, Color.White);
                sb.Draw(_exportTex, _exportButton, Color.White);
                for (int i = 0; i < 12; i++)
                {
                    sb.Draw(_pixelTex, tileButtons[i], Color.LightGray * 0.7f);
                    if (SelectedTex == _textureLib.Values.ElementAt(i))
                    {
                        sb.Draw(_textureLib.Values.ElementAt(i), tileButtons[i], Color.Green);
                    }
                    else
                    {
                        sb.Draw(_textureLib.Values.ElementAt(i), tileButtons[i], Color.White);
                    }
                    
                }

                //UI Text
                sb.DrawString(Game1.debugFont, "W,A,S,D to move camera", new Vector2(BackgroundRect.Left + 25, BackgroundRect.Top + 150), Color.White);
                sb.DrawString(Game1.debugFont, "Q,E to Zoom in and out", new Vector2(BackgroundRect.Left + 25, BackgroundRect.Top + 200), Color.White);

                if (!Game1._paintMode)
                {
                    sb.DrawString(Game1.debugFont, "* '1' to Activate Paint Mode", new Vector2(BackgroundRect.Left + 25, BackgroundRect.Top + 50), Color.Orange);
                }
                else
                {
                    sb.DrawString(Game1.debugFont, "* '1' to Activate Paint Mode", new Vector2(BackgroundRect.Left + 25, BackgroundRect.Top + 50), Color.GreenYellow);
                }

                if (!Game1._hideGrid)
                {
                    sb.DrawString(Game1.debugFont, "* '2' to Deactivate Grid", new Vector2(BackgroundRect.Left + 25, BackgroundRect.Top + 100), Color.Orange);
                }
                else
                {
                    sb.DrawString(Game1.debugFont, "* '2' to Deactivate Grid", new Vector2(BackgroundRect.Left + 25, BackgroundRect.Top + 100), Color.GreenYellow);
                }

            }
            
        }
    }
}
