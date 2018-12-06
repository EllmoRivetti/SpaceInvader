using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Enemies.EnemyTypes
{
    class TelyEnemy : Enemy
    {
        public TelyEnemy() : base(Image.FromFile("../../Resources/ship8.png")) { }
    }
}
