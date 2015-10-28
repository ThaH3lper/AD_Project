using Game1.Scene;
using Microsoft.Xna.Framework;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Entitys.Powers
{
    class WeaponCase : BasePower
    {
        public WeaponCase(Vector2 position, int size, SimulationWorld world) : base(Globals.create, position, size, world, new Rectangle(0, 0, 64, 64))
        {
            this.color = Color.Yellow;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            rotation += delta;
        }

        public override void onPickup(Entity e)
        {
            Weapon weapon;
            var rand = new Random();
            switch (rand.Next(0, 4))
            {
                case 0:
                    weapon = new ShotGun(world, e);
                    break;
                case 1:
                    weapon = new Bazooka(world, e);
                    break;
                case 2:
                    weapon = new Rifle(world, e);
                    break;
                default:
                    weapon = new Pistol(world, e);
                    break;
            }

            e.SetWeapon(weapon);
        }
    }
}
