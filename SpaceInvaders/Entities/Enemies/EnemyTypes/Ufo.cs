using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class Ufo : Enemy
    {
        public Ufo() : base(Image.FromFile("../../Resources/ship4.png")) { }
    }
}
