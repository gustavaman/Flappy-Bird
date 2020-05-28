using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.models
{
    internal class Meteor : DrawableGameComponent
    {
        public Vector2 pos;
        Texture2D meteor;
        public Rectangle rec, spawnrec;
        public Meteor(Vector2 pos, Game game) : base(game)
        {
            meteor = Game.Content.Load<Texture2D>("meteor");
            this.pos = pos;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            pos.X -= 7;
            pos.Y += 3;

            rec = new Rectangle((int)pos.X + 8, (int)pos.Y + 8, 44, 44);
            spawnrec = new Rectangle((int)pos.X + 160, (int)pos.Y + 78, 348, 184);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(meteor, new Rectangle((int)pos.X, (int)pos.Y, 60, 60), Color.White);
        }
    }
}
