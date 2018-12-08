using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Nodes
{
    abstract class Node
    {

        public Node(){}


        public static bool ToCreate(Entity e)
        {
            throw new Exception("The children function must be called instead !");
        }

    }
}
