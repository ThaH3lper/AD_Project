using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Hud
{
    class MainMenu
    {
        Vector2 positionStart, positionBy, positionName, positionExit, origin;
        float rotationStart = -0.4f, rotationBy = 0.5f, rotationName = -0.2f, rotationExit = 0.1f;

        float angle;
        public MainMenu()
        {
            origin = new Vector2(0, Globals.font.MeasureString("Press \"H\"-Key to start").Y/2);
        }

        public void Update(Vector2 position, float delta)
        {
            angle += 7f * delta;
            this.positionStart = position + (new Vector2((float)Math.Cos(rotationStart), (float)Math.Sin(rotationStart)) * (30 + (int)(Math.Sin(angle) * 5)));
            this.positionBy = position + (new Vector2((float)Math.Cos(rotationBy), (float)Math.Sin(rotationBy)) * (-112 - (int)(Math.Sin(angle) * 5)));
            this.positionName = position + (new Vector2((float)Math.Cos(rotationName), (float)Math.Sin(rotationName)) * (-135 - (int)(Math.Sin(angle) * 5)));
            this.positionExit = position + (new Vector2((float)Math.Cos(rotationExit), (float)Math.Sin(rotationExit)) * (30 + (int)(Math.Sin(angle) * 5)));
        }

        public void Render(SpriteBatch batch)
        {
            batch.DrawString(Globals.font, "Press \"Y\"-Key to Start", positionStart, Color.Black, rotationStart, origin, 1f, SpriteEffects.None, 1f);
            batch.DrawString(Globals.font, "Press \"N\"-Key to Exit", positionExit, Color.Black, rotationExit, origin, 1f, SpriteEffects.None, 1f);
            batch.DrawString(Globals.font, "A Game By:", positionBy, Color.Black, rotationBy, origin, 1f, SpriteEffects.None, 1f);
            batch.DrawString(Globals.font, "Patrik & Simon", positionName, Color.Black, rotationName, origin, 1f, SpriteEffects.None, 1f);
        }
    }
}
