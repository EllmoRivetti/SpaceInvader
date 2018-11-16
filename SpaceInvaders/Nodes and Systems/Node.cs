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
        private Dictionary<Type,Component> ListComponent;

        public Node()
        {
            ListComponent = new Dictionary<Type, Component>();
        }

        public void AddComponent(Component c)
        {
            ListComponent.Add(c.GetType(), c);
        }

        public void RemoveComponent(Type typeC)
        {
            ListComponent.Remove(typeC);
        }

        public Component GetComponent(Type componentType)
        {
            return ListComponent[componentType];
        }

        public static bool ToCreate(Entity e)
        {
            throw new Exception("The children function must be called instead !");
        }

    }
}
