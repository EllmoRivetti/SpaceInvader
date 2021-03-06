﻿using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities.Missiles
{
    class PlayerMissile:MissileAbs
    {
        public PlayerMissile(Vecteur2D origin) : base(origin,Image.FromFile("../../Resources/shoot2.png"), CollisionTag.PLAYERMISSILE)
        {
            PositionComponent startPos = GetComponent(typeof(PositionComponent)) as PositionComponent;
            startPos.Position = origin;
        }
    }
}
