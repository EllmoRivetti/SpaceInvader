using SpaceInvaders.Entities.Missiles;
using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.Shoot
{
    class ShootEnemySystem : ISystem
    {
        private List<Node> listNode;
        private Random random;

        public ShootEnemySystem()
        {
            this.random = new Random();
        }

        public void Update(double time)
        {
            
            listNode = Engine.instance.NodeListByType[typeof(ShootEnemyNode)];
            //Console.WriteLine("nb node: " + listNode.Count());
            foreach (ShootEnemyNode n in listNode)
            {
                n.ShootComponent.TimeSinceLastShoot += time;
                //Console.WriteLine("n.ShootComponent.TimeSinceLastShoot: " + n.ShootComponent.TimeSinceLastShoot);
                //Console.WriteLine("n.ShootComponent.FireRate " + n.ShootComponent.FireRate);
                if (n.ShootComponent.TimeSinceLastShoot >= n.ShootComponent.FireRate)
                {
                    
                    int probabilty = this.random.Next(0, 2000000);//Changer le maximum pour diminuer la proba de tirer
                    //Console.WriteLine("---");
                    //Console.WriteLine("proba = " + probabilty);
                    //Console.WriteLine("nextShoot = " + n.ShootComponent.NextShootProbability);
                    if (probabilty < n.ShootComponent.NextShootProbability)
                    {
                        //Console.WriteLine("Shoot");
                        n.ShootComponent.NextShootProbability = n.ShootComponent.ShootBaseProbability;
                        n.ShootComponent.TimeSinceLastShoot = 0;
                        Vecteur2D posMissile = n.EnemyPosition.Position + new Vecteur2D(15, 24);
                        EnemyMissile missile = new EnemyMissile(posMissile);
                        Engine.instance.AddEntity(missile);
                    }
                    else
                    {
                        //Console.WriteLine("Increm");
                        n.ShootComponent.NextShootProbability += 1;
                    }
                }
            }
        }
    }
}
