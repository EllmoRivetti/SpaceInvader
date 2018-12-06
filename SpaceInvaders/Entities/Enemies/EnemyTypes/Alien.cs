using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class Alien : Enemy
    {
        public Alien() : base(Image.FromFile("../../Resources/ship2.png")){}
    }
}
