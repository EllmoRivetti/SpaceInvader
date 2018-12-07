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
        public int NbPixelToDestroy { get; set; }
        public MissileAbs(Vecteur2D origin, Image img, CollisionTag tag) : base(img,tag)
        {
            NbPixelToDestroy = 14;
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            VelocityComponent velocity = GetComponent(typeof(VelocityComponent)) as VelocityComponent;
        }
    }
}
