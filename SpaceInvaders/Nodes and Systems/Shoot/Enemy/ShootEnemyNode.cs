using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.Shoot
{
    class ShootEnemyNode : Node
    {
        public TransformComponent EnemyPosition { get; set; }
        public ShootComponent ShootComponent { get; set; }

        public ShootEnemyNode(Entity e)
        {
            EnemyPosition = (TransformComponent)e.GetComponent(typeof(TransformComponent));
            ShootComponent = (ShootComponent)e.GetComponent(typeof(ShootComponent));
        }

        //public new static bool ToCreate(Entity e) => e.GetType() == typeof(SpaceInvaders.Entities.Enemy);
        public new static bool ToCreate(Entity e) => e is Enemy;
    }
}
