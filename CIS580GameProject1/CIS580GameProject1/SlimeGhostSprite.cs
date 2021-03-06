﻿/*SlimeGhostSprite.cs
 * Written By: Nathan Bean
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using CIS580GameProject1.Collisions;
using Microsoft.Xna.Framework.Audio;

namespace CIS580GameProject1
{
    /// <summary>
    /// Class written by Nathan Bean to represent a slime Ghost in the game from Collision Demo
    /// </summary>
    public class SlimeGhostSprite
    {
        //private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        private Vector2 position = new Vector2(250, 300);

        private Vector2 velocity = new Vector2(0, 10);

        private SoundEffect jump;


        //private bool flipped;

        private BoundingCircle bounds = new BoundingCircle(new Vector2(200, 200), 16);

        /// <summary>
        /// the bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// The color to blend with the ghost
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("slime");
            jump = content.Load<SoundEffect>("Jump2");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            //gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 acceleration = new Vector2(0, 200);

            // Apply the gamepad movement with inverted Y axis
            /* position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);
             if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
             if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;
            */

            // Apply keyboard movement
            /*
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) position += new Vector2(0, -1) * 5;
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) position += new Vector2(0, 1) * 5;
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0) * 5;
                //flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0) * 5;
                //flipped = false;
            }
            */

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                acceleration += new Vector2(0, -1000);
                jump.Play();
            }

            velocity += acceleration * time;
            position += velocity * time;

            //update the bounds
            bounds.Center = position;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            //SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, position, null, Color, 0, new Vector2(64, 64), 0.25f, spriteEffects, 0);
        }
    }
}
