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
        public Vector2 vel, pos;
        Texture2D bullet;
        public Rectangle rec;
        public Bullet(Vector2 pos, Vector2 vel, Game game) : base(game)
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

            rec = new Rectangle((int)pos.X + 23, (int)pos.Y + 20, 16, 16);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bullet, new Rectangle((int)pos.X - 45, (int)pos.Y - 42, 150, 150), Color.White);
        }

    }
}
