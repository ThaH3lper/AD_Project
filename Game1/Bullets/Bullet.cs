using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class Bullet
    {
        private static Rectangle recDraw = new Rectangle(0, 0, 10, 5);
        private static float SPEED = 1600;
        float rotation, scale;
        Texture2D texture;
        Vector2 position, origin, direction;
        
        public Bullet(Entity shooter)
        {
            rotation = shooter.GetRotation();
            scale = shooter.GetScale();
            position = shooter.GetPosition();
            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            origin = new Vector2(recDraw.Width / 2, recDraw.Height / 2);
            texture = Globals.bullet;
        }

        public void Update(float delta)
        {
            position += direction * SPEED * delta;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, recDraw, Color.Yellow, rotation, origin, scale, SpriteEffects.None, 1f);
        }
    }
}
