using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class SquidOne : Enemy
    {
        public SquidOne() : base(Image.FromFile("../../Resources/ship3.png")) { }
    }
}
