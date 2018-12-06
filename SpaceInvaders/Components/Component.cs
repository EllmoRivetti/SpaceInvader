using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Components
{
    /// <summary>
    /// Classe mère a tous les différents compoants
    /// </summary>
    abstract class Component
    {
        /// <summary>
        /// L'entitée pour laquelle existe le composant
        /// </summary>
        public Entity entity;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="e">L'entité du composant</param>
        public Component(Entity e)
        {
            this.entity = e;
        }
    }
}
