using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    public class Bullet : GameObject
    {
        float time, speed;

        public bool Dead { get; set; }

        public Bullet(Entity shooter) : base(Globals.bullet, shooter.GetPosition(), 10, new Rectangle(0, 0, 10, 5))
        {
            this.speed = 1600;
            rotation = shooter.GetRotation();
            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            position = shooter.GetPosition() + (direction * shooter.GetMaxRadius());
        }

        public override void Update(float delta)
        {
            time += delta;

            // remove after time T
            if (time > 1f)
            {
                Dead = true;
            }


            position += direction * speed * delta;
            base.Update(delta);
        }

        public float GetTime() { return time;  }
    }
}
