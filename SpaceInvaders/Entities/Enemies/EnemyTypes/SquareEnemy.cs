using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class SquareEnemy : Enemy
    {
        public SquareEnemy() : base(Image.FromFile("../../Resources/ship6.png")) { }
    }
}
