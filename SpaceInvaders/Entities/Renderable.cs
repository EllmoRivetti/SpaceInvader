using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class Renderable:Entity
    {
        public Renderable(Image image) : base()
        {
            AddComponent(new RenderComponent(this,image));
        }

        public void Render(Graphics g)
        {
            RenderComponent renderComponent = ((RenderComponent)GetComponent(typeof(RenderComponent)));
            //g.DrawImage(renderComponent.sprite, (float)renderComponent.view.x, (float)renderComponent.view.y);
            g.DrawImage(renderComponent.sprite, (float)renderComponent.view.x, (float)renderComponent.view.y, (float)renderComponent.sprite.Width, (float)renderComponent.sprite.Height);
        }

    }
}
