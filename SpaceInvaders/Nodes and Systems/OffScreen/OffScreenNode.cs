using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes_and_Systems.OffScreen
{
    class OffScreenNode : Node
    {
        public RenderComponent RenderComponent { get; set; }

        public OffScreenNode(Entity e)
        {
            RenderComponent = (RenderComponent)e.GetComponent(typeof(RenderComponent));
        }

        public new static bool ToCreate(Entity e) => e is MissileAbs;
    }
}
