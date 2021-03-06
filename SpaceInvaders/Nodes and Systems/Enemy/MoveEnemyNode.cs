﻿using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.Ennemy
{
    class MoveEnemyNode : Node
    {
        public PositionComponent TransformComponent { get; set; }
        public VelocityComponent VelocityComponent { get; set; }
        public RenderComponent RenderComponent { get; set; }

        public bool toLeft { get; set; }

        public MoveEnemyNode(Entity e)
        {
            toLeft = true;
            TransformComponent = (PositionComponent)e.GetComponent(typeof(PositionComponent));
            VelocityComponent = (VelocityComponent)e.GetComponent(typeof(VelocityComponent));
            RenderComponent = (RenderComponent)e.GetComponent(typeof(RenderComponent));
        }

        public new static bool ToCreate(Entity e) => e is Enemy;
    }
}
