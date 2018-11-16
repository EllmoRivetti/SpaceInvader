using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.Nodes_and_Systems.Ennemy
{
    class MoveEnemySystem : ISystem
    {
        private List<Node> listNode;
        public void Update(double time)
        {
            listNode = Engine.instance.NodeListByType[typeof(MoveEnemyNode)];

            

            foreach (MoveEnemyNode n in listNode)
            {
                Vecteur2D movementVector = new Vecteur2D();
                if (n.toLeft)
                {
                    movementVector += new Vecteur2D(time, 0);
                }
                else
                {
                    movementVector -= new Vecteur2D(time, 0);
                }

                Vecteur2D tempPos = n.TransformComponent.Position + movementVector * n.VelocityComponent.Velocity;
                if (tempPos.x > RenderForm.instance.Width - n.RenderComponent.sprite.Width)
                {
                    Console.WriteLine("--------");
                    foreach (MoveEnemyNode no in listNode)
                    {
                        Console.WriteLine("Node info: " + no.TransformComponent.ToString());
                        no.TransformComponent.Position.y += 25;
                        no.toLeft = false;
                    }
                }
                else if (tempPos.x < 0)
                {
                    
                    foreach (MoveEnemyNode no in listNode)
                    {
                        no.TransformComponent.Position.y += 25;
                        no.toLeft = true;
                        Console.WriteLine("Node info: " + no.TransformComponent.ToString());
                    }
                }
                else
                {
                    n.TransformComponent.Position = tempPos;
                }
            }
        }
    }
}
