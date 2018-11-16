using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Missiles
{
    class PlayerMissile:Missile
    {
        public PlayerMissile(Vecteur2D origin) : base(origin,Image.FromFile("../../Resources/shoot2.png"))
        {
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            startPos.Position = new Vecteur2D();
        }
    }
}
