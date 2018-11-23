using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    class Player:Kinematic
    {
        public Player():base(Image.FromFile("../../Resources/ship1.png"))
        {
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            startPos.Position.y = RenderForm.instance.Size.Height * 4 / 5;
            startPos.Position.x = RenderForm.instance.Size.Width / 2;
            VelocityComponent velocity = GetComponent(typeof(VelocityComponent)) as VelocityComponent;
            velocity.Velocity.x = 500;

            ShootComponent shootStatus = new ShootComponent(this,0.2,0,1);//Le dernier parametre n'est pas nécessaire pour le joueur
            AddComponent(shootStatus);
        }
    }
}
