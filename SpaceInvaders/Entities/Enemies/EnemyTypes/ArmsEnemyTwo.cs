using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class ArmsEnemyTwo : Enemy
    {
        public ArmsEnemyTwo() : base(Image.FromFile("../../Resources/ship9.png")) { }
    }
}
