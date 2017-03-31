using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Vectors.GraphicSupport
{
    class ShowVector
    {
        protected static Texture2D sImage = null;
        private static float klenTOWIdthRatio = 0.2f;

        static private void LoadImage()
        {
            if (null == sImage)
                sImage = Game1.sContent.Load<Texture2D>("arrow");
        }

        static public void DrawPointVector(Vector2 from, Vector2 dir)
        {
            LoadImage();

            #region 1 Compute the angle to rotate
            float length = dir.Length();
            float theta = 0f;

            if (length > 0.0001f)
            {
                dir /= length;
                theta = (float)Math.Acos((double)dir.X);

                if (dir.X < 0.0f)
                {
                    if (dir.Y > 0.0f)
                        theta = -theta;
                }
                else
                {
                    if (dir.Y > 0.0f)
                        theta = -theta;
                }
            }

            #endregion

            //Definir localização e do tamanho da imagem
            Vector2 size = new Vector2(length,klenTOWIdthRatio * length);
            Rectangle dest = Camera.ComputePixelRectangle(from, size);
            Vector2 org = new Vector2(0f, ShowVector.sImage.Height / 2f);
            Game1.mSpriteBatch.Draw(ShowVector.sImage, dest, null, Color.White, theta, org, SpriteEffects.None, 0f);

        }

        static public void DrawFromTo(Vector2 from, Vector2 to) {
            DrawPointVector(from, to - from);
        }

        static public Vector2 RotateVectorByAngle(Vector2 v, float angleInRadian) {
            float sinTheta = (float)(Math.Sin((double)angleInRadian));
            float cosTheta = (float)(Math.Cos((double)angleInRadian));
            float x, y;
            x = cosTheta * v.X + sinTheta * v.Y;
            y = -sinTheta * v.X + cosTheta * v.Y;

            return new Vector2(x, y); }
    }


}
