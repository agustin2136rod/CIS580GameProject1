/*GameProject1.cs
 * Written By: Agustin Rodriguez
 */
using CIS580GameProject1.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Timers;

namespace CIS580GameProject1
{
    /// <summary>
    /// Class to represent the entire GameProject1 where a slime ghost has to evade a moving ball for as long as possible
    /// </summary>
    public class GameProject1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //set up all variables
        private Texture2D ballTexture;
        private SlimeGhostSprite slimeGhost;
        private SpriteFont spriteFont;
        private BoundingCircle bounding;
        private Stopwatch stopWatch;
        private BatSprite bat;
        

        /// <summary>
        /// constructor
        /// </summary>
        public GameProject1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //instructions for the game
            Window.Title = "Don't Get Hit Game - Avoid getting hit by the bat for as long as possible!";
            
        }

        /// <summary>
        /// initialization of the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bounding = new BoundingCircle(new Vector2(200, 500), 16);
            bat = new BatSprite() { Position = new Vector2(150, 150), Direction = Direction.Right };

            slimeGhost = new SlimeGhostSprite();
            stopWatch = new Stopwatch();
            stopWatch.Start();

            base.Initialize();
        }

        /// <summary>
        /// content to load into the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bat.LoadContent(Content);
            slimeGhost.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("arial");
            bat.LoadContent(Content);
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Update the game as events happen
        /// </summary>
        /// <param name="gameTime">total game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            slimeGhost.Update(gameTime);
            bat.Update(gameTime);
            slimeGhost.Color = Color.White;
            if (slimeGhost.Bounds.CollidesWith(bounding))
            {
                slimeGhost.Color = Color.Black;
                
                stopWatch.Restart();
            }

            

            base.Update(gameTime);
        }

        /// <summary>
        /// Method that draws each of the game pieces on the window
        /// </summary>
        /// <param name="gameTime">total time of the game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            slimeGhost.Draw(gameTime, _spriteBatch);
            bat.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(spriteFont, $"Total time without being hit:{stopWatch.Elapsed:c}", new Vector2(2, 2), Color.Gold);
            _spriteBatch.DrawString(spriteFont, $"Press space to Jump:", new Vector2(0, 45), Color.Gold);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
