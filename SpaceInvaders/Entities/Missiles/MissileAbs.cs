using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    abstract class MissileAbs:Collidable
    {
        public MissileAbs(Vecteur2D origin, Image img, CollisionTag tag) : base(img,tag)
        {
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            VelocityComponent velocity = GetComponent(typeof(VelocityComponent)) as VelocityComponent;
        }
    }
}
