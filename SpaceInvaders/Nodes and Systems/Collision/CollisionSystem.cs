using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SpaceInvaders.Entities.Collidable;

namespace SpaceInvaders.Nodes_and_Systems.Collision
{
    class CollisionSystem : ISystem
    {
        private bool[,] TruthTableCollision;
        private List<Node> listNode;
        public CollisionSystem()
        {
            Console.WriteLine("CREATE COLLISION SYSTEM");
            CreateTruthTable();
            Console.WriteLine("END CREATION");
        }

        private void CreateTruthTable()
        {
            Console.WriteLine("CREATE TRUTH TABLE");
            TruthTableCollision = new bool[Enum.GetNames(typeof(CollisionTag)).Length, Enum.GetNames(typeof(CollisionTag)).Length];
            SetTruthTableComponent(CollisionTag.PLAYER,CollisionTag.ENEMYMISSILE,true);
            SetTruthTableComponent(CollisionTag.ENEMY, CollisionTag.PLAYERMISSILE, true);
            SetTruthTableComponent(CollisionTag.BUNKER, CollisionTag.ENEMYMISSILE, true);
            SetTruthTableComponent(CollisionTag.BUNKER, CollisionTag.PLAYERMISSILE, true);
            SetTruthTableComponent(CollisionTag.ENEMYMISSILE, CollisionTag.PLAYERMISSILE, true);
        }

        private void SetTruthTableComponent(CollisionTag tag1, CollisionTag tag2, bool result)
        {
            Console.WriteLine("SET COMPONENT");
            TruthTableCollision[(int)tag1, (int)tag2] = result;
            TruthTableCollision[(int)tag2, (int)tag1] = result;
        }

        private bool CanCollide(CollisionTag tag1, CollisionTag tag2)
        {
            return TruthTableCollision[(int)tag1,(int)tag2];
        }

        public void Update(double time)
        {
            listNode = Engine.instance.NodeListByType[typeof(CollisionNode)];
            Console.WriteLine("LIST SIZE: "+listNode.Count);
            for (int idFirstNode = 0; idFirstNode < listNode.Count; idFirstNode++)
            {
                for (int idSecondNode = 0; idSecondNode < listNode.Count; idSecondNode++)
                {
                    CollisionNode node1 = (CollisionNode)listNode[idFirstNode];
                    CollisionNode node2 = (CollisionNode)listNode[idSecondNode];

                    if(CanCollide(node1.HitBoxComponent.tag, node2.HitBoxComponent.tag))
                    {
                        if (node1.HitBoxComponent.HitBox.Collides(node2.HitBoxComponent.HitBox))
                        {
                            Engine.instance.RemoveEntity(node1.HitBoxComponent.entity);
                            Engine.instance.RemoveEntity(node2.HitBoxComponent.entity);
                        }
                    }
                }
            }
        }
    }
}
