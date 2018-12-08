using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Components
{
    class PositionComponent:Component
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
                try
                {
                    
                    RenderComponent r = ((RenderComponent)entity.GetComponent(typeof(RenderComponent)));
                    r.view = position;
                    try
                    {
                        HitBoxComponent h = ((HitBoxComponent)entity.GetComponent(typeof(HitBoxComponent)));
                        h.Update(position.x,position.y,r.sprite.Width,r.sprite.Height);
                        //Console.WriteLine(h.HitBox.ToString());
                    }
                    catch { }
                }
                catch{ }
                
            }
        }
        public Vecteur2D LocalScale;

        public PositionComponent(Entity e) : base(e)
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
