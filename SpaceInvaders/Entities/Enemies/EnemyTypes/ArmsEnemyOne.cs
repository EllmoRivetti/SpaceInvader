using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class ArmsEnemyOne : Enemy
    {
        public ArmsEnemyOne() : base(Image.FromFile("../../Resources/ship7.png")) { }
    }
}
