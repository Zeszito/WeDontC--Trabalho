using BookExample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RotatingBall
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        static public GraphicsDeviceManager mGraphics;
        static public SpriteBatch mSpriteBatch;
        static public ContentManager sContent;

        TexturePrimitive mBall, UWMLogo;
        TexturePrimitive WhichOne;

        #region Preferred Window Size
        // Prefer window size
        // Convention: "k" to begin constant variable names
        const int kWindowWidth = 800;
        const int kWindowHeight = 600;
        #endregion 

        public Game1()
        {
            // Content resource loading support
            Content.RootDirectory = "Content";
            Game1.sContent = Content;

            // Create graphics device to access window size
            Game1.mGraphics = new GraphicsDeviceManager(this);
            // set prefer window size
            Game1.mGraphics.PreferredBackBufferWidth = kWindowWidth;
            Game1.mGraphics.PreferredBackBufferHeight = kWindowHeight;


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

            mBall = new TexturePrimitive("Soccer", new Vector2(30, 30), new Vector2(10, 15));
            UWMLogo = new TexturePrimitive("UWB-JPG", new Vector2(60, 30), new Vector2(20, 20));
            WhichOne = mBall;

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

            #region Select which primitive to work on
            if (InputWrapper.Buttons.A == ButtonState.Pressed)
                WhichOne = mBall;
            else if (InputWrapper.Buttons.B == ButtonState.Pressed)
                WhichOne = UWMLogo;
            #endregion
            #region Update the work primitive
            float rotation = 0;
            rotation = MathHelper.ToRadians(1f);
            
            //if (InputWrapper.Buttons.X == ButtonState.Pressed)
            //    rotation = MathHelper.ToRadians(4f); // 1 degree pre-press
            //else if (InputWrapper.Buttons.Y == ButtonState.Pressed)
            //    rotation = MathHelper.ToRadians(-4f); // 1 degree pre-press
            WhichOne.Update(
            InputWrapper.ThumbSticks.Left,
            InputWrapper.ThumbSticks.Right,
            rotation);
            #endregion

            // TODO: Add your update logic here

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
            mBall.Draw();
            UWMLogo.Draw();
            mSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
