using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Missiles
{
    class EnemyMissile:MissileAbs
    {

       
        public EnemyMissile(Vecteur2D origin) : base(origin,Image.FromFile("../../Resources/shoot1.png"), CollisionTag.ENEMYMISSILE)
        {
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            startPos.Position = origin;
        }
    }
}
