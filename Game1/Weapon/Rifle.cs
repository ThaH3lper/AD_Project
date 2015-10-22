using Game1.Scene;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class Rifle : Weapon
    {
        public Rifle(SimulationWorld world, Entity owner) : base(world, owner)
        {
            cooldown = 0.15f;
            damage = 10f;
        }

        public override bool Fire()
        {
            if(base.Fire())
            {
                world.SpawnBullet(owner, 0, damage, size, owner.GetColor());
            }
            return true;
        }
    }
}
