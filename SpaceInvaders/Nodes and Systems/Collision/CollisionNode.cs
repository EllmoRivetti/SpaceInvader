using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.Collision
{
    class CollisionNode : Node
    {
        public HitBoxComponent HitBoxComponent { get; set; }
        public RenderComponent RenderComponent { get; set; }

        public CollisionNode(Entity e)
        {
            HitBoxComponent = (HitBoxComponent)e.GetComponent(typeof(HitBoxComponent));
            RenderComponent = (RenderComponent)e.GetComponent(typeof(RenderComponent));
            //if(RenderComponent.Image == null)
            //{
            //    throw new Exception("render component is null");
            //}
        }

        public new static bool ToCreate(Entity e) => e is Collidable;
    }
}
