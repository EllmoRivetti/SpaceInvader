using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Missiles
{
    class EnemyMissile:Missile
    {
        public EnemyMissile(Vecteur2D origin) : base(origin,Image.FromFile("../../Resources/shoot1.png"))
        {
            
        }
    }
}
