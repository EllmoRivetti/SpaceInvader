using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.Shoot
{
    class ShootPlayerNode : Node
    {
        public PositionComponent PlayerPosition { get; set; }
        public ShootComponent ShootComponent { get; set; }
        public ShootPlayerNode(Entity e)
        {
            PlayerPosition = (PositionComponent)e.GetComponent(typeof(PositionComponent));
            ShootComponent = (ShootComponent)e.GetComponent(typeof(ShootComponent));
        }

        public new static bool ToCreate(Entity e) => e.GetType() == typeof(SpaceInvaders.Entities.Player);

    }
}
