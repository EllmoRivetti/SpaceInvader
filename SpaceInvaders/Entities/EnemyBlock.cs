using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{ 
    class EnemyBlock:Entity
    {
        public List<EnemyLine> lineList { get; }

        public EnemyBlock()
        {
            lineList = new List<EnemyLine>();
            CreateBlock(5);
        }

        public void CreateBlock(int nbLines)
        {
            for(int i = 0; i < nbLines; i++)
            {
                EnemyLine lineFront = new EnemyLine();
                lineFront.AddNbEnemiesToLine(6, new Enemy(), (RenderForm.instance.Size.Height * 2 / 8)-i*40);
                Engine.instance.AddEntity(lineFront);
            }
          
        }
    }
}
