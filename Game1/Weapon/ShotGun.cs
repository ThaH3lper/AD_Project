using Game1.Scene;
using Microsoft.Xna.Framework;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class ShotGun : Weapon
    {
        public ShotGun(SimulationWorld world, Entity owner) : base(world, owner)
        {
            cooldown = 0.5f;
            damage = 40.0f;
        }

        public override bool Fire()
        {
            if(base.Fire())
            {
                world.SpawnBullet(owner, 0, damage, size, owner.GetColor());
                world.SpawnBullet(owner, ((float)Math.PI / 180 * 10), damage, size, owner.GetColor());
                world.SpawnBullet(owner, ((float)Math.PI / 180 * -10), damage, size, owner.GetColor());
            }
            return true;
        }
    }
}
