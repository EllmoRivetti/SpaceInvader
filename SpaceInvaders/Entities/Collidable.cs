﻿using SpaceInvaders.Components;
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
            PositionComponent origin = GetComponent(typeof(PositionComponent)) as PositionComponent;
            RenderComponent render = GetComponent(typeof(RenderComponent)) as RenderComponent;
            double width = origin.Position.x + render.sprite.Width;
            double height = origin.Position.y + render.sprite.Height;
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
