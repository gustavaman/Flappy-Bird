using Flappy_Bird.Content;
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
        //Hej Gustav

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, background2, hitbox, gameover;
        SpriteFont font;
        List<MarsRock> rocks;
        List<Meteor> meteors;
        CyberTruck player;
        List<Bullet> bullets;
        Vector2 back_pos, back2_pos, spawnrecsize;
        bool isDead = false;
        bool devMode = false;
        bool isPress = false;
        bool isPress2 = false;
        bool isShooting = false;
        bool godMode = false;
        int maxmeteors = 2;
        int score = 0;
        Random random;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 396;
            graphics.PreferredBackBufferWidth = 1214;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            player = new CyberTruck(new Vector2(200, 290), 100, this);
            rocks = new List<MarsRock>();
            meteors = new List<Meteor>();
            bullets = new List<Bullet>();
            random = new Random();
            hitbox = new Texture2D(GraphicsDevice, 1, 1);
            hitbox.SetData(new Color[] { Color.Red });
            Components.Add(player);
            IsMouseVisible = true;
            spawnrecsize = new Vector2(304, 140);
            Mouse.SetCursor(MouseCursor.Crosshair);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            background = Content.Load<Texture2D>("bakgrund");
            background2 = Content.Load<Texture2D>("bakgrund2");
            gameover = Content.Load<Texture2D>("gameover");
            font = Content.Load<SpriteFont>("score");

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

            //Ju längre tid spelet pågår desto fler meteorer spawnas.
            if (gameTime.TotalGameTime.Seconds > 15)
            {
                maxmeteors++;
                gameTime.TotalGameTime = gameTime.TotalGameTime.Subtract(gameTime.TotalGameTime);
            }

            //Rullande bakgrund
            BackgroundLogic();

            //Kollisionslogik
            CollisionLogic();

            //Metod som spawnar objekt c:
            ObjectSpawner();

            //Metod för omstart
            if (isDead && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                rocks.Clear();
                meteors.Clear();
                bullets.Clear();
                player.pos = new Vector2(200, 290);
                score = 0;
                gameTime.TotalGameTime = gameTime.TotalGameTime.Subtract(gameTime.TotalGameTime);
                maxmeteors = 2;
                spawnrecsize = new Vector2(304, 140);
                isDead = false;
            }

            //Metod för att visa hitboxes
            DevMode();

            //Metod för att vara odödlig
            GodMode();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
                        
            spriteBatch.Draw(background, back_pos, new Rectangle(0, 0, background.Width, background.Height), Color.White);

            spriteBatch.Draw(background2, back2_pos, new Rectangle(0, 0, background.Width, background.Height), Color.White);

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(spriteBatch);
                if (devMode)
                    spriteBatch.Draw(hitbox, bullets[i].rec, Color.Red);
            }

            for (int i = 0; i < rocks.Count; i++)
            {
                rocks[i].Draw(spriteBatch);
                if (devMode)
                    spriteBatch.Draw(hitbox, rocks[i].rec, Color.Red);
            }
            
            for (int i = 0; i < meteors.Count; i++)
            {
                meteors[i].Draw(spriteBatch);
                if (devMode)
                    spriteBatch.Draw(hitbox, meteors[i].rec, Color.Red);
            }

            player.Draw(spriteBatch);

            if (devMode)
                spriteBatch.Draw(hitbox, player.rec, Color.Red);

            if (isDead)
                spriteBatch.Draw(gameover, new Rectangle(0, 0, 1214, 396), Color.White);
                       
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(10, 10), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }        

        public void BackgroundLogic()
        {
            // back2_pos.X + 1214 används eftersom det annars bildas ett svart streck i skarven mellan bakgrunderna. Strecket uppstår pga att bakgrundens hastighet inte är en faktor av dess bredd.
            if (back_pos.X <= -background.Width)
                back_pos.X = back2_pos.X + 1214;

            if (back2_pos.X <= -background.Width)
                back2_pos.X = back_pos.X + 1214;

            back_pos.X -= 6;
            back2_pos.X -= 6;
        }

        public void CollisionLogic()
        {
            if (!godMode)
            {
                for (int i = 0; i < rocks.Count; i++)
                    if (player.rec.Intersects(rocks[i].rec))
                        isDead = true;

                for (int i = 0; i < meteors.Count; i++)
                    if (player.rec.Intersects(meteors[i].rec))
                        isDead = true;
            }

            for (int i = 0; i < bullets.Count; i++)
                for (int p = 0; p < meteors.Count; p++)
                    if (bullets[i].rec.Intersects(meteors[p].rec) && !isDead)
                    {
                        meteors.RemoveAt(p);
                        bullets.RemoveAt(i);
                        score++;

                        // Om man tar bort en bullet eller meteor och kör for-loopen igen för samma värde på en av dem kommer indexet inte att finnas längre.
                        break;
                    }

        }

        public void ObjectSpawner()
        {
            //Det tillåtna avståndet mellan meteorer minskar med tiden. Detta då de annars inte hade spawnat alls när det är många meteorer i spelet.
            spawnrecsize.X -= 0.04f;
            spawnrecsize.Y -= 0.02f;

            //Tar bort stenar när de hamnat utanför bild till vänster.
            for (int i = 0; i < rocks.Count; i++)
                if (rocks[i].pos.X <= -120)
                    rocks.RemoveAt(i);

            //Stenar och meteorer kan inte spawna nära varandra.
            for (int i = 0; i < rocks.Count; i++)
                for (int p = 0; p < rocks.Count; p++)
                    if (i != p && rocks[i].spawnrec.Intersects(rocks[p].spawnrec))
                        rocks.RemoveAt(p);

            for (int i = 0; i < meteors.Count; i++)
                for (int p = 0; p < meteors.Count; p++)
                    if (i != p && meteors[i].spawnrec.Intersects(meteors[p].spawnrec))
                    {
                        meteors.RemoveAt(p);
                        break;
                    }
            
            //Det finns alltid 3 stenar, även om alla inte alltid är på bilden. När en ny sten skapas görs en Component.Add för den stenen.
            while (rocks.Count < 3)
            {
                rocks.Add(new MarsRock(new Vector2(random.Next(1214, 3000), 270), this));
                Components.Add(rocks[rocks.Count - 1]);
            }

            for (int i = 0; i < meteors.Count; i++)
                if (meteors[i].pos.Y >= 396 || meteors[i].pos.X <= -60)
                    meteors.RemoveAt(i);

            //Samma logik för meteorer som för stenar.
            while (meteors.Count < maxmeteors)
            {
                meteors.Add(new Meteor(new Vector2(random.Next(1714, 2000), random.Next(-600, -200)), spawnrecsize, this));
                Components.Add(meteors[meteors.Count - 1]);
            }

            for (int i = 0; i < bullets.Count; i++)
                if (bullets[i].pos.Y >= 396 || bullets[i].pos.Y <= -31 || bullets[i].pos.X <= -31 || bullets[i].pos.X >= 1214)
                    bullets.RemoveAt(i);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && bullets.Count < 3 && !isDead && !isShooting)
            {
                double bulletvel = 100;

                /* Uträkningen av bullet.vel.X kan förenklat skrivas på följande vis, där m är musen, b är bullet och x och y anger vilken av dess koord
                sqrt(bulletvel/((mus.pos.Y-bullet.pos.Y)/(mus.pos.X-bullet.pos.X)+1))
                För att beräkna bullet.vel.Y multiplicerar man bullet.vel.X med (mus.pos.Y-bullet.pos.Y)/(mus.pos.X-bullet.pos.X)
                På grund av att uträkningen är en andragradsekvation behövs ett fall då musen är till höger om bilen och ett där den är till vänster.
                Det behövs även två fall då musens x-position är på samma pixel som bulletspawnern, ett då musen är över bilen och ett då musen är under bilen.
                */

                if (Mouse.GetState().X > (player.pos.X + 65))
                    bullets.Add(new Bullet(new Vector2(player.pos.X + 35, player.pos.Y + 30), new Vector2((float)Math.Sqrt(bulletvel / (Math.Pow((Mouse.GetState().Y - (player.pos.Y + 57)) / (Mouse.GetState().X - (player.pos.X + 65)), 2) + 1)), (float)Math.Pow(bulletvel / (Math.Pow((Mouse.GetState().Y - (player.pos.Y + 57)) / (Mouse.GetState().X - (player.pos.X + 65)), 2) + 1), 0.5) * (Mouse.GetState().Y - (player.pos.Y + 57)) / (Mouse.GetState().X - (player.pos.X + 65))), this));
                else if (Mouse.GetState().X < (player.pos.X + 65))
                    bullets.Add(new Bullet(new Vector2(player.pos.X + 35, player.pos.Y + 30), new Vector2((float)-Math.Sqrt(bulletvel / (Math.Pow((Mouse.GetState().Y - (player.pos.Y + 57)) / (Mouse.GetState().X - (player.pos.X + 65)), 2) + 1)), (float)-Math.Pow(bulletvel / (Math.Pow((Mouse.GetState().Y - (player.pos.Y + 57)) / (Mouse.GetState().X - (player.pos.X + 65)), 2) + 1), 0.5) * (Mouse.GetState().Y - (player.pos.Y + 57)) / (Mouse.GetState().X - (player.pos.X + 65))), this));
                else if (Mouse.GetState().Y < (player.pos.Y + 57))
                    bullets.Add(new Bullet(new Vector2(player.pos.X + 35, player.pos.Y + 30), new Vector2(0, (float)-Math.Sqrt(bulletvel)), this));
                else
                    bullets.Add(new Bullet(new Vector2(player.pos.X + 35, player.pos.Y + 30), new Vector2(0, (float)Math.Sqrt(bulletvel)), this));

                Components.Add(bullets[bullets.Count - 1]);

                isShooting = true;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
                isShooting = false;
        }

        private void DevMode()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B) && !isPress)
            {
                if (!devMode)
                    devMode = true;
                else
                    devMode = false;

                isPress = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.B))
                isPress = false;
        }

        private void GodMode()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.G) && !isPress2)
            {
                if (!godMode)
                    godMode = true;
                else
                    godMode = false;

                isPress2 = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.G))
                isPress2 = false;
        }
    }
}
