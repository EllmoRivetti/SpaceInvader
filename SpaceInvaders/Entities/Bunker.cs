using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class Bunker : Collidable
    {
        public Bunker(Vecteur2D position) : base(Image.FromFile("../../Resources/bunker.png"),CollisionTag.BUNKER)
        {
            PositionComponent BunkerPosition = GetComponent(typeof(PositionComponent)) as PositionComponent;
            BunkerPosition.Position = position;
        }

        public Bunker() : base(Image.FromFile("../../Resources/bunker.png"),CollisionTag.BUNKER)
        {
            PositionComponent BunkerPosition = GetComponent(typeof(PositionComponent)) as PositionComponent;
            BunkerPosition.Position.x = RenderForm.instance.Size.Width * 2 / 3;
            BunkerPosition.Position.y = RenderForm.instance.Size.Height * 3 / 5;
        }
    }
}
