﻿using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes
{
    class MovePlayerNode : Node
    {  
        public TransformComponent TransformComponent { get; set; }
        public VelocityComponent VelocityComponent { get; set; }
        public RenderComponent RenderComponent { get; set; }

        public MovePlayerNode(Entity e)
        {
            TransformComponent = (TransformComponent)e.GetComponent(typeof(TransformComponent));
            VelocityComponent = (VelocityComponent)e.GetComponent(typeof(VelocityComponent));
            RenderComponent = (RenderComponent)e.GetComponent(typeof(RenderComponent));
        }

        public new static bool ToCreate(Entity e) => e.GetType() == typeof(Player);
    }  
}
