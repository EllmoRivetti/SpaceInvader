using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Entities.Missiles;
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
        private int EnlargedWidthHitbox;
        public CollisionSystem()
        {
            EnlargedWidthHitbox = 2;
            CreateTruthTable();
        }

        private void CreateTruthTable()
        {
            TruthTableCollision = new bool[Enum.GetNames(typeof(CollisionTag)).Length, Enum.GetNames(typeof(CollisionTag)).Length];
            SetTruthTableComponent(CollisionTag.PLAYER,CollisionTag.ENEMYMISSILE,true);
            SetTruthTableComponent(CollisionTag.ENEMY, CollisionTag.PLAYERMISSILE, true);
            SetTruthTableComponent(CollisionTag.BUNKER, CollisionTag.ENEMYMISSILE, true);
            SetTruthTableComponent(CollisionTag.BUNKER, CollisionTag.PLAYERMISSILE, true);
            SetTruthTableComponent(CollisionTag.ENEMYMISSILE, CollisionTag.PLAYERMISSILE, true);
            //SetTruthTableComponent(CollisionTag.BUNKER, CollisionTag.ENEMY, true);//Permet d'ajouter la collision entre les ennemis et les bunkers. Si elle est activée, les ennemis detruiront les bunkers sans etre détruits quand ils passeront dessus
            SetTruthTableComponent(CollisionTag.ENEMY, CollisionTag.PLAYER, true);
        }

        private void SetTruthTableComponent(CollisionTag tag1, CollisionTag tag2, bool result)
        {
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
            for (int idFirstNode = 0; idFirstNode < listNode.Count; idFirstNode++)
            {
                for (int idSecondNode = 0; idSecondNode < listNode.Count; idSecondNode++)
                {
                    if(idFirstNode < listNode.Count && listNode[idFirstNode] is CollisionNode node1 && idSecondNode < listNode.Count && listNode[idSecondNode] is CollisionNode node2)
                    {
                        if (CanCollide(node1.HitBoxComponent.tag, node2.HitBoxComponent.tag))
                        {
                            if (node1.HitBoxComponent.HitBox.Collides(node2.HitBoxComponent.HitBox))
                            {
                                if (node1.HitBoxComponent.tag == CollisionTag.BUNKER || node2.HitBoxComponent.tag == CollisionTag.BUNKER)
                                {
                                    if (node1.HitBoxComponent.tag == CollisionTag.BUNKER)
                                    {
                                        ManageAccurateCollision(node1, node2);
                                    }
                                    else if (node2.HitBoxComponent.tag == CollisionTag.BUNKER)
                                    {
                                        ManageAccurateCollision(node2, node1);
                                    }
                                }
                                else if (node1.HitBoxComponent.tag == CollisionTag.PLAYER || node2.HitBoxComponent.tag == CollisionTag.PLAYER)
                                {
                                    ManagePlayerCollision(node1, node2);
                                }
                                else
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

        public void ManagePlayerCollision(CollisionNode node1, CollisionNode node2)
        {
            if(node1.HitBoxComponent.tag == CollisionTag.ENEMY || node2.HitBoxComponent.tag == CollisionTag.ENEMY)
            {
                ManageEnemyPlayerCollision(node1, node2);
            }
            else
            {
                ManageMissilePlayerColision(node1, node2);
            }
        }


        public void ManageEnemyPlayerCollision(CollisionNode node1, CollisionNode node2)
        {
            if (node1.HitBoxComponent.tag == CollisionTag.PLAYER)
            {
                ((SpaceInvaders.Entities.Player)node1.HitBoxComponent.entity).Life = 0;
            }
            else if (node2.HitBoxComponent.tag == CollisionTag.PLAYER)
            {
                ((SpaceInvaders.Entities.Player)node2.HitBoxComponent.entity).Life = 0;
            }
        }

        public void ManageMissilePlayerColision(CollisionNode node1, CollisionNode node2)
        {
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

        public void ManageAccurateCollision(CollisionNode bunkerCollisionNode, CollisionNode missileCollisionNode)
        {
            Box intersection = GetIntersection(bunkerCollisionNode.HitBoxComponent.HitBox, missileCollisionNode.HitBoxComponent.HitBox);

            Box relativeHitBox = new Box
            {
                X = Clamp(intersection.X - bunkerCollisionNode.HitBoxComponent.HitBox.X - EnlargedWidthHitbox, 0, bunkerCollisionNode.RenderComponent.sprite.Width),
                Y = Clamp(intersection.Y - bunkerCollisionNode.HitBoxComponent.HitBox.Y , 0, bunkerCollisionNode.RenderComponent.sprite.Height),

                XPlusWidth = Clamp(intersection.XPlusWidth - bunkerCollisionNode.HitBoxComponent.HitBox.X + EnlargedWidthHitbox, 0, bunkerCollisionNode.RenderComponent.sprite.Width),
                YPlusHeight = Clamp(intersection.YPlusHeight - bunkerCollisionNode.HitBoxComponent.HitBox.Y , 0, bunkerCollisionNode.RenderComponent.sprite.Height)
            };

            if (ManagePixelCollisionsBunker(bunkerCollisionNode.RenderComponent, relativeHitBox, missileCollisionNode))
            {
                if(missileCollisionNode.HitBoxComponent.tag != CollisionTag.ENEMY)
                    Engine.instance.RemoveEntity(missileCollisionNode.HitBoxComponent.entity);
            }
        }

        public static double Clamp(double valeur,double debut, double fin)
        {
            if(valeur < debut)
            {
                valeur = debut;
            }else if(valeur > fin)
            {
                valeur = fin;
            }
            return valeur;
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

        public static bool ManagePixelCollisionsBunker(RenderComponent renderComponent, Box rectToColor, CollisionNode missileCollisionNode)
        {
            Color actualColor;
            MissileAbs missileEntity = missileCollisionNode.HitBoxComponent.entity as MissileAbs;
            for (int i = (int)rectToColor.X; i < (int)rectToColor.XPlusWidth; i++)
            {
                for (int j = (int)rectToColor.Y; j < (int)rectToColor.YPlusHeight; j++)
                {
                    actualColor = ((Bitmap)renderComponent.sprite).GetPixel(i, j);

                    if(missileEntity.NbPixelToDestroy > 0)
                    {
                        if (actualColor.A == 255 && actualColor.R == 0 && actualColor.B == 0 && actualColor.G == 0)
                        {
                            ((Bitmap)renderComponent.sprite).SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                            missileEntity.NbPixelToDestroy--;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
