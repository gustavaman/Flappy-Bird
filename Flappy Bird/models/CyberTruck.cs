using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird.models
{
    class CyberTruck
    {
        public Vector2 pos { get; set; }
        public int hp = 100;
        public Rectangle rec { get; set; }


        public CyberTruck()
        {
            pos = new Vector2(200, 304);
            rec = new Rectangle(200, 304, 120, 75);
        }

    }
}
