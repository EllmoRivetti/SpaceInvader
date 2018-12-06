using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SpaceInvaders.Entities.Enemy;

namespace SpaceInvaders.Entities
{ 
    class EnemyBlock:Entity
    {
        public List<EnemyLine> lineList { get; }
        public Random random;

        public EnemyBlock()
        {
            lineList = new List<EnemyLine>();
            this.random = new Random();
            CreateBlock(5);
        }

        public void CreateBlock(int nbLines)
        {
            for(int i = 0; i < nbLines; i++)
            {
                EnemyLine lineFront = new EnemyLine();
                int res = this.random.Next(0, 7);
                EnemyTag tag;
                switch (res)
                {
                    case 0:
                        tag = EnemyTag.ALIEN;
                        break;
                    case 1:
                        tag = EnemyTag.UFO;
                        break;
                    case 2:
                        tag = EnemyTag.SQUID1;
                        break;
                    case 3:
                        tag = EnemyTag.SQUID2;
                        break;
                    case 4:
                        tag = EnemyTag.SQUARE;
                        break;
                    case 5:
                        tag = EnemyTag.ARMS1;
                        break;
                    case 6:
                        tag = EnemyTag.ARMS2;
                        break;
                    default:
                        tag = EnemyTag.TELY;
                        break;
                }
                lineFront.AddNbEnemiesToLine(6, tag, (RenderForm.instance.Size.Height * 2 / 8)-i*40);
                Engine.instance.AddEntity(lineFront);
            }
          
        }
    }
}
