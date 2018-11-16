using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class EnemyLine: Entity
    {

        public List<Enemy> enemyList { get; }

        public EnemyLine()
        {
            enemyList = new List<Enemy>();
        }

        public void AddNbEnemiesToLine(int nbEnemies, Enemy enemyType, double y)
        {
            for(int i = 0; i < nbEnemies; i++)
            {
                Enemy e = new Enemy();
                e.SetStartPos((RenderForm.instance.Size.Width / 6)+i*80, y);
                enemyList.Add(e);
                Engine.instance.AddEntity(e);
            }
        }
    }
}
