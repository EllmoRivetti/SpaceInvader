using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Entities;

namespace SpaceInvaders.Components
{
    class ShootComponent : Component
    {
        public double FireRate { get; set; }
        public double TimeSinceLastShoot { get; set; }
        public double ShootBaseProbability { get; set; }
        public double NextShootProbability { get; set; }
        public ShootComponent(Entity e, double FireRate, double baseProbability) : base(e)
        {
            this.FireRate = FireRate;
            this.TimeSinceLastShoot = FireRate;
            this.ShootBaseProbability = baseProbability;
            this.NextShootProbability = 0;
        }
    }
}
