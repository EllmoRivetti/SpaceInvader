using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Entities;

namespace SpaceInvaders.Components
{

    /// <summary>
    /// Compsoant permettant de gérer les propriétés de tir des entitées
    /// </summary>
    class ShootComponent : Component
    {
        /// <summary>
        /// La cadence de tir 
        /// </summary>
        public double FireRate { get; set; }

        /// <summary>
        /// LA durée depuis le dernier tir
        /// </summary>
        public double TimeSinceLastShoot { get; set; }

        /// <summary>
        /// La probabilité de tir de base
        /// </summary>
        public double ShootBaseProbability { get; set; }

        /// <summary>
        /// La probabilité du prochain tir
        /// </summary>
        public double NextShootProbability { get; set; }

        /// <summary>
        /// La vitesse des missiles
        /// </summary>
        public double MissileSpeed { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="e"></param>
        /// <param name="FireRate"></param>
        /// <param name="baseProbability"></param>
        /// <param name="speed"></param>
        public ShootComponent(Entity e, double FireRate, double baseProbability, double speed) : base(e)
        {
            this.FireRate = FireRate;
            this.TimeSinceLastShoot = FireRate;
            this.ShootBaseProbability = baseProbability;
            this.NextShootProbability = 0;
            this.MissileSpeed = speed;
        }
    }
}
