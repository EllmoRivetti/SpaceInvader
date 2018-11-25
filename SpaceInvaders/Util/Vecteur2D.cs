using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        public double x { set; get; }

        public double y { set; get; }

        public double norme
        {
            get
            {
                return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            }
        }

        public Vecteur2D(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;

        }

        public static Vecteur2D operator +(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vecteur2D operator -(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vecteur2D operator -(Vecteur2D v1)
        {
            return v1 * -1;
        }


        public static Vecteur2D operator *(Vecteur2D v, double value)
        {
            return new Vecteur2D(v.x * value, v.y * value);
        }

        public static Vecteur2D operator *(Vecteur2D v, Vecteur2D v2)
        {
            return new Vecteur2D(v.x * v2.x, v.y * v2.y);
        }

        public static Vecteur2D operator *(double value, Vecteur2D v)
        {
            return v * value;
        }



        public static Vecteur2D operator /(Vecteur2D v, Double value)
        {
            return new Vecteur2D(v.x / value, v.y / value);
        }


    }
}