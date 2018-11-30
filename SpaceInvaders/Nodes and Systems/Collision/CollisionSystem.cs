using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            SetTruthTableComponent(CollisionTag.BUNKER, CollisionTag.ENEMY, true);
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
            RenderForm.instance.Text = listNode.Count.ToString();
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
                            Console.WriteLine("Do collide: " + node1.HitBoxComponent.entity.ToString() + " and " + node2.HitBoxComponent.entity.ToString());

                            if(node1.HitBoxComponent.tag == CollisionTag.BUNKER || node2.HitBoxComponent.tag == CollisionTag.BUNKER)
                            {
                                Console.WriteLine("has bunker");
                                if(node1.HitBoxComponent.tag == CollisionTag.BUNKER)
                                {
                                    ManageAccurateCollision(node1, node2);
                                }
                                else if(node2.HitBoxComponent.tag == CollisionTag.BUNKER)
                                {
                                    ManageAccurateCollision(node2, node1);
                                }  
                            }
                            else if(node1.HitBoxComponent.tag == CollisionTag.PLAYER || node2.HitBoxComponent.tag == CollisionTag.PLAYER)
                            {
                                ManagePlayerMissileColision(node1, node2);
                            }
                            else
                            {
                                Console.WriteLine("no bunker");
                                Engine.instance.RemoveEntity(node1.HitBoxComponent.entity);
                                Engine.instance.RemoveEntity(node2.HitBoxComponent.entity);
                            }
                        }
                    }
                }
            }
        }

        public void ManagePlayerMissileColision(CollisionNode node1, CollisionNode node2)
        {
            Console.WriteLine("has player");
            if (node1.HitBoxComponent.tag == CollisionTag.PLAYER)
            {
                DecreasePlayerLife(node1);
                Engine.instance.RemoveEntity(node2.HitBoxComponent.entity);
            }
            else if (node2.HitBoxComponent.tag == CollisionTag.PLAYER)
            {
                DecreasePlayerLife(node2);
                Engine.instance.RemoveEntity(node1.HitBoxComponent.entity);
            }
        }


        public void DecreasePlayerLife(CollisionNode nodePlayer)
        {
            ((SpaceInvaders.Entities.Player)nodePlayer.HitBoxComponent.entity).Life--;
        }

        public void ManageAccurateCollision(CollisionNode node1, CollisionNode node2)
        {
            Box intersection = GetIntersection(node1.HitBoxComponent.HitBox, node2.HitBoxComponent.HitBox);

            Box relativeHitBox = new Box
            {
                X = intersection.X - node1.HitBoxComponent.HitBox.X,
                Y = intersection.Y - node1.HitBoxComponent.HitBox.Y,

                XPlusWidth = intersection.XPlusWidth - node1.HitBoxComponent.HitBox.X,
                YPlusHeight = intersection.YPlusHeight - node1.HitBoxComponent.HitBox.Y
            };

            Console.WriteLine(relativeHitBox.ToString());

            if (ChangeColorToTransparent(node1.RenderComponent, relativeHitBox))
            {
                if(node2.HitBoxComponent.tag != CollisionTag.ENEMY)
                    Engine.instance.RemoveEntity(node2.HitBoxComponent.entity);
            }
        }

        public Box GetIntersection(Box box1, Box box2)
        {
            Box intersection = new Box
            {
                X = Math.Max(box1.X, box2.X),
                XPlusWidth = Math.Min(box1.XPlusWidth, box2.XPlusWidth),

                Y = Math.Max(box1.Y, box2.Y),
                YPlusHeight = Math.Min(box1.YPlusHeight, box2.YPlusHeight)
            };

            return intersection;
        }

        public static bool ChangeColorToTransparent(RenderComponent renderComponent, Box rectToColor)
        {
            Color actualColor;
            bool bitmapHasChanged = false;
            for (int i = (int)rectToColor.X; i < (int)rectToColor.XPlusWidth; i++)
            {
                for (int j = (int)rectToColor.Y; j < (int)rectToColor.YPlusHeight; j++)
                {
                    actualColor = ((Bitmap)renderComponent.sprite).GetPixel(i, j);
                    if (actualColor.A == 255 && actualColor.R == 0 && actualColor.B == 0 && actualColor.G == 0)
                    {
                        ((Bitmap)renderComponent.sprite).SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                        bitmapHasChanged = true;
                    }

                }
            }
            return bitmapHasChanged;
        }

    }
}
