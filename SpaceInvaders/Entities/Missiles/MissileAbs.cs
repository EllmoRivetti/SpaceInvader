﻿using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    abstract class MissileAbs:Kinematic
    {
        public MissileAbs(Vecteur2D origin, Image img) : base(img)
        {
            TransformComponent startPos = GetComponent(typeof(TransformComponent)) as TransformComponent;
            VelocityComponent velocity = GetComponent(typeof(VelocityComponent)) as VelocityComponent;
        }
    }
}