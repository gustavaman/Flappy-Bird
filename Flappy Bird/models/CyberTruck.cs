﻿using Microsoft.Xna.Framework;
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
        public bool isjump = false;
        Texture2D car;
        public Rectangle rec { get; set; }
        
        public CyberTruck(Vector2 pos, int hp, Game game): base(game)
        {
            car = Game.Content.Load<Texture2D>("car");
            this.pos = pos;
            this.hp = hp;         
        }

        public override void Update(GameTime gameTime)
        {
            //Om båda rörelsetangenterna är nedtryckta är hastigheten 0. Detta för att inte en tangent inte ska överskrida den andra.
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Left))
                vel.X = 0;
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && pos.X < 1089)
                vel.X = 7f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && pos.X > 4)
                vel.X = -7f;
            else
                vel.X = 0f;
            
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && isjump == false)
            {
                vel.Y = -7.5f;
                isjump = true;
            }

            pos += vel;

            //Gravitation
            if (isjump == true)
                vel.Y += 0.15f;

            //stanna y hastighet
            if (isjump == false)
                vel.Y = 0f;

            /*Marklogik.
            pos.Y återställs så att bilen inte landar under 290 pixlar vilket kunde ske pga att hastigheten inte var en faktor av 290.
            Detta kunde leda till att bilen kunde åka uppåt en liten bit utan att hoppa helt.*/
            if (pos.Y >= 290)
            {
                pos.Y = 290;
                isjump = false;
            }
                        
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
