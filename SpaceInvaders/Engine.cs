using SpaceInvaders.Components;
using SpaceInvaders.Entities;
using SpaceInvaders.Nodes;
using SpaceInvaders.Nodes_and_Systems.Collision;
using SpaceInvaders.Nodes_and_Systems.Ennemy;
using SpaceInvaders.Nodes_and_Systems.GameManagement;
using SpaceInvaders.Nodes_and_Systems.Missile;
using SpaceInvaders.Nodes_and_Systems.OffScreen;
using SpaceInvaders.Nodes_and_Systems.Player;
using SpaceInvaders.Nodes_and_Systems.Shoot;
using SpaceInvaders.Nodes_and_Systems.UI;
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
        /// <summary>
        /// Le singleton de l'Engine
        /// </summary>
        public static Engine instance { get; set; }

        /// <summary>
        /// La liste de système de bases
        /// </summary>
        private List<ISystem> SystemsList { get; }

        /// <summary>
        /// List pour les systèmes en rapport avec les interactions utilisateurs (ici la pause)
        /// </summary>
        private List<ISystem> UISystemsList { get; }

        /// <summary>
        /// Liste des systèmes se déroulant en fin de jeu (ex: le système pour relancer le jeu)
        /// </summary>
        private List<ISystem> EndGameSystemsList { get; }

        /// <summary>
        /// Liste des entitées présentes dans le jeu
        /// </summary>
        public List<Entity> EntitiesList { set; get; }

        /// <summary>
        /// Permet de liste les node selon leur type
        /// </summary>
        public Dictionary<Type, List<Node>> NodeListByType;

        /// <summary>
        /// permet de lister les nodes selon les entitées
        /// </summary>
        private Dictionary<Entity, List<Node>> NodeListByEntity;

        /// <summary>
        /// Tous les types de nodes qui existent
        /// </summary>
        private IEnumerable<Type> nodeTypes;

        /// <summary>
        /// Set permettant de connaitre quelles sont les touches que le joueur active
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        /// <summary>
        /// permet de savoir si le jeu est en pause ou non
        /// </summary>
        public bool IsPaused { get; set; }

        /// <summary>
        /// Permet de savoir si le joueur a gagné
        /// </summary>
        public bool IsVictory { get; set; }

        /// <summary>
        /// Permet de savoir si le joueur a perdu
        /// </summary>
        public bool IsDefeat { get; set; }

        /// <summary>
        /// Le constructeur de la classe Engine. Instancie toutes les listes et variables nécessaires au bon fonctionnement du jeu. 
        /// </summary>
        private Engine()
        {
            SystemsList = new List<ISystem>();
            UISystemsList = new List<ISystem>();
            EndGameSystemsList = new List<ISystem>();
            EntitiesList = new List<Entity>();
            NodeListByType = new Dictionary<Type, List<Node>>();
            NodeListByEntity = new Dictionary<Entity, List<Node>>();

            nodeTypes = typeof(Node)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Node)));

            IsPaused = false;
            IsDefeat = false;
            IsVictory = false;

            foreach(Type t in nodeTypes)
            {
                NodeListByType[t] = new List<Node>();
            }

            //Ici on créé tous les systems
            AddSystems();

            //TODO Gérer les entities selon les scènes
        }

        /// <summary>
        /// Permet de créer le singleton de l'engine
        /// </summary>
        /// <returns>
        /// Retourne l'instance du singleton
        /// </returns>
        public static Engine CreateEngine()
        {
            if(instance == null)
                instance = new Engine();
            return instance;
        }


        /// <summary>
        /// Ajoute les entitées nécessaire pour le jeu
        /// </summary>
        public void InstantiateGame()
        {
            Engine.instance.AddEntity(new Player());
            Engine.instance.AddEntity(new EnemyBlock());
            Engine.instance.AddEntity(new Bunker(new Vecteur2D(((RenderForm.instance.Size.Width - (87 * 3)) / 4) * 1, RenderForm.instance.Size.Height * 4.6 / 6)));
            Engine.instance.AddEntity(new Bunker(new Vecteur2D(((RenderForm.instance.Size.Width - (87 * 3)) / 4) * 2.9, RenderForm.instance.Size.Height * 4.6 / 6)));
            Engine.instance.AddEntity(new Bunker(new Vecteur2D(((RenderForm.instance.Size.Width - (87 * 3)) / 4) * 4.7, RenderForm.instance.Size.Height * 4.6 / 6)));
        }

        /// <summary>
        /// Reinitialise le jeu. Reinitialise les listes et variables puis appel une fonction pour re-creer le jeu
        /// </summary>
        public void ReinitGame()
        {
            RemoveAllEntities();
            EntitiesList = new List<Entity>();
            IsPaused = false;
            IsDefeat = false;
            IsVictory = false;

            InstantiateGame();
        }

        /// <summary>
        /// Supprime toutes les entitées de la liste d'entitées
        /// </summary>
        public void RemoveAllEntities()
        {
            for(int i = EntitiesList.Count - 1; i >= 0; i--)
            {
                RemoveEntity(EntitiesList[i]);
            }
        }

        /// <summary>
        /// Ajoute une entité à la liste d'entitées puis créer les nodes qui sont nécessaire selon le type de l'entité
        /// </summary>
        /// <param name="e">
        /// L'entité qui va être ajoutée dans la liste
        /// </param>
        public void AddEntity(Entity e)
        {
            EntitiesList.Add(e);
            NodeListByEntity[e] = new List<Node>();
            foreach (Type nType in nodeTypes)
            {
               
                Entity[] currentEntity = { e };
                if ((bool)nType.GetMethod("ToCreate").Invoke(null, currentEntity))
                {
                    Type[] currentType = {typeof(Entity)};
                    Node node = (Node)nType.GetConstructor(currentType).Invoke(currentEntity);
                    NodeListByType[nType].Add(node);
                    NodeListByEntity[e].Add(node);
                }

            }
        }

        /// <summary>
        /// Supprime une entitée de la liste d'entitées
        /// </summary>
        /// <param name="e">
        /// L'entité qui va être enlevée dans la liste
        /// </param>
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

        /// <summary>
        /// Ajout de tous les systèmes nécessaires au bon fonctionnement du jeu 
        /// </summary>
        private void AddSystems()
        {
            AddSystem(new MovePlayerSystem());
            AddSystem(new MoveEnemySystem());
            AddSystem(new MoveMissileSystem());
            AddSystem(new ShootPlayerSystem());
            AddSystem(new ShootEnemySystem());
            AddSystem(new CollisionSystem());
            AddSystem(new OffScreenSystem());
            AddSystem(new CheckEndGameSystem());
            AddSystem(new SetPauseSystem());
            AddUISystem(new ReLaunchGameSystem());
            AddEndGameSystem(new RestartGameSystem());
        }

        /// <summary>
        /// Ajoute un système a la liste des systèmes
        /// </summary>
        /// <param name="s">Le système qui va etre ajouté dans la liste</param>
        private void AddSystem(ISystem s)
        {
            SystemsList.Add(s);
        }

        /// <summary>
        /// Ajoute le système de type UI (User Interface) qui va etre ajouté a la liste des systèmes d'UI
        /// </summary>
        /// <param name="s">Le système qui va etre ajouté dans la liste</param>
        private void AddUISystem(ISystem s)
        {
            UISystemsList.Add(s);
        }

        /// <summary>
        /// Ajoute le système de type EndGame qui va etre ajouté a la liste des systèmes de fin de jeu
        /// </summary>
        /// <param name="s">Le système qui va etre ajouté dans la liste</param>
        private void AddEndGameSystem(ISystem s)
        {
            EndGameSystemsList.Add(s);
        }

        /// <summary>
        /// Supprime un système de la liste des systèmes
        /// </summary>
        /// <param name="s">Le système qui va etre supprimé dans la liste</param>
        private void RemoveSystem(ISystem s)
        {
            SystemsList.Remove(s);
        }


        /// <summary>
        /// Cette fonction permet de mettre a jour le jeu. Elle va appeler la fonction Update() de tous les système présent dans la liste des système.
        /// La liste qui est parcourue peut changer selon l'etat du jeu. Des booléens nous permette de savoir l'état du jeu. 
        /// Si le jeu est en pause, la liste parcourue sera la liste de UI Systems
        /// Si le jeu est fini, la liste parcourue sera la liste de EndGame Systems
        /// </summary>
        /// <param name="time">
        /// La variable de temps
        /// </param>
        public void Update(double time)
        {

            if (IsPaused)
            {
                foreach (ISystem s in UISystemsList)
                {
                    s.Update(time);
                }
            }
            else
            {
                if (IsVictory || IsDefeat)
                {
                    foreach (ISystem s in EndGameSystemsList)
                    {
                        s.Update(time);
                    }
                }
                else
                {
                    foreach (ISystem s in SystemsList)
                    {
                        s.Update(time);
                    }
                }
            }
        }

        /// <summary>
        /// Permet d'afficher tous les différents composants du jeu
        /// </summary>
        /// <param name="g">
        /// L'objet graphics permettant de dessiner
        /// </param>
        public void Render(Graphics g)
        {
            foreach(Entity e in EntitiesList)
            {
                var entity = e as Renderable;
                if (entity != null)
                    ((Renderable)e).Render(g);
            }
            DrawPlayerLife(g);

            if (IsDefeat)
            {
                DrawDefeatScreen(g);
            }

            if (IsVictory)
            {
                DrawVictoryScreen(g);
            }

            if (IsPaused)
            {
                DrawPauseScreen(g);
            }

            bool showHitBox = false;//Mettre a true pour afficher les hitbox des entities
            if (showHitBox)
            {
                ShowHitBox(g);
            }
        }

        /// <summary>
        /// Permet d'afficher les hitbox des entitées (grace a un rectangle rouge)
        /// </summary>
        /// <param name="g">
        /// L'objet graphics permettant de dessiner
        /// </param>
        public void ShowHitBox(Graphics g)
        {
            foreach (CollisionNode col in Engine.instance.NodeListByType[typeof(CollisionNode)])
            {
                Box box = col.HitBoxComponent.HitBox;
                Rectangle rect = new Rectangle((int)box.X, (int)box.Y, (int)(box.XPlusWidth - box.X), (int)(box.YPlusHeight - box.Y));
                Pen redPen = new Pen(Color.Red)
                {
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
                };
                g.DrawRectangle(redPen, rect);
            }   
        }

        /// <summary>
        /// Permet d'afficher l'ecran de défaite
        /// </summary>
        /// <param name="g">
        /// L'objet graphics permettant de dessiner
        /// </param>
        public void DrawDefeatScreen(Graphics g)
        {
            Rectangle rect = new Rectangle(RenderForm.instance.Width / 5, RenderForm.instance.Height / 5, RenderForm.instance.Width / 5 * 3, RenderForm.instance.Width / 5);
            Pen pen = new Pen(Color.Black)
            {
                Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
            };
            g.DrawRectangle(pen, rect);
            g.FillRectangle(new SolidBrush(Color.FromArgb(247, 247, 247, 255)), rect);
            g.DrawString("You Lost ...", new System.Drawing.Font("Arial", 24), new System.Drawing.SolidBrush(System.Drawing.Color.Black), rect.Width / 3 * 2 - 20, rect.Height / 2 * 3 - 20);
            g.DrawString("Press R to restart", new System.Drawing.Font("Arial", 16), new System.Drawing.SolidBrush(System.Drawing.Color.Black), rect.Width / 3 * 2 - 30, rect.Height / 2 * 3 + 10);
        }


        /// <summary>
        /// Permet d'afficher l'écran de victoire
        /// </summary>
        /// <param name="g">
        /// L'objet graphics permettant de dessiner
        /// </param>
        public void DrawVictoryScreen(Graphics g)
        {
            Rectangle rect = new Rectangle(RenderForm.instance.Width / 5, RenderForm.instance.Height / 5, RenderForm.instance.Width / 5 * 3, RenderForm.instance.Width / 5);
            Pen pen = new Pen(Color.Black)
            {
                Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
            };
            g.DrawRectangle(pen, rect);
            g.FillRectangle(new SolidBrush(Color.FromArgb(247, 247, 247, 255)), rect);
            g.DrawString("You Won !", new System.Drawing.Font("Arial", 24), new System.Drawing.SolidBrush(System.Drawing.Color.Black), rect.Width / 3 * 2 - 20, rect.Height / 2 * 3 - 20);
            g.DrawString("Press R to restart", new System.Drawing.Font("Arial", 16), new System.Drawing.SolidBrush(System.Drawing.Color.Black), rect.Width / 3 * 2 - 30, rect.Height / 2 * 3 + 10);
        }

        /// <summary>
        /// Permet d'afficher l'ecran de pause
        /// </summary>
        /// <param name="g">
        /// L'objet graphics permettant de dessiner
        /// </param>
        public void DrawPauseScreen(Graphics g)
        {
            Rectangle rect = new Rectangle(RenderForm.instance.Width / 5, RenderForm.instance.Height / 5, RenderForm.instance.Width / 5 * 3, RenderForm.instance.Width / 5);
            Pen pen = new Pen(Color.Black)
            {
                Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
            };
            g.DrawRectangle(pen, rect);
            g.FillRectangle(new SolidBrush(Color.FromArgb(247, 247, 247, 255)), rect);
            g.DrawString("The game is paused", new System.Drawing.Font("Arial", 24), new System.Drawing.SolidBrush(System.Drawing.Color.Black), rect.Width / 3 * 2 - 90, rect.Height / 2 * 3 - 20);
            g.DrawString("Press ESCAPE to resume", new System.Drawing.Font("Arial", 16), new System.Drawing.SolidBrush(System.Drawing.Color.Black), rect.Width / 3 * 2 - 70, rect.Height / 2 * 3 + 20);
        }

        /// <summary>
        /// Permet d'afficher la vie du joueur
        /// </summary>
        /// <param name="g">
        /// L'objet graphics permettant de dessiner
        /// </param>
        public void DrawPlayerLife(Graphics g)
        {
            g.DrawString("Life : " + ((Player)EntitiesList[0]).Life, new System.Drawing.Font("Arial", 16), new System.Drawing.SolidBrush(System.Drawing.Color.Black), 10, RenderForm.instance.Height - 75);
        }
    }
}
