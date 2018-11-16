using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Components
{
    class RenderComponent:Component
    {
        public Image sprite { get; set; }
        public Vecteur2D view { get; set; }

        public RenderComponent(Entity e, Image a) : base(e)
        {
            sprite = a;
            view = new Vecteur2D();
        }
    }
}
