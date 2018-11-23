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
            VelocityComponent velocity = GetComponent(typeof(VelocityComponent)) as VelocityComponent;
            velocity.Velocity.x = 50;
            ShootComponent shootStat = new ShootComponent(this,0.2,1);
            AddComponent(shootStat);
        }

        public void SetStartPos(double x , double y)
        {
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            startPos.Position.y = y;
            startPos.Position.x = x;
        }

    }
}
