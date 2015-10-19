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
        Hud hud;
        int frame;

        public Aim(Player player, Hud hud)
        {
            this.player = player;
            this.hud = hud;
        }

        public void Update(float delta)
        {
            position = hud.WorldToHudPosition(player.PosAim);
        }

        public void setAimCooldown(float cooldown)
        {
            frame = (int) (cooldown * 15.0f) + 1;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Globals.aim, position, new Rectangle(16 * 32, 0, 32, 32), Color.FromNonPremultiplied(new Vector4(0.3f, 0.1f, 0.1f, 0.9f)), 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 1f);
            batch.Draw(Globals.aim, position, new Rectangle(32 * frame, 0, 32, 32), Color.Red, 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 1f);
            batch.Draw(Globals.aim, position, new Rectangle(0 * 32, 0, 32, 32), Color.Red, 0f, new Vector2(16, 16), 1f, SpriteEffects.None, 1f);
        }
    }
}
