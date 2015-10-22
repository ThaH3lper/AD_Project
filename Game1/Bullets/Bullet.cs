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
        float time, speed, damage;
        private Entity owner;

        public bool Dead { get; set; }

        public Bullet(Entity shooter, float offsetAngle, float damage, int size, Color color) : base(Globals.bullet, shooter.GetPosition(), size, new Rectangle(0, 0, 10, 5))
        {
            this.owner = shooter;
            this.speed = 800;
            this.damage = damage;
            this.color = color;
            rotation = shooter.GetRotation();
            direction = new Vector2((float)Math.Cos(rotation + offsetAngle), (float)Math.Sin(rotation + offsetAngle));
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

            return owner != other;
        }

        public override void Update(float delta)
        {
            time += delta;

            // remove after time T
            if (time > 10f || speed < 100)
            {
                Dead = true;
            }


            position += direction * speed * delta;
            base.Update(delta);
        }

        public float GetTime() { return time;  }

        public bool CheckKill(GameObject obj)
        {
            if (obj != owner)
            {
                if ( obj is Tile)
                {
                    Tile t = obj as Tile;
                    if (t.GetTileType() == ETileType.CRATE)
                    {
                        color = Color.Blue;
                        speed *= 0.9f;
                        damage *= 0.8f;
                        return true;
                    }
                }

                Dead = true;
                return true;

            }
            return false;
        }

        public float GetDamage() { return damage; }

        public Entity GetOwner() { return owner; }
    }
}
