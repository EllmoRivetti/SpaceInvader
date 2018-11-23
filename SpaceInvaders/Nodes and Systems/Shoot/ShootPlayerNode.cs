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
        public TransformComponent PlayerPosition { get; set; }
        public ShootComponent ShootComponent { get; set; }
        public ShootPlayerNode(Entity e)
        {
            PlayerPosition = (TransformComponent)e.GetComponent(typeof(TransformComponent));
            Console.WriteLine("Is ok since here");
            ShootComponent = (ShootComponent)e.GetComponent(typeof(ShootComponent));
        }

        public new static bool ToCreate(Entity e) => e.GetType() == typeof(SpaceInvaders.Entities.Player);

    }
}
