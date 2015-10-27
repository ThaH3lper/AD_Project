using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Entitys.Powers
{
    public class BasePower : GameObject
    {
        protected SimulationWorld world;

        public BasePower(Texture2D texture, Vector2 position, int size, SimulationWorld world, Rectangle bounds) : base(texture, position, size, bounds)
        {
            this.world = world;
        }

        public virtual void onPickup(Entity e)
        {
        }
    }
}
