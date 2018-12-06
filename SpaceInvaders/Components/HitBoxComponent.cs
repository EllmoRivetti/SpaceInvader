using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SpaceInvaders.Entities.Collidable;

namespace SpaceInvaders.Components
{
    /// <summary>
    /// Composant permettant de créer des hitboxs
    /// </summary>
    class HitBoxComponent : Component
    {
        /// <summary>
        /// La Box representant la hitbox
        /// </summary>
        public Box HitBox { get; set; }

        /// <summary>
        /// Le tag de collision permettant de savoir a quel type d'entité appartient le composant
        /// </summary>
        public CollisionTag tag { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="e">L'entité du composant</param>
        /// <param name="tag">Le tag de collision</param>
        public HitBoxComponent(Entity e, CollisionTag tag) : base(e)
        {
            this.HitBox = new Box();
            this.tag = tag;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="e">L'entité du composant</param>
        /// <param name="tag">Le tag de collision</param>
        /// <param name="origin">Le vecteur 2D permettant de connaitre le point d'origine de la hitbox </param>
        /// <param name="width">la largeur de la hitbox</param>
        /// <param name="height">la hauteur de la hitbox</param>
        public HitBoxComponent(Entity e, CollisionTag tag, Vecteur2D origin, double width, double height) : base(e)
        {
            HitBox = new Box(origin, width, height);
            //Console.WriteLine(HitBox.ToString());
            this.tag = tag;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="e">L'entité du composant</param>
        /// <param name="tag">Le tag de collision</param>
        /// <param name="x">La coordonnée du point x de l'origine de la hitbox</param>
        /// <param name="y">La coordonnée du point y de l'origine de la hitbox></param>
        ///<param name="width">la largeur de la hitbox</param>
        /// <param name="height">la hauteur de la hitbox</param>
        public HitBoxComponent(Entity e, CollisionTag tag, double x, double y, double width, double height) : base(e)
        {
            HitBox = new Box(x,y,width,height);
            this.tag = tag;
        }

        /// <summary>
        /// Permet de mettre a jour la hitbox
        /// </summary>
        /// <param name="x">Nouvelle coordonnées de X</param>
        /// <param name="y">Nouvelle coordonnées de Y</param>
        /// <param name="width">Nouvelle largeur</param>
        /// <param name="height">Nouvelle hauteur</param>
        public void Update(double x, double y, double width, double height)
        {
            this.HitBox.X = x;
            this.HitBox.Y = y;
            this.HitBox.XPlusWidth = x + width;
            this.HitBox.YPlusHeight = y + height;
        }
    }

    /// <summary>
    /// Classe permettant de représenter un carré grâce à un point ainsi qu'une largeur et une hauteur
    /// </summary>
    class Box
    {
        /// <summary>
        /// Coordonnée X de l'origine
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Coordonnée Y de l'origine
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Coordonnée X de l'origine à laquelle est additioné la largeur de la boite
        /// </summary>
        public double XPlusWidth { get; set; }

        /// <summary>
        /// Coordonnée Y de l'origine à laquelle est additioné la hauteur de la boite
        /// </summary>
        public double YPlusHeight { get; set; }


        /// <summary>
        /// Constructeur
        /// </summary>
        public Box()
        {
            X = 0;
            Y = 0;
            XPlusWidth = 0;
            YPlusHeight = 0;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Box(Vecteur2D origin, double width, double height) : this(origin.x, origin.y, width, height){}


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Box(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.XPlusWidth = x + width;
            this.YPlusHeight = y + height;
        }

        /// <summary>
        /// Permet de savoir si la Box actuelle collide avec une autre box
        /// </summary>
        /// <param name="secondBox">La deuxième box</param>
        /// <returns>Retourne un booleen permetant de savoir si les deux Box sont rentrées en collision</returns>
        public bool Collides(Box secondBox)
        {
            return !((this.X > secondBox.XPlusWidth || this.XPlusWidth < secondBox.X) ||
                     (this.Y > secondBox.YPlusHeight || this.YPlusHeight < secondBox.Y));       
        }


        /// <summary>
        /// Retourne les informations principales de l'entitée
        /// </summary>
        /// <returns>les informations principales de l'entitée</returns>
        public override string ToString()
        {
            return "Box[X: "+X+" | Y: "+Y+" | X + width: "+XPlusWidth+" | Y + heigth: " + YPlusHeight;
        }

    }


}
