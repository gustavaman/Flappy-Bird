using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.models
{
    class CyberTruck: DrawableGameComponent 
    {
        Vector2 pos;
        Vector2 vel;
        public int hp;
        public bool isjump = true;
        Texture2D car;
        public Rectangle rec { get; set; }

        



        public CyberTruck(Vector2 pos, int hp, Game game): base(game)
        {
            car = Game.Content.Load<Texture2D>("bil");
            this.pos = pos;
            this.hp = hp;
           
            vel.Y = 0f;
        }

        public override void Update(GameTime gameTime)
        {

            


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                vel.X = 7f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                vel.X = -7f;
            else
                vel.X = 0f;

            if (pos.X <= 0)
            {
                vel.X = 0;
                pos.X = 1;
            }
                

            if (pos.X >= 1094)
            {
                vel.X = 0;
                pos.X = 1093;
            }
                

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && isjump == false)
            {
                pos.Y -= 1f;
                vel.Y = -7.5f;
                isjump = true;
            }

            //Gravitation
            if (isjump == true)
                vel.Y += 0.15f;
             
            //Marklogik
            if (pos.Y >= 290)
                isjump = false;

            //stanna y hastighet
            if (isjump == false)
                vel.Y = 0f;





            pos += vel;


            rec = new Rectangle((int)pos.X, (int)pos.Y,120,120);

            base.Update(gameTime);
        }

        public override void Initialize()
        {
          
            base.Initialize();
        }

        protected override void LoadContent()
        {

           




            base.LoadContent();

        }

        public void Draw(SpriteBatch spritebatch)
        {
          
            spritebatch.Draw(car, rec,Color.White);
           
        }
    }
}
