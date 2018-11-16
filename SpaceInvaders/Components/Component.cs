using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Components
{
    abstract class Component
    {
        public Entity entity;

        public Component(Entity e)
        {
            this.entity = e;
        }
    }
}
