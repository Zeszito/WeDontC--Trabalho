﻿#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EncapsulateInput;
using Window_Size_project;
using Window_Size_project.GraphicsSupport;
using System;
#endregion

namespace BookExample
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        #region Class variables defined to be globally accessible!!
        // for drawing support
        // Convention: staticClassVariable names begin with "s"
        /// <summary>
        /// sGraphicsDevice - reference to th graphics device for current display size
        /// sSpriteBatch - reference to the SpriteBatch to draw all of the primitives
        /// sContent - reference to the ContentManager to load the textures
        /// </summary>
        static public SpriteBatch sSpriteBatch;  // Drawing support
        static public ContentManager sContent;   // Loading textures
        static public GraphicsDeviceManager sGraphics; // Current display size
        static public Random sRan;
        #endregion

        #region Preferred Window Size
        // Prefer window size
        // Convention: "k" to begin constant variable names
        const int kWindowWidth = 800;
        const int kWindowHeight = 600;
        #endregion 

        const int kNumObjects = 4;
        // Work with TexturedPrimitive Class
        TexturedPrimitive[] mGraphicsObjects; // An array of objects
        int mCurrentIndex = 0;
        TexturedPrimitive mUWBLogo;
        SoccerBall mBall;
        Vector2 mSoccerPosition = new Vector2(50, 50);
        float mSoccerBallRadius = 3f;
        public Game1()
        {

     

            // Create the primitives
            mUWBLogo = new TexturedPrimitive("UWB-PNG", new Vector2(30, 30), new Vector2(20, 20));
            mBall = new SoccerBall(mSoccerPosition, mSoccerBallRadius * 2f);

            Game1.sRan = new Random(); 
            // Content resource loading support
            Content.RootDirectory = "Content";
            Game1.sContent = Content;

            // Create graphics device to access window size
            Game1.sGraphics = new GraphicsDeviceManager(this);
            // set prefer window size
            Game1.sGraphics.PreferredBackBufferWidth = kWindowWidth;
            Game1.sGraphics.PreferredBackBufferHeight = kWindowHeight;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //// Create a new SpriteBatch, which can be used to draw textures.
            //Game1.sSpriteBatch = new SpriteBatch(GraphicsDevice);

            //// Create the primitives
            //mGraphicsObjects = new TexturedPrimitive[kNumObjects];
            //mGraphicsObjects[0] = new TexturedPrimitive("UWB-JPG", new Vector2(10, 10), new Vector2(30, 30));
            //mGraphicsObjects[1] = new TexturedPrimitive("UWB-JPG", new Vector2(200, 200), new Vector2(100, 100));
            //mGraphicsObjects[2] = new TexturedPrimitive("UWB-PNG", new Vector2(50, 10), new Vector2(30, 30));
            //mGraphicsObjects[3] = new TexturedPrimitive("UWB-PNG", new Vector2(50, 200), new Vector2(100, 100));
            ////

         
            // Create a new SpriteBatch, which can be used to draw textures.
            Game1.sSpriteBatch = new SpriteBatch(GraphicsDevice);

            // Define camera window bounds
            camera.SetCameraWindow(new Vector2(10f, 20f), 100f);

            // Create the primitives
            mGraphicsObjects = new TexturedPrimitive[kNumObjects];
            mGraphicsObjects[0] = new TexturedPrimitive(
            "UWB-JPG", // Image file name
             new Vector2(15f, 25f), // Position to draw
             new Vector2(10f, 10f)); // Size to draw
            mGraphicsObjects[1] = new TexturedPrimitive(
           "UWB-JPG",
           new Vector2(35f, 60f),
           new Vector2(50f, 50f));
            mGraphicsObjects[2] = new TexturedPrimitive(
           "UWB-PNG",
            new Vector2(105f, 25f),
           new Vector2(10f, 10f));
            mGraphicsObjects[3] = new TexturedPrimitive(
           "UWB-PNG",
           new Vector2(90f, 60f),
           new Vector2(35f, 35f));

          
            // NOTE: Since the creation of TextruedPrimitive involves loading of textures
            // The creation should occure in or after LoadContent()
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (InputWrapper.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            #region Toggle full screen and window size
            // "A" to toggle full screen
            if (InputWrapper.Buttons.A == ButtonState.Pressed)
            {
                if (!Game1.sGraphics.IsFullScreen)
                {
                    Game1.sGraphics.IsFullScreen = true;
                    Game1.sGraphics.ApplyChanges();
                }
            }

            // "B" toggles back to window
            if (InputWrapper.Buttons.B == ButtonState.Pressed)
            {
                if (Game1.sGraphics.IsFullScreen)
                {
                    Game1.sGraphics.IsFullScreen = false;
                    Game1.sGraphics.ApplyChanges();
                }
            }
            #endregion

            #region Select object and control selected object
            // Button-x to select the next object to work with
            if (InputWrapper.Buttons.X == ButtonState.Pressed)
                mCurrentIndex = (mCurrentIndex + 1) % kNumObjects;

            // Update currently working object with thumb sticks.
            mGraphicsObjects[mCurrentIndex].Update(
                    InputWrapper.ThumbSticks.Left,
                    InputWrapper.ThumbSticks.Right);
            #endregion

            mUWBLogo.Update(InputWrapper.ThumbSticks.Left, Vector2.Zero);
            mBall.Update();
            mBall.Update(Vector2.Zero, InputWrapper.ThumbSticks.Right);

            if (InputWrapper.Buttons.A == ButtonState.Pressed)
                mBall = new SoccerBall(mSoccerPosition, mSoccerBallRadius * 2f);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear to background color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Game1.sSpriteBatch.Begin(); // Initialize drawing support

            mUWBLogo.Draw();
            mBall.Draw();
            // Print out text message to echo status
            FontSupport.PrintStatus("Ball Position:" + mBall.mPosition, null);
            FontSupport.PrintStatusAt(mUWBLogo.mPosition,
            mUWBLogo.mPosition.ToString(), Color.White);
            FontSupport.PrintStatusAt(mBall.mPosition, "Radius" + mBall.Radius, Color.Red);

            // Loop over and draw each primitive
            foreach (TexturedPrimitive p in mGraphicsObjects)
            {
                p.Draw();
            }
            // Print out text message to echo status
            FontSupport.PrintStatus("Selected object is:" + mCurrentIndex +
            "Location=" + mGraphicsObjects[mCurrentIndex].mPosition, null);
           
            FontSupport.PrintStatusAt(mGraphicsObjects[mCurrentIndex].mPosition, "Selected", Color.Red);

            Game1.sSpriteBatch.End(); // inform graphics system we are done drawing

            base.Draw(gameTime);

        }
    }
}