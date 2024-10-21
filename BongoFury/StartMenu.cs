using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BongoFury
{
    internal class StartMenu
    {
        // Class Vars
        private SunClass _sun;
        private SelectionBoxClass _selectBox;
        private List<LetterBounceClass> _letterList;
        
        // Textures
        private Texture2D _sunBase, _sunCore, _sunRingIn, _sunRingOut, _sunBeams;
        private Texture2D _b, _f, _g, _n, _o, _r, _u, _y;
        private Texture2D _arcade, _arcadeEdge, _adv, _advEdge, _quit, _quitEdge, _menuBox, _selecta;
        private Texture2D _foreground;

        public StartMenu(Dictionary<string, Texture2D> menuTextures) 
        {
            AssignTextures(menuTextures);
            FillLetterList();
            _sun = new SunClass(_sunBase, _sunCore, _sunRingIn, _sunRingOut, _sunBeams, new Vector2(1500, 400));
            _selectBox = new SelectionBoxClass(_selecta, _menuBox, _arcade, _adv, _quit, _quit, new Vector2(360, 820), false);
        }

        private void AssignTextures(Dictionary<string, Texture2D> menuTextures)
        {
            menuTextures.TryGetValue("SunBase", out _sunBase);
            menuTextures.TryGetValue("SunCore", out _sunCore);
            menuTextures.TryGetValue("SunRingsIn", out _sunRingIn);
            menuTextures.TryGetValue("SunRingsOut", out _sunRingOut);
            menuTextures.TryGetValue("SunBeams", out _sunBeams);

            menuTextures.TryGetValue("LetterB", out _b);
            menuTextures.TryGetValue("LetterF", out _f);
            menuTextures.TryGetValue("LetterG", out _g);
            menuTextures.TryGetValue("LetterN", out _n);
            menuTextures.TryGetValue("LetterO", out _o);
            menuTextures.TryGetValue("LetterR", out _r);
            menuTextures.TryGetValue("LetterU", out _u);
            menuTextures.TryGetValue("LetterY", out _y);

            menuTextures.TryGetValue("ArcadeText", out _arcade);
            menuTextures.TryGetValue("ArcadeEdgeText", out _arcadeEdge);
            menuTextures.TryGetValue("AdventureText", out _adv);
            menuTextures.TryGetValue("AdventureEdgeText", out _advEdge);
            menuTextures.TryGetValue("QuitText", out _quit);
            menuTextures.TryGetValue("QuitEdgeText", out _quitEdge);
            menuTextures.TryGetValue("MenuBox", out _menuBox);
            menuTextures.TryGetValue("Selecta", out _selecta);

            menuTextures.TryGetValue("TreeLine", out _foreground);

        }

        private void FillLetterList()
        {
            _letterList = new List<LetterBounceClass>();

            _letterList.Add(new LetterBounceClass(_b, new Vector2(100, -200), 1));
            _letterList.Add(new LetterBounceClass(_o, new Vector2(250, -410), 1));
            _letterList.Add(new LetterBounceClass(_n, new Vector2(400, -600), 1));
            _letterList.Add(new LetterBounceClass(_g, new Vector2(550, -800), 1));
            _letterList.Add(new LetterBounceClass(_o, new Vector2(700, -1025), 1));

            _letterList.Add(new LetterBounceClass(_f, new Vector2(100, -1219), 2));
            _letterList.Add(new LetterBounceClass(_u, new Vector2(250, -1410), 2));
            _letterList.Add(new LetterBounceClass(_r, new Vector2(400, -1619), 2));
            _letterList.Add(new LetterBounceClass(_y, new Vector2(550, -1800), 2));
        }

        public void Update()
        {
            _sun.UpdateMe();
            //letterList.ForEach(letter => letter.UpdateMe(bHit1, bHit2, bHit3, RNG));
            //selectBoxList.ForEach(box => box.UpdateMe(old_Pad, curr_pad, bHit1, bHit2, bHit3));
        }

        public void Draw(SpriteBatch sb)
        {
            _sun.DrawMe(sb);
            sb.Draw(_foreground, Vector2.Zero, Color.White);
            _letterList.ForEach(letter => letter.DrawMe(sb));
            _selectBox.DrawMe(sb, _arcadeEdge, _advEdge, _quitEdge, _quitEdge);

        }
    }
}
