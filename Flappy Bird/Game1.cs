using Flappy_Bird.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Flappy_Bird
{
    public class Game1 : Game
    {
        //Hej Johan
        //hej Gustav

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D meteor, background, background2, hitbox;
        List<MarsRock> rocks;
        List<Meteor> meteors;
        CyberTruck player;
        Vector2 back_pos, back2_pos;
        
        Random random;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 396;
            graphics.PreferredBackBufferWidth = 1214;

            random = new Random();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            player = new CyberTruck(new Vector2(200, 290), 100, this);
            rocks = new List<MarsRock>();
            meteors = new List<Meteor>();
            hitbox = new Texture2D(GraphicsDevice, 1, 1);
            hitbox.SetData(new Color[] { Color.Red });
            Components.Add(player);
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            meteor = Content.Load<Texture2D>("meteor");
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

            // back2_pos.X + 1214 används eftersom det annars bildas ett svart streck i skarven mellan bakgrunderna. Strecket uppstår pga att bakgrundens hastighet inte är en faktor av dess bredd.
            if (back_pos.X <= -background.Width)
                back_pos.X = back2_pos.X + 1214;

            if (back2_pos.X <= -background.Width)
                back2_pos.X = back_pos.X + 1214;

            back_pos.X -= 6;
            back2_pos.X -= 6;

            //Metod som spawnar objekt c:
            ObjectSpawner();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(background, back_pos, new Rectangle(0, 0, background.Width, background.Height), Color.White);

            spriteBatch.Draw(background2, back2_pos, new Rectangle(0, 0, background.Width, background.Height), Color.White);
            
            player.Draw(spriteBatch);

            for (int i = 0; i < rocks.Count; i++)
            {
                rocks[i].Draw(spriteBatch);
                spriteBatch.Draw(hitbox, rocks[i].rec, Color.Red);
            }
                

            for (int i = 0; i < meteors.Count; i++)
            {
                meteors[i].Draw(spriteBatch);
                spriteBatch.Draw(hitbox, meteors[i].rec, Color.Red);
            }
                

            spriteBatch.End();

            base.Draw(gameTime);
        }



        public void ObjectSpawner()
        {
            //Tar bort stenar när de hamnat utanför bild till vänster.
            for (int i = 0; i < rocks.Count; i++)
                if (rocks[i].pos.X <= -120)
                    rocks.RemoveAt(i);

            //Det finns alltid 3 stenar, även om alla inte alltid är på bilden. När en ny sten skapas görs en Component.Add för den stenen.
            while (rocks.Count < 3)
            {
                rocks.Add(new MarsRock(new Vector2(random.Next(1214, 3000), 270), this));
                Components.Add(rocks[rocks.Count - 1]);
            }

            for (int i = 0; i < meteors.Count; i++)
                if (meteors[i].pos.Y >= 398)
                    meteors.RemoveAt(i);


            while (meteors.Count < 3)
            {
                meteors.Add(new Meteor(new Vector2(random.Next(1714, 2000), random.Next(-600, -200)), this));
                Components.Add(meteors[meteors.Count - 1]);
            }

        }
    }
}
