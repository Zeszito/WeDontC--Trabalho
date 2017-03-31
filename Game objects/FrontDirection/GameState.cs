using FrontDirection.GraphicsSupport;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDirection
{
    class GameState
    {
        // Rocket support
        Vector2 mRocketInitDirection = Vector2.UnitY; // This does not change
        Vector2 kInitRocketPosition = new Vector2(10, 10); // Rocket support 
        GameObject mRocket; // The arrow 
        GameObject mArrow;
        // Support the flying net
        TexturedPrimitive mNet;
        bool mNetInFlight = false;
        Vector2 mNetVelocity = Vector2.Zero;
        float mNetSpeed = 0.5f;
        // Insect support
        TexturedPrimitive mInsect;
        bool mInsectPreset = true;
        // Simple game status
        int mNumInsectShot;
        int mNumMissed;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameState()
        {

            // Create and set up the primitives
            mRocket = new GameObject("Rocket", kInitRocketPosition, new Vector2(3, 10));
            mArrow = new GameObject("Arrow", new Vector2(50, 30), new Vector2(10, 4));    // Initially pointing in the x direction    
            mArrow.InitialFrontDirection = Vector2.UnitX; ;
            // Initially the rocket is pointing in the positive y direction
            mRocketInitDirection = Vector2.UnitY;
            mNet = new TexturedPrimitive("Net", new Vector2(0, 0), new Vector2(2, 5));
            mNetInFlight = false; // until user press "A", rocket is not in flight
            mNetVelocity = Vector2.Zero;
            mNetSpeed = 0.5f;

            // Initialize a new insect
            mInsect = new TexturedPrimitive("Insect", Vector2.Zero, new Vector2(5, 5));
            mInsectPreset = false;
            // Initialize game status
            mNumInsectShot = 0;
            mNumMissed = 0;

        }

        /// <summary>
        /// Update the game state
        /// </summary>
        public void UpdateGame()
        {
            // rodar o foguete
            #region Step 3a. Control and fly the rocket 
            mRocket.RotateAngleInRadian += MathHelper.ToRadians(InputWrapper.ThumbSticks.Right.X);

            mRocket.Speed += InputWrapper.ThumbSticks.Left.Y * 0.1f;

            mRocket.VelocityDirection = mRocket.FrontDirection;

            if (Camera.CollidedWithCameraWindow(mRocket) != Camera.CameraWindowCollisionStatus.InsideWindow) {
                mRocket.Speed = 0f;
                mRocket.Position = kInitRocketPosition;
            }

            mRocket.Update();
            #endregion

            #region Step 3b. Set the arrow to point toward the rocket 
            Vector2 toRocket = mRocket.Position - mArrow.Position;
            mArrow.FrontDirection = toRocket; 
            #endregion

            //Carregar no A e lancar rede
            if (InputWrapper.Buttons.A == ButtonState.Pressed)
            {
                mNetInFlight = true;
                mNet.RotateAngleInRadian = mRocket.RotateAngleInRadian;
                mNet.Position = mRocket.Position;
                mNetVelocity = ShowVector.RotateVectorByAngle(
                mRocketInitDirection,
                mNet.RotateAngleInRadian) * mNetSpeed;
            }

            //Acertei ou nao?
            if (!mInsectPreset)
            {
                float x = 15f + ((float)Game1.sRan.NextDouble() * 30f);
                float y = 15f + ((float)Game1.sRan.NextDouble() * 30f);
                mInsect.Position = new Vector2(x, y);
                mInsectPreset = true;
            }

            // ver se apanha o insecto ou mao
            if (mNetInFlight)
            {
                mNet.Position += mNetVelocity;
                if (mNet.PrimitivesTouches(mInsect))
                {
                    mInsectPreset = false;
                    mNetInFlight = false;
                    mNumInsectShot++;
                }

                if ((Camera.CollidedWithCameraWindow(mNet) !=
                Camera.CameraWindowCollisionStatus.InsideWindow))
                {
                    mNetInFlight = false;
                    mNumMissed++;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        public void DrawGame()
        {
            mRocket.Draw();
            mArrow.Draw();
            if (mNetInFlight)
                mNet.Draw();
            if (mInsectPreset)
                mInsect.Draw();
            // Print out text message to echo status
            FontSupport.PrintStatus(
            "Num insects netted = " + mNumInsectShot +
            " Num missed = " + mNumMissed + "\n", null);
            FontSupport.PrintStatus("\nRocket Speed(LeftThumb-Y)=" + mRocket.Speed + " VelocityDirection(RightThumb-X):" + mRocket.VelocityDirection, null);

            FontSupport.PrintStatusAt(mRocket.Position, mRocket.Position.ToString(), Color.White);
        }
    }
}
