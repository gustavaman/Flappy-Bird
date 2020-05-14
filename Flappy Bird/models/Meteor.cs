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
        public Rectangle rec;
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

            rec = new Rectangle((int)pos.X, (int)pos.Y, 60, 60);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(meteor, rec, Color.White);
        }
    }
}
