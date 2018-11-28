using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class Collidable : Kinematic 
    {
        public Collidable(Image image, CollisionTag tag) : base(image)
        {
            TransformComponent origin = GetComponent(typeof(TransformComponent)) as TransformComponent;
            double width = origin.Position.x + image.Width;
            double height = origin.Position.y + image.Height;
            AddComponent(new HitBoxComponent(this , tag , origin.Position , width , height));
        }

        public enum CollisionTag
        {
            PLAYER=0,
            PLAYERMISSILE,
            ENEMY,
            ENEMYMISSILE,
            BUNKER
        }
    }
}
