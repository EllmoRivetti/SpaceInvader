using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Components
{
    class VelocityComponent : Component
    {
        public Vecteur2D Velocity { get; set; }

        public VelocityComponent(Entity e) : base(e)
        {
            Velocity = new Vecteur2D();
        }


    }
}
