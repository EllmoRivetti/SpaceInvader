using SpaceInvaders.Nodes;
using SpaceInvaders.Nodes_and_Systems.Ennemy;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.GameManagement
{
    class CheckEndGameSystem : ISystem
    {
        private List<Node> listEnemyNode;

        public void Update(double time)
        {
            listEnemyNode = Engine.instance.NodeListByType[typeof(MoveEnemyNode)];
            
            //Console.WriteLine("nb enemy = " + listEnemyNode.Count);
            //Console.WriteLine("nb player = " + listPlayerNode.Count);
            if (listEnemyNode.Count == 0)
            {
                Engine.instance.IsVictory = true;
            }

            if (((SpaceInvaders.Entities.Player)Engine.instance.EntitiesList[0]).Life <= 0)
            {
                Engine.instance.IsDefeat = true;
            }
        }
    }
}
