using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.models
{
    class MarsRock : DrawableGameComponent
    {
        public Vector2 pos;
        Texture2D rock;
        public Rectangle rec;
        public MarsRock(Vector2 pos, Game game): base(game)
        {
            rock = Game.Content.Load<Texture2D>("MarsRock");
            this.pos = pos;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            pos.X -= 6;
           

            rec = new Rectangle((int)pos.X, (int)pos.Y, 120, 120);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rock, rec, Color.White);
        }
    }
}
