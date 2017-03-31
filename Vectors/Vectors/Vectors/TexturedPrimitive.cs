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
using Vectors.GraphicSupport;

namespace Vectors
{
    class TexturedPrimitive
    {
        protected Texture2D mImage;
        public Vector2 mPosition;
        protected Vector2 mSize;

        private float mrotation;

        public float mRotation
        {
            get { return mrotation; }
            set { mrotation = value; }
        }


        public TexturedPrimitive(string image, Vector2 position, Vector2 size)
        {
            mImage = Game1.sContent.Load<Texture2D>(image);
            mPosition = position;
            mSize = size;
            mrotation = 0f;
        }

        public TexturedPrimitive(string image)
        {
            mrotation = 0f;
        }


        public void Update(Vector2 DeltaTranslate, Vector2 DeltaScale, float deltaAngle)
        {
            mPosition += DeltaTranslate;
            mSize += DeltaScale;
            mrotation += deltaAngle;
        }

        virtual public void Draw()
        {
            Rectangle loc = Camera.ComputePixelRectangle(mPosition, mSize);

            Vector2 centro = new Vector2(mImage.Width / 2, mImage.Height / 2);

            Game1.mSpriteBatch.Draw(mImage, loc, null, Color.White, mRotation, centro, SpriteEffects.None, 0f);

        }
    }
}
