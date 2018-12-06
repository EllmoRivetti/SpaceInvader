using SpaceInvaders.Components;
using SpaceInvaders.Entities.Enemies.EnemyTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static SpaceInvaders.Entities.Enemy;

namespace SpaceInvaders.Entities
{
    class EnemyLine: Entity
    {

        public List<Enemy> enemyList { get; }

        public EnemyLine()
        {
            enemyList = new List<Enemy>();
        }

        public void AddNbEnemiesToLine(int nbEnemies, EnemyTag enemyTag, double y)
        {
            for(int i = 0; i < nbEnemies; i++)
            {
                switch (enemyTag)//Permet d'integrer un type specifique d'ennemis
                {
                    case EnemyTag.ALIEN:
                        Alien alien1 = new Alien();
                        AddEnemy(alien1, i ,y);
                        break;
                    case EnemyTag.UFO:
                        Ufo alien2 = new Ufo();
                        AddEnemy(alien2, i, y);
                        break;
                    case EnemyTag.SQUID1:
                        SquidOne alien3 = new SquidOne();
                        AddEnemy(alien3, i, y);
                        break;
                    case EnemyTag.SQUID2:
                        SquidTwo alien4 = new SquidTwo();
                        AddEnemy(alien4, i, y);
                        break;
                    case EnemyTag.SQUARE:
                        SquareEnemy alien5 = new SquareEnemy();
                        AddEnemy(alien5, i, y);
                        break;
                    case EnemyTag.ARMS1:
                        ArmsEnemyOne alien6 = new ArmsEnemyOne();
                        AddEnemy(alien6, i, y);
                        break;
                    case EnemyTag.ARMS2:
                        ArmsEnemyTwo alien7 = new ArmsEnemyTwo();
                        AddEnemy(alien7, i, y);
                        break;
                    default:
                        TelyEnemy alien8 = new TelyEnemy();
                        AddEnemy(alien8, i, y);
                        break;
                }
            }
        }

        public void AddEnemy(Enemy e, int i, double y)
        {
            e.SetStartPos((RenderForm.instance.Size.Width / 6) + i * 80, y);
            enemyList.Add(e);
            Engine.instance.AddEntity(e);
        }
    }
}
