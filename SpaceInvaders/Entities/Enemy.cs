using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class Enemy:Kinematic
    {
        public Enemy() : base(Image.FromFile("../../Resources/ship2.png"))
        {
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            startPos.Position.y = RenderForm.instance.Size.Height * 2 / 5;
            startPos.Position.x = RenderForm.instance.Size.Width / 2;
            VelocityComponent velocity = GetComponent(typeof(VelocityComponent)) as VelocityComponent;
            velocity.Velocity.x = 500;
        }
    }
}
