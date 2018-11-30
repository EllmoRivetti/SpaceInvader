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

        private List<Node> listPlayerNode;

        public void Update(double time)
        {
            listEnemyNode = Engine.instance.NodeListByType[typeof(MoveEnemyNode)];
            listPlayerNode = Engine.instance.NodeListByType[typeof(MovePlayerNode)];
            //Console.WriteLine("nb enemy = " + listEnemyNode.Count);
            //Console.WriteLine("nb player = " + listPlayerNode.Count);
            if (listEnemyNode.Count == 0)
            {
                Engine.instance.IsVictory = true;
            }
            if(listPlayerNode.Count == 0)
            {
                Engine.instance.IsDefeat = true;
            }
        }
    }
}
