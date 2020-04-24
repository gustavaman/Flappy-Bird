using Flappy_Bird.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flappy_Bird
{
    public class Game1 : Game
    {
        //Hej Johan
        //hej Gustav

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D car, meteor, rock, background;
        CyberTruck player;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Constants.resolution_Y;
            graphics.PreferredBackBufferWidth = Constants.resolution_X;

            player = new CyberTruck();

        
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            car = Content.Load<Texture2D>("Car");
            meteor = Content.Load<Texture2D>("meteor");
            rock = Content.Load<Texture2D>("MarsRock");
            background = Content.Load<Texture2D>("bakgrund");
        }
        
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.resolution_X, Constants.resolution_Y), Color.White);

            spriteBatch.Draw(car, player.rec, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
