using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SpaceInvaders.Entities;
using SpaceInvaders.Components;

namespace SpaceInvaders
{
    public partial class RenderForm : Form
    {

        public static RenderForm instance;

        private Game game;

        Stopwatch watch = new Stopwatch();

        private System.Windows.Forms.Timer WorldClock;

        private System.ComponentModel.IContainer components = null;

        long lastTime = 0;

        private RenderForm()
        {
            InitializeComponent();
            game = Game.CreateGame(this.ClientSize);
            watch.Start();
            WorldClock.Start();
        }

        public static RenderForm CreateRenderForm()
        {
            if (instance == null)
                instance = new RenderForm();//TODO Systeme de scene pour gerer la création des entités
            Engine.instance.InstantiateGame();

            return instance;
        }

        /// <summary>
        /// Tick event => update game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldClock_Tick(object sender, EventArgs e)
        {
            // lets do 5 ms update to avoid quantum effects
            int maxDelta = 5;

            // get time with millisecond precision
            long nt = watch.ElapsedMilliseconds;
            // compute ellapsed time since last call to update
            double deltaT = (nt - lastTime);

            for (; deltaT >= maxDelta; deltaT -= maxDelta)
                game.Update(maxDelta / 1000.0);

            // remember the time of this update
            lastTime = nt;

            Invalidate();

        }

        internal bool IsObjectOnScreen(RenderComponent rc)
        {
            return !(rc.view.x > instance.Size.Width - rc.sprite.Width || rc.view.y > instance.Size.Height - rc.sprite.Height || rc.view.x < 0 || rc.view.y < 0);
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenderForm));
            this.WorldClock = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // WorldClock
            // 
            this.WorldClock.Interval = 30;
            this.WorldClock.Tick += new System.EventHandler(this.WorldClock_Tick);
            // 
            // RenderForm
            // 
            this.ClientSize = new System.Drawing.Size(605, 605);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenderForm";
            this.Text = "Space Invader";
            this.Load += new System.EventHandler(this.RenderForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenderForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RenderForm_KeyUp);
            this.ResumeLayout(false);

        }

        private void RenderForm_Load(object sender, EventArgs e)
        {

        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            bg.Graphics.Clear(Color.White);

            Game.game.Render(bg.Graphics);

            bg.Render();
            bg.Dispose();
        }

        private void RenderForm_KeyUp(object sender, KeyEventArgs e)
        {
            Engine.instance.keyPressed.Remove(e.KeyCode);
        }

        private void RenderForm_KeyDown(object sender, KeyEventArgs e)
        {
            Engine.instance.keyPressed.Add(e.KeyCode);
        }
    }
}
