using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DrawAndControl
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Support for loading and drawing the JPG image
        Texture2D mJPGImage; // The UWB-JPG.jpg image to be loaded
        Vector2 mJPGPosition; // Top-Left pixel position of UWB-JPG.jpg
                              // Support for loading and drawing of the PNG image
        Texture2D mPNGImage; // The UWB-PNG.png image to be loaded
        Vector2 mPNGPosition; // Top-Left pixel position of UWB-PNG.png
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            // Initialize the initial image positions.
            mJPGPosition = new Vector2(10f, 10f);
            mPNGPosition = new Vector2(100f, 100f);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Load the images.
            mJPGImage = Content.Load<Texture2D>("UWB-JPG");
            mPNGImage = Content.Load<Texture2D>("UWB-PNG");
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
            #region Keyboard
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // Update the image positions with Arrow keys
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                mJPGPosition.X--;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                mJPGPosition.X++;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                mJPGPosition.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                mJPGPosition.Y++;
            // Update the image positions with AWSD
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                mPNGPosition.X--;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                mPNGPosition.X++;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                mPNGPosition.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                mPNGPosition.Y++;
            #endregion
            #region Mouse
            // Poll mouse state
            MouseState mMouseState = Mouse.GetState();
            // If left mouse button is pressed
            if (mMouseState.LeftButton == ButtonState.Pressed)
                mJPGPosition = new Vector2(mMouseState.X, mMouseState.Y);
            // If right mouse button is pressed
            if (mMouseState.RightButton == ButtonState.Pressed)
                mPNGPosition = new Vector2(mMouseState.X, mMouseState.Y);
            #endregion
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            // TODO: Add your drawing code here
            spriteBatch.Begin(); // Initialize drawing support
                                 // Draw the JPGImage
            spriteBatch.Draw(mJPGImage, mJPGPosition, Color.White);
            // Draw the PNGImage
            spriteBatch.Draw(mPNGImage, mPNGPosition, Color.White);
            spriteBatch.End(); // Inform graphics system we are done drawing

            base.Draw(gameTime);
        }
    }
}
