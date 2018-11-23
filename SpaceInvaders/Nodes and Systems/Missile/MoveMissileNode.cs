using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Entities.Missiles;
using SpaceInvaders.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.Missile
{
    class MoveMissileNode : Node
    {
        public TransformComponent TransformComponent { get; set; }
        public VelocityComponent VelocityComponent { get; set; }
        public MoveMissileNode(Entity e)
        {
            TransformComponent = (TransformComponent)e.GetComponent(typeof(TransformComponent));
            VelocityComponent = (VelocityComponent)e.GetComponent(typeof(VelocityComponent));
        }

        public new static bool ToCreate(Entity e) => e.GetType() == typeof(PlayerMissile);

    }
}
