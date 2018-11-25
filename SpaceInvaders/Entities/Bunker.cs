using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class Bunker : Renderable
    {
        public Bunker(Vecteur2D position) : base(Image.FromFile("../../Resources/bunker.png"))
        {
            TransformComponent BunkerPosition = GetComponent(typeof(TransformComponent)) as TransformComponent;
            BunkerPosition.Position = position;
        }

        public Bunker() : base(Image.FromFile("../../Resources/bunker.png"))
        {
            TransformComponent BunkerPosition = GetComponent(typeof(TransformComponent)) as TransformComponent;
            BunkerPosition.Position.x = RenderForm.instance.Size.Width * 2 / 3;
            BunkerPosition.Position.y = RenderForm.instance.Size.Height * 3 / 5;
        }
    }
}
