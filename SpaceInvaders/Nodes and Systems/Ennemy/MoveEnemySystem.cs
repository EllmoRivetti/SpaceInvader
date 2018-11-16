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

            Vecteur2D movementVector = new Vecteur2D();
            if (Engine.instance.keyPressed.Contains(Keys.Left))
            {
                movementVector += new Vecteur2D(time, 0);
            }
            if (Engine.instance.keyPressed.Contains(Keys.Right))
            {
                movementVector += new Vecteur2D(-time, 0);
            }
            if (movementVector != new Vecteur2D())
            {
                foreach (MoveEnemyNode n in listNode)
                {
                    Vecteur2D tempPos = n.TransformComponent.Position + movementVector * n.VelocityComponent.Velocity;
                    if (tempPos.x > RenderForm.instance.Width - n.RenderComponent.sprite.Width)
                    {
                        tempPos.x = RenderForm.instance.Width - n.RenderComponent.sprite.Width;
                    }
                    else if (tempPos.x < 0)
                    {
                        tempPos.x = 0;
                    }
                    n.TransformComponent.Position = tempPos;
                }
            }
        }
    }
}
