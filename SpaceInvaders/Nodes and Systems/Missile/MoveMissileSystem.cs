using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.Missile
{
    class MoveMissileSystem : ISystem
    {
        private List<Node> listNode;
        public void Update(double time)
        {
            listNode = Engine.instance.NodeListByType[typeof(MoveMissileNode)];

            Console.WriteLine("nb Missile Node:");
            Console.WriteLine(listNode.Count());
            Vecteur2D movementVector = new Vecteur2D(0, 2);
            foreach (MoveMissileNode n in listNode)
            {
                Console.WriteLine("move");
                Console.WriteLine("old: " + n.TransformComponent.Position);
                // Vecteur2D tempPos = n.TransformComponent.Position + movementVector * n.VelocityComponent.Velocity;
                n.TransformComponent.Position.y -= 1.5;
                Console.WriteLine("new: " + n.TransformComponent.Position);
            }
        }
    }
}
