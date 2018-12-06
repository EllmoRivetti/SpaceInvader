using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Components
{
    /// <summary>
    /// Composant permetant l'affichage
    /// </summary>
    class RenderComponent:Component
    {
        /// <summary>
        /// La sprite
        /// </summary>
        public Image sprite { get; set; }

        /// <summary>
        /// La position de la sprite
        /// </summary>
        public Vecteur2D view { get; set; }

        /// <summary>
        /// La BitMap représentant l'image
        /// </summary>
        public Bitmap Image { get; internal set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="e">L'entité du composant</param>
        /// <param name="a">L'image à afficher</param>
        public RenderComponent(Entity e, Image a) : base(e)
        {
            sprite = a;
            if(a == null)
            {
                throw new Exception("image is null");
            }
            view = new Vecteur2D();
        }
    }
}
