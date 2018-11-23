using SpaceInvaders.Components;
using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.Nodes_and_Systems.Player
{
    class MovePlayerSystem : ISystem
    {
        private List<Node> listNode;
        public void Update(double time)
        {
            listNode = Engine.instance.NodeListByType[typeof(MovePlayerNode)];

            Vecteur2D movementVector = new Vecteur2D();
            if (Engine.instance.keyPressed.Contains(Keys.Left))
            {
                movementVector += new Vecteur2D(-time, 0);
            }
            if (Engine.instance.keyPressed.Contains(Keys.Right))
            {         
                movementVector += new Vecteur2D(time, 0);
            }
            if (movementVector != new Vecteur2D())
            {
                foreach (MovePlayerNode n in listNode)
                {
                    Vecteur2D tempPos = n.TransformComponent.Position + movementVector * n.VelocityComponent.Velocity;
                    if (tempPos.x > RenderForm.instance.Width-n.RenderComponent.sprite.Width-25)//-25 pour empecher le bug de fenetre ou la sprite sort a moitié
                    {
                        tempPos.x = RenderForm.instance.Width - n.RenderComponent.sprite.Width-25;//-25 pour empecher le bug de fenetre ou la sprite sort a moitié
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
