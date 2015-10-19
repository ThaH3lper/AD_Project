using Game1.Scene;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class Bazooka : Weapon
    {
        public Bazooka(SimulationWorld world, Entity owner) : base(world, owner)
        {
            cooldown = 1.4f;
            damage = 200.0f;
            size = 20;
        }

        public override bool Fire()
        {
            if(base.Fire())
            { 
                world.SpawnBullet(owner, 0, damage, size);
            }
            return true;
        }
    }
}
