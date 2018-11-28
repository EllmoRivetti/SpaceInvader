using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.OffScreen
{
    class OffScreenSystem : ISystem
    {

        List<Node> listNode;
        public void Update(double time)
        {
            listNode = Engine.instance.NodeListByType[typeof(OffScreenNode)];
            for (int i = 0; i < listNode.Count; i++)
            {
                OffScreenNode node = (OffScreenNode)listNode[i];
                if(node.RenderComponent.view.y > RenderForm.instance.Height || node.RenderComponent.view.y + node.RenderComponent.sprite.Height < 0)
                {
                    Engine.instance.RemoveEntity(node.RenderComponent.entity);
                }
            }
        }
    }
}
