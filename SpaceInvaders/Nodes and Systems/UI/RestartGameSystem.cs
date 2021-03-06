﻿using SpaceInvaders.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.Nodes_and_Systems.UI
{
    class RestartGameSystem : ISystem
    {
        public void Update(double time)
        {
            if (Engine.instance.keyPressed.Contains(Keys.R))
            {
                Engine.instance.ReinitGame();
            }
        }
    }
}
