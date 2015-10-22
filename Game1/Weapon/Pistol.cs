using Game1.Scene;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class Pistol : Weapon
    {
        public Pistol(SimulationWorld world, Entity owner) : base(world, owner)
        {
            cooldown = 0.4f;
            damage = 50f;
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
