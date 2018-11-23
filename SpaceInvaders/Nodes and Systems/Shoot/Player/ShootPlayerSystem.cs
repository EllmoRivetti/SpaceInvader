using SpaceInvaders.Entities;
using SpaceInvaders.Entities.Missiles;
using SpaceInvaders.Nodes;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.Nodes_and_Systems.Shoot
{
    class ShootPlayerSystem : ISystem
    {
        private List<Node> listNode;
        public void Update(double time)
        {
            listNode = Engine.instance.NodeListByType[typeof(ShootPlayerNode)];
            bool toShoot = false;
            if (Engine.instance.keyPressed.Contains(Keys.Space))
            {
                //Console.WriteLine("TO SHOOT");
                toShoot = true;
            }

            if (toShoot)
            {
                LaunchMissile(time);
            }
        }

        public void LaunchMissile(double time)
        {
            foreach (ShootPlayerNode n in listNode)
            {
                n.ShootComponent.TimeSinceLastShoot += time;
                //Console.WriteLine("Time since last shoot: "+ n.ShootComponent.TimeSinceLastShoot);
                //Console.WriteLine("Fire rate: " + n.ShootComponent.FireRate);
                if (n.ShootComponent.TimeSinceLastShoot > n.ShootComponent.FireRate)
                {
                    n.ShootComponent.TimeSinceLastShoot = 0;
                    Vecteur2D posMissile = n.PlayerPosition.Position + new Vecteur2D(15,-24);
                    PlayerMissile missile = new PlayerMissile(posMissile);
                    Engine.instance.AddEntity(missile);
                }
            }
        }
    }
}
