using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.Content
{ 
    class Bullet : DrawableGameComponent
    {
        public Vector2 vel;
        public Vector2 pos;
        Texture2D bullet;
        public Rectangle rec;
        public Bullet(Vector2 pos,Vector2 vel, Game game) : base(game)
        {
            this.vel = vel;
            bullet = Game.Content.Load<Texture2D>("bullet");
            this.pos = pos;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            pos.X += vel.X;
            pos.Y += vel.Y;

            rec = new Rectangle((int)pos.X + 28, (int)pos.Y + 25, 6, 6);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bullet, new Rectangle((int)pos.X, (int)pos.Y, 60, 60), Color.White);
        }

    }
}
