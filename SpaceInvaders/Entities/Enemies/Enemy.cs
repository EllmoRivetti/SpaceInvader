using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    abstract class Enemy:Collidable
    {

        public Enemy(Image image) : base(image, CollisionTag.ENEMY)
        {
            PositionComponent startPos = GetComponent(typeof(PositionComponent)) as PositionComponent;
            VelocityComponent velocity = GetComponent(typeof(VelocityComponent)) as VelocityComponent;
            velocity.Velocity.x = 50;
            ShootComponent shootStat = new ShootComponent(this,0.2,1,0.5);
            AddComponent(shootStat);
        }

        public void SetStartPos(double x , double y)
        {
            PositionComponent startPos = GetComponent(typeof(PositionComponent)) as PositionComponent;
            startPos.Position.y = y;
            startPos.Position.x = x;
        }

        public enum EnemyTag
        {
            ALIEN =0,
            UFO,
            SQUID1,
            SQUID2,
            SQUARE,
            ARMS1,
            ARMS2,
            TELY
        }

    }
}
