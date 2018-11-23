using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using SpaceInvaders.Nodes_and_Systems.Ennemy;
using SpaceInvaders.Nodes_and_Systems.Missile;
using SpaceInvaders.Nodes_and_Systems.Player;
using SpaceInvaders.Nodes_and_Systems.Shoot;
using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Engine
    {

        public static Engine instance { get; set; }

        private List<ISystem> SystemsList { get; }

        public List<Entity> EntitiesList { get; }

        public Dictionary<Type, List<Node>> NodeListByType;//Type : type des nodes (genre toutes les nodes de render etc ..)

        private Dictionary<Entity, List<Node>> NodeListByEntity;

        private IEnumerable<Type> nodeTypes;

        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        private Engine()
        {
            SystemsList = new List<ISystem>();
            EntitiesList = new List<Entity>();
            NodeListByType = new Dictionary<Type, List<Node>>();
            NodeListByEntity = new Dictionary<Entity, List<Node>>();
            //Instanciate nodeTypes
            nodeTypes = typeof(Node)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Node)));

            foreach(Type t in nodeTypes)
            {
                NodeListByType[t] = new List<Node>();
            }

            //Ici on créé tous les systems
            AddSystems();

            //TODO Gérer les entities selon les scènes
        }


        public static Engine CreateEngine()
        {
            if(instance == null)
                instance = new Engine();
            return instance;
        }



        public void AddEntity(Entity e)
        {
            Console.WriteLine("AJOUT DE L'ENTITY: " + e);
            EntitiesList.Add(e);
            NodeListByEntity[e] = new List<Node>();
            foreach (Type nType in nodeTypes)
            {
               
                Entity[] currentEntity = { e };
                if ((bool)nType.GetMethod("ToCreate").Invoke(null, currentEntity))
                {
                    Console.WriteLine("To create for:" +nType);
                    Type[] currentType = {typeof(Entity)};
                    Node node = (Node)nType.GetConstructor(currentType).Invoke(currentEntity);
                    NodeListByType[nType].Add(node);
                    NodeListByEntity[e].Add(node);
                }

            }
            Console.WriteLine("-------------");
        }

        public void RemoveEntity(Entity e)
        {
            if (EntitiesList.Contains(e))
            {
                EntitiesList.Remove(e);
                foreach (Node node in NodeListByEntity[e])
                {
                    NodeListByType[node.GetType()].Remove(node);
                }
                NodeListByEntity.Remove(e);
            }
        }


        private void AddSystems()
        {
            AddSystem(new MovePlayerSystem());
            AddSystem(new MoveEnemySystem());
            AddSystem(new MoveMissileSystem());
            AddSystem(new ShootPlayerSystem());
            AddSystem(new ShootEnemySystem());
        }

        private void AddSystem(ISystem s)
        {
            SystemsList.Add(s);
        }

        private void RemoveSystem(ISystem s)
        {
            SystemsList.Remove(s);
        }


        public void Update(double time)
        {
            foreach(ISystem s in SystemsList)
            {
                s.Update(time);
            }
        }

        public void Render(Graphics g)
        {
            foreach(Entity e in EntitiesList)
            {
                var entity = e as Renderable;
                if (entity != null)
                    ((Renderable)e).Render(g);
            }
        }


    }
}
