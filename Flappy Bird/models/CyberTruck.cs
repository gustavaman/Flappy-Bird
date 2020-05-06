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
        public int hp;
        
        public CyberTruck(Vector2 pos, int hp)
        {
            this.pos = pos;
            this.hp = hp;
        }
    }
}
