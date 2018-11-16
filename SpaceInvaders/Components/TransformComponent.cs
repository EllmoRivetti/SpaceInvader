using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Components
{
    class TransformComponent:Component
    {
        private Vecteur2D position;
        public Vecteur2D Position
        {
            get
            {
                return position; 
            }

            set
            {
                position = value; 
                RenderComponent r = ((RenderComponent)entity.GetComponent(typeof(RenderComponent)));
                r.view = position;
            }
        }
        public Vecteur2D LocalScale;

        public TransformComponent(Entity e) : base(e)
        {
            position = new Vecteur2D();
            LocalScale = new Vecteur2D();
        }

        public override string ToString()
        {
            return "Position x: "+Position.x+" y: "+Position.y;
        }

    }
}
