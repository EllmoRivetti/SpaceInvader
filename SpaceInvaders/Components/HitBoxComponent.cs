using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SpaceInvaders.Entities.Collidable;

namespace SpaceInvaders.Components
{
    class HitBoxComponent : Component
    {
        public Box HitBox { get; set; }
        public CollisionTag tag { get; set; }
        public HitBoxComponent(Entity e, CollisionTag tag) : base(e)
        {
            this.HitBox = new Box();
            this.tag = tag;
        }

        public HitBoxComponent(Entity e, CollisionTag tag, Vecteur2D origin, double width, double height) : base(e)
        {
            HitBox = new Box(origin, width, height);
            this.tag = tag;
        }

        public HitBoxComponent(Entity e, CollisionTag tag, double x, double y, double width, double height) : base(e)
        {
            HitBox = new Box(x,y,width,height);
            this.tag = tag;
        }

        public void CreateHitBox(Vecteur2D origin, double width, double height)
        {
            CreateHitBox(origin.x, origin.y, width, height);
        }

        public void CreateHitBox(double x, double y, double width, double height)
        {
            this.HitBox.X = x;
            this.HitBox.Y = y;
            this.HitBox.XPlusWidth = x + width;
            this.HitBox.YPlusHeight = y + height;
        }
    }

    class Box
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double XPlusWidth { get; set; }
        public double YPlusHeight { get; set; }

        public Box()
        {
            X = 0;
            Y = 0;
            XPlusWidth = 0;
            YPlusHeight = 0;
        }

        public Box(Vecteur2D origin, double width, double height) : this(origin.x, origin.y, width, height){}

        public Box(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.XPlusWidth = x + width;
            this.YPlusHeight = y + height;
        }
        public bool Collides(Box secondBox)
        {
            return !(this.X > secondBox.XPlusWidth || this.XPlusWidth > secondBox.X ||
                     this.Y > secondBox.YPlusHeight || this.YPlusHeight > secondBox.Y);       
        }
    }


}
