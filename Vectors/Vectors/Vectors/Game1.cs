using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using BookExample;
using Vectors.GraphicSupport;

namespace Vectors
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        static public GraphicsDeviceManager mGraphics;
        static public SpriteBatch mSpriteBatch;
        static public ContentManager sContent;
        Vector2 kPointSize = new Vector2(5f, 5f);

        // Size of all the positions Vector2 kPointSize = new Vector2(5f, 5f);

        // Work with TexturedPrimitive 
        TexturedPrimitive mPa, mPb;               // The locators for showing Point A and Point B 
        TexturedPrimitive mPx;                    // to show same displacement can be applied to any position

        TexturedPrimitive mPy;                    // To show we can rotate/manipulate vectors independently 
        Vector2 mVectorAtPy = new Vector2(10, 0); // Start with vector in the X direction;

        TexturedPrimitive mCurrentLocator;


        public Game1()
        {
            Content.RootDirectory = "Content";
            Game1.sContent = Content;

            mGraphics = new GraphicsDeviceManager(this);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            mSpriteBatch = new SpriteBatch(GraphicsDevice);

            // Create the primitives    
            mPa = new TexturedPrimitive("arrow", new Vector2(30, 30), kPointSize);
            mPb = new TexturedPrimitive("arrow", new Vector2(60, 30), kPointSize);
            mPx = new TexturedPrimitive("arrow", new Vector2(20, 10), kPointSize);
            mPy = new TexturedPrimitive("arrow", new Vector2(20, 50), kPointSize);
            mCurrentLocator = mPa;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here

            #region Step 3a. Change current selected vector    
            if (InputWrapper.Buttons.A == ButtonState.Pressed)
                mCurrentLocator = mPa;
            else if (InputWrapper.Buttons.B == ButtonState.Pressed)
                mCurrentLocator = mPb;
            else if (InputWrapper.Buttons.X == ButtonState.Pressed)
                mCurrentLocator = mPx;
            else if (InputWrapper.Buttons.Y == ButtonState.Pressed)
                mCurrentLocator = mPy;
            #endregion

            #region Step 3b. Move Vector  Change the current locator position    
            mCurrentLocator.mPosition +=InputWrapper.ThumbSticks.Right;
            #endregion
            

            #region Step 3c. Rotate Vector    // Left thumbstick-X rotates the vector at Py    
            float rotateYByRadian = MathHelper.ToRadians(InputWrapper.ThumbSticks.Left.X);
            #endregion

            #region Step 3d. Increase/Decrease the length of vector    // Left thumbstick-Y increase/decrease the length of vector at Py    
            float vecYLen = mVectorAtPy.Length();
            vecYLen += InputWrapper.ThumbSticks.Left.Y;
            #endregion

            #region Step 3e. Compute vector changes    // Compute the rotated direction of vector at Py    
            mVectorAtPy = ShowVector.RotateVectorByAngle(mVectorAtPy, rotateYByRadian);    
            mVectorAtPy.Normalize(); // Normalize vectorAtPy to size of 1f    
            mVectorAtPy *= vecYLen;  // Scale the vector to the new size #endregion
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mSpriteBatch.Begin();
            // TODO: Add your drawing code here
            // Drawing the vectors    
            Vector2 v = mPb.mPosition - mPa.mPosition;  // Vector V is from Pa to Pb

            // Draw Vector-V at Pa, and Px    
            ShowVector.DrawFromTo(mPa.mPosition, mPb.mPosition);
            ShowVector.DrawPointVector(mPx.mPosition, v);
            // Draw vectorAtPy at Py    
            ShowVector.DrawPointVector(mPy.mPosition, mVectorAtPy);

            mPa.Draw();
            mPb.Draw();
            mPx.Draw();
            mPy.Draw();

            mSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
