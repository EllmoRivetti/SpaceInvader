using SpaceInvaders.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Entities
{
    abstract class Entity
    {

        public Dictionary<Type, Component> ListComp { get; private set;  }

        public Entity()
        {
            ListComp = new Dictionary<Type, Component>();
            AddComponent(new PositionComponent(this));
        }
        
        public void AddComponent(Component c)
        {
            ListComp.Add(c.GetType(),c);
        }

        public void RemoveComponent(Type componentType)
        {
            ListComp.Remove(componentType.GetType());
        }

        public Component GetComponent(Type componentType)
        {
            return ListComp[componentType];
        }
    }
}
