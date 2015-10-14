using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class Aim
    {
        Player player;
        Vector2 position;

        public Aim(Player player)
        {
            this.player = player;
        }

        public void Update(float delta)
        {
            position = player.PosAim;
        }

        public void Draw(SpriteBatch batch)
        {
            //batch.Draw()
        }
    }
}
