using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class Entity : GameObject
    {
        protected Map map;
        protected Vector2 posAim;

        public Entity(Vector2 position, float speed, int size, Map map) : base(Globals.player, position, speed, size, new Rectangle(0, 0, 64, 64))
        {
            this.map = map;
            posAim = position;
        }

        public override void Update(float delta)
        {
            VerticalMove(delta);
            HorizontalMove(delta);

            rotation = (float) Math.Atan2(posAim.Y - position.Y, posAim.X - position.X);

            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);
        }

        public void Move(Vector2 direction)
        {
            this.direction = direction;
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }

        public void VerticalMove(float delta)
        {
            position.Y += direction.Y * speed * delta;
            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);

            Tile colliding = map.GetCollidingTile(recHit);
            if (colliding == null)
                return;
            Rectangle rec = colliding.GetRecHit();
            if (direction.Y > 0)
                position.Y = rec.Y - originHit.Y;
            else if (direction.Y < 0)
                position.Y = rec.Y + rec.Height + originHit.Y;
        }
        public void HorizontalMove(float delta)
        {
            position.X += direction.X * speed * delta;
            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);

            Tile colliding = map.GetCollidingTile(recHit);
            if (colliding == null)
                return;
            Rectangle rec = colliding.GetRecHit();
            if (direction.X > 0)
                position.X = rec.X - originHit.X;
            else if (direction.X < 0)
                position.X = rec.X + rec.Width + originHit.X;
        }
    }
}
