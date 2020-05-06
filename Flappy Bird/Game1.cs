﻿using Flappy_Bird.models;
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
        Texture2D car, meteor, rock, background, background2;
        CyberTruck player;
        Vector2 back_pos, back2_pos;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 396;
            graphics.PreferredBackBufferWidth = 1214;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            player = new CyberTruck(new Vector2(200, 290), 100);
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            car = Content.Load<Texture2D>("bil");
            meteor = Content.Load<Texture2D>("meteor");
            rock = Content.Load<Texture2D>("MarsRock");
            background = Content.Load<Texture2D>("bakgrund");
            background2 = Content.Load<Texture2D>("bakgrund2");
                        
            back_pos = new Vector2(0, 0);
            back2_pos = new Vector2(background.Width, 0);
        }
        
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Rörelse i sidled som stoppas vid skärmens kanter.
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && player.pos.X < 1094)
                player.pos = new Vector2(player.pos.X + 7, player.pos.Y);
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && player.pos.X > 0)
                player.pos = new Vector2(player.pos.X - 7, player.pos.Y);
                        
            // back2_pos.X + 1214 används eftersom det annars bildas ett svart streck i skarven mellan bakgrunderna. Strecket uppstår pga att bakgrundens hastighet inte är en faktor av dess bredd.
            if (back_pos.X <= -background.Width)
                back_pos.X = back2_pos.X + 1214;

            if (back2_pos.X <= -background.Width)
                back2_pos.X = back_pos.X + 1214;

            back_pos.X -= 4;
            back2_pos.X -= 4;
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(background, back_pos, new Rectangle(0, 0, background.Width, background.Height), Color.White);

            spriteBatch.Draw(background2, back2_pos, new Rectangle(0, 0, background.Width, background.Height), Color.White);
            
            spriteBatch.Draw(car, new Rectangle((int)player.pos.X, (int)player.pos.Y, 120, 120), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
