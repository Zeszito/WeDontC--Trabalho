using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Window_Size_project;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EncapsulateInput;
using Window_Size_project.GraphicsSupport;
using BookExample;


namespace Window_Size_project
{
    class SoccerBall : TexturedPrimitive
    {
        private Vector2 mDeltaPosition; // Change current position by this amount

        public SoccerBall(Vector2 position, float diameter) :
        base("Soccer", position, new Vector2(diameter, diameter))
        {
            //mDeltaPosition.X = (float)(Game1.sRan.NextDouble()) * 2f - 1f;
            //mDeltaPosition.Y = (float)(Game1.sRan.NextDouble()) * 2f - 1f;
        }
        public float Radius
        {
            get { return mSize.X * 0.5f; }
            set { mSize.X = 2f * value; mSize.Y = mSize.X; }
        }

        public void Update()
        {
            camera.CameraWindowCollisionStatus status =
            camera.CollidedWithCameraWindow(this);
            switch (status)
            {
                case camera.CameraWindowCollisionStatus.CollideBottom:
                case camera.CameraWindowCollisionStatus.CollideTop:
                    mDeltaPosition.Y *= -1;
                    break;
                case camera.CameraWindowCollisionStatus.CollideLeft:
                case camera.CameraWindowCollisionStatus.CollideRight:
                    mDeltaPosition.X *= -1;
                    break;
            }
            mPosition += mDeltaPosition;
        }

    }
}
