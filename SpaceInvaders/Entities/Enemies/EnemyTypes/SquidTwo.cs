using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class SquidTwo : Enemy
    {
        public SquidTwo() : base(Image.FromFile("../../Resources/ship5.png")) { }
    }
}
