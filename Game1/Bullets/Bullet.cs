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
        private Entity owner;

        public bool Dead { get; set; }

        public Bullet(Entity shooter) : base(Globals.bullet, shooter.GetPosition(), 10, new Rectangle(0, 0, 10, 5))
        {
            this.owner = shooter;
            this.speed = 1600;
            rotation = shooter.GetRotation();
            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            position = shooter.GetPosition() + (direction * shooter.GetMaxRadius());
        }

        public override bool Blocks(GameObject other)
        {
            if (other is Tile)
            {
                Tile t = other as Tile;
                if (t.GetTileType() == ETileType.CRATE)
                    return false;
            }
            return true;
        }

        public override void Update(float delta)
        {
            time += delta;

            // remove after time T
            if (time > 1f || speed < 100)
            {
                Dead = true;
            }


            position += direction * speed * delta;
            base.Update(delta);
        }

        public float GetTime() { return time;  }

        public void Kill(GameObject obj)
        {
            if (obj != owner)
            {
                if ( obj is Tile)
                {
                    Tile t = obj as Tile;
                    if (t.GetTileType() == ETileType.CRATE)
                    {
                        color = Color.Blue;
                        speed *= 0.8f;
                        return;
                    }
                }

                Dead = true;

            }
        }
    }
}
