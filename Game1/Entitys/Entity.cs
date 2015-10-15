using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    public class Entity : GameObject
    {
        protected SimulationWorld world;
        protected Weapon weapon;

        public Entity(Texture2D texture, Vector2 position, float speed, int size, SimulationWorld world) : base(texture, position, speed, size, new Rectangle(0, 0, 64, 64))
        {
            this.world = world;
            this.weapon = new Weapon(world, this);
        }

        public override void Update(float delta)
        {
            VerticalMove(delta);
            HorizontalMove(delta);
            weapon.Update(delta);

            base.Update(delta);
        }

        public void Move(Vector2 direction)
        {
            this.direction = direction;
        }

        public void Fire()
        {
            weapon.Fire();
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }

        public void VerticalMove(float delta)
        {
            position.Y += direction.Y * speed * delta;
            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);

            //Tile colliding = world.Map.GetCollidingTile(recHit);
            //if (colliding == null)
            //    return;

            //Rectangle rec = colliding.GetRecHit();

            var collide = world.GetColliders(this).FirstOrDefault();

            if (collide == null)
                return;

            var rec = collide.GetHitRectangle();

            if (direction.Y > 0)
                position.Y = rec.Y - originHit.Y;
            else if (direction.Y < 0)
                position.Y = rec.Y + rec.Height + originHit.Y;
        }

        public void HorizontalMove(float delta)
        {
            position.X += direction.X * speed * delta;
            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);

            //Tile colliding = world.Map.GetCollidingTile(recHit);
            //if (colliding == null)
            //    return;

            //Rectangle rec = colliding.GetRecHit();

            var collide = world.GetColliders(this).FirstOrDefault();

            if (collide == null)
                return;

            var rec = collide.GetHitRectangle();

            if (direction.X > 0)
                position.X = rec.X - originHit.X;
            else if (direction.X < 0)
                position.X = rec.X + rec.Width + originHit.X;
        }

        public void Face(Vector2 target)
        {
            rotation = (float)Math.Atan2(target.Y - position.Y, target.X - position.X);
        }
    }
}
