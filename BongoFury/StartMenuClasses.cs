using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BongoFury
{
    /// <summary>
    /// Class defining the main menus spinning sun behaviour  
    /// </summary>
    public class SunClass
    {
        // class vars
        private Texture2D sunBase, sunCore, sunRingOne, sunRingTwo, sunBeams;
        private Vector2 m_pos;
        private float rotationR1, rotationR2, rotationBeam;

        public SunClass(Texture2D sBase, Texture2D sCore, Texture2D sRingOne, Texture2D sRingTwo, Texture2D sBeams, Vector2 pos)
        {
            sunBase = sBase;
            sunCore = sCore;
            sunRingOne = sRingOne;
            sunRingTwo = sRingTwo;
            sunBeams = sBeams;

            m_pos = pos;
        }

        public void UpdateMe()
        {
            rotationR1 += 0.01f;
            rotationR2 -= 0.01f;
            rotationBeam += 0.01f;
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(sunBase, m_pos, null, Color.White, 0, new Vector2(sunBase.Width / 2, sunBase.Height / 2), 1, SpriteEffects.None, 0);
            sb.Draw(sunCore, m_pos, null, Color.White, rotationR2, new Vector2(sunCore.Width / 2, sunCore.Height / 2), 1, SpriteEffects.None, 0);
            sb.Draw(sunBeams, m_pos, null, Color.White, rotationBeam, new Vector2(sunBeams.Width / 2, sunBeams.Height / 2), 1, SpriteEffects.None, 0);
            sb.Draw(sunRingOne, m_pos, null, Color.White, rotationR1, new Vector2(sunRingOne.Width / 2, sunRingOne.Height / 2), 1, SpriteEffects.None, 0);
            sb.Draw(sunRingTwo, m_pos, null, Color.White, rotationR2, new Vector2(sunRingTwo.Width / 2, sunRingTwo.Height / 2), 1, SpriteEffects.None, 0);
        }

    }

    /// <summary>
    /// Class defining the main menus bouncing letter behaviour
    /// </summary>
    public class LetterBounceClass
    {
        // class vars
        private Texture2D m_tex;
        private Rectangle floor, collisionRect;
        private Vector2 m_pos;
        private float velocityY;
        public int health;

        public LetterBounceClass(Texture2D image, Vector2 pos, int bl)
        {
            m_tex = image;
            m_pos = pos;

            health = 2;

            collisionRect = new Rectangle((int)m_pos.X, (int)m_pos.Y, m_tex.Width, m_tex.Height);

            if (bl == 1)
            {
                velocityY = 2;
                floor = new Rectangle((int)m_pos.X, 310, m_tex.Width, 1);
            }
            else
            {
                velocityY = -10;
                floor = new Rectangle((int)m_pos.X, 550, m_tex.Width, 1);
            }

        }

        public void UpdateMe(SoundEffect b1, SoundEffect b2, SoundEffect b3, Random rng)
        {
            if (health > 0)
            {
                m_pos.Y += velocityY;
                velocityY += 0.2f;
            }

            collisionRect.X = (int)m_pos.X;
            collisionRect.Y = (int)m_pos.Y;

            if (collisionRect.Intersects(floor) && health == 2)
            {
                velocityY = -7;
                health = 1;
                b1.Play(1, (float)rng.NextDouble() * (-1), 0);
            }
            else if (collisionRect.Intersects(floor) && health == 1)
            {
                velocityY = 0;
                health = 0;
                b2.Play(1, (float)rng.NextDouble() * (-1), 0);
            }

            //if (Game1.isFuryMain)
            //{
            //    m_pos.Y += velocityY;
            //    velocityY += (float)rng.NextDouble() * 2;
            //}

        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_tex, m_pos, Color.White);
        }
    }

    public class SelectionBoxClass
    {
        public enum OptionSelected
        {
            Arcade,
            Adventure,
            Tutorial,
            Quit
        }

        public OptionSelected currOption;

        private Texture2D m_backTex, m_tex1, m_tex2, m_tex3, m_tex4, m_selecta;
        private Texture2D arcEdge, advEdge, tutEdge, quitEdge;
        public Vector2 mainPos, pos1, pos2, pos3, pos4, selectaPos;
        public bool isPlayerSelect;

        public SelectionBoxClass(Texture2D cursor, Texture2D back, Texture2D option1, Texture2D option2, Vector2 pos, bool isPS)
        {
            currOption = OptionSelected.Arcade;

            m_backTex = back;
            m_tex1 = option1;
            m_tex2 = option2;

            mainPos = pos;
            isPlayerSelect = isPS;
        }
        public SelectionBoxClass(Texture2D cursor, Texture2D back, Texture2D option1, Texture2D option2, Texture2D option3, Texture2D option4, Vector2 pos, bool isPS)
        {
            currOption = OptionSelected.Arcade;

            m_backTex = back;
            m_selecta = cursor;
            m_tex1 = option1;
            m_tex2 = option2;
            m_tex3 = option3;
            m_tex4 = option4;

            mainPos = pos;
            isPlayerSelect = isPS;

            pos1 = new Vector2(mainPos.X, mainPos.Y - 150);
            pos2 = new Vector2(mainPos.X, mainPos.Y - 50);
            pos3 = new Vector2(mainPos.X, mainPos.Y + 50);
            pos4 = new Vector2(mainPos.X, mainPos.Y + 150);

            selectaPos = pos1;
        }

        public void UpdateMe(GamePadState oldPad, GamePadState currPad, SoundEffect b1, SoundEffect b2, SoundEffect b3)
        {
            if (currPad.DPad.Down == ButtonState.Pressed && oldPad.DPad.Down == ButtonState.Released || currPad.ThumbSticks.Left.Y < 0 && oldPad.ThumbSticks.Left.Y == 0)
            {
                if (currOption != OptionSelected.Quit)
                {
                    currOption++;
                    b1.Play();
                }
            }
            if (currPad.DPad.Up == ButtonState.Pressed && oldPad.DPad.Up == ButtonState.Released || currPad.ThumbSticks.Left.Y > 0 && oldPad.ThumbSticks.Left.Y == 0)
            {
                if (currOption != OptionSelected.Arcade)
                {
                    currOption--;
                    b2.Play();
                }
            }
        }

        public void DrawMe(SpriteBatch sb, Texture2D arc, Texture2D adv, Texture2D tut, Texture2D quit)
        {
            arcEdge = arc;
            advEdge = adv;
            tutEdge = tut;
            quitEdge = quit;

            if (!isPlayerSelect)
            {
                sb.Draw(m_backTex, mainPos, null, Color.White, 0, new Vector2(m_backTex.Width / 2, m_backTex.Height / 2), 1, SpriteEffects.None, 0);
                sb.Draw(m_tex1, pos1, null, Color.White, 0, new Vector2(m_tex1.Width / 2, m_tex1.Height / 2), 1, SpriteEffects.None, 0);
                sb.Draw(m_tex2, pos2, null, Color.White, 0, new Vector2(m_tex2.Width / 2, m_tex2.Height / 2), 1, SpriteEffects.None, 0);
                sb.Draw(m_tex3, pos3, null, Color.White, 0, new Vector2(m_tex3.Width / 2, m_tex3.Height / 2), 1, SpriteEffects.None, 0);
                sb.Draw(m_tex4, pos4, null, Color.White, 0, new Vector2(m_tex4.Width / 2, m_tex4.Height / 2), 1, SpriteEffects.None, 0);
                sb.Draw(m_selecta, selectaPos, null, Color.White, 0, new Vector2(m_selecta.Width / 2, m_selecta.Height / 2), 1, SpriteEffects.None, 0);

                switch (currOption)
                {
                    case OptionSelected.Arcade:

                        sb.Draw(arcEdge, pos1, null, Color.White, 0, new Vector2(arcEdge.Width / 2, arcEdge.Height / 2), 1, SpriteEffects.None, 0);
                        selectaPos = pos1;
                        break;

                    case OptionSelected.Adventure:

                        sb.Draw(advEdge, pos2, null, Color.White, 0, new Vector2(advEdge.Width / 2, advEdge.Height / 2), 1, SpriteEffects.None, 0);
                        selectaPos = pos2;
                        break;

                    case OptionSelected.Tutorial:

                        sb.Draw(tutEdge, pos3, null, Color.White, 0, new Vector2(tutEdge.Width / 2, tutEdge.Height / 2), 1, SpriteEffects.None, 0);
                        selectaPos = pos3;
                        break;

                    case OptionSelected.Quit:

                        sb.Draw(quitEdge, pos4, null, Color.White, 0, new Vector2(quitEdge.Width / 2, quitEdge.Height / 2), 1, SpriteEffects.None, 0);
                        selectaPos = pos4;
                        break;

                }
            }

        }
    }
}
