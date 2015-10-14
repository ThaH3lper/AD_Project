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
        float time;

        public bool Dead { get; private set; }

        public Bullet(Entity shooter) : base(Globals.bullet, shooter.GetPosition(), 1600, 7, new Rectangle(0, 0, 10, 5))
        {
            rotation = shooter.GetRotation();
            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            position = shooter.GetPosition() + (direction * shooter.GetMaxRadius());
        }

        public override void Update(float delta)
        {
            time += delta;

            // remove after time T
            if (time > 0.3f)
            {
                Dead = true;
            }

            position += direction * speed * delta;
            base.Update(delta);
        }

        public float GetTime() { return time;  }
    }
}
