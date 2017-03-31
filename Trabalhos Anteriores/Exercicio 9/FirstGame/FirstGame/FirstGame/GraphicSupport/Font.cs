using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BookExample;


namespace FirstGame.GraphicSupport
{
    class Font
    {
        static private SpriteFont sTheFont = null;
        static private Color sDefaultDrawColor = Color.Black;
        static private Vector2 sStatusLocation = new Vector2(5, 5);

        static public class FontSupportt
        {
            static private void LoadFont()
            {
                // for demo purposes, loads Arial.spritefont
                if (null == sTheFont) { }
                sTheFont = Game1.sContent.Load<SpriteFont>("Arial");
            }

            static private Color ColorToUse(Nullable<Color> c)
            {
                return (null == c) ? sDefaultDrawColor : (Color)c;
            }
            static public void PrintStatusAt(Vector2 pos, String msg, Nullable<Color> drawColor)
            {
                LoadFont();
                Color useColor = ColorToUse(drawColor);

                int pixelX, pixelY;
                Camera.ComputePixelPosition(pos, out pixelX, out pixelY);
                Game1.sSpriteBatch.DrawString(sTheFont, msg,
                new Vector2(pixelX, pixelY), useColor);
            }
            static public void PrintStatus(String msg, Nullable<Color> drawColor)
            {
                LoadFont();
                Color useColor = ColorToUse(drawColor);
                // compute top-left corner as the reference for output status
                Game1.sSpriteBatch.DrawString(sTheFont, msg, sStatusLocation, useColor);
            }
        }
    }
}
