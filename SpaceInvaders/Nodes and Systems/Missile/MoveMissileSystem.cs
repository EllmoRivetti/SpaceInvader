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
            Vecteur2D movementVector = new Vecteur2D(0, 2);
            foreach (MoveMissileNode n in listNode)
            {
                if(n.IsFromPlayer)
                {
                    n.TransformComponent.Position += new Vecteur2D(0,-1);
                }
                else
                {
                    n.TransformComponent.Position += new Vecteur2D(0, +0.5);
                }  
            }
        }
    }
}
