using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class Kinematic : Renderable
    {
        public Kinematic(Image image) : base(image)
        {
            AddComponent(new VelocityComponent(this));
        }
    }
}
