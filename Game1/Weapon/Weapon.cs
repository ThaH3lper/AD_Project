using Game1.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    public class Weapon
    {
        protected SimulationWorld world;

        protected float cooldown;
        protected string name;

        private float currentTime;
        private bool fired;

        Entity owner;

        public Weapon(SimulationWorld world, Entity owner)
        {
            this.world = world;
            this.owner = owner;
            cooldown = 0.5f;
        }

        public virtual void Fire()
        {
            if (fired == true)
                return;
            fired = true;
            currentTime = 0;

            world.SpawnBullet(owner);
        }

        public void Update(float delta)
        {
            if (cooldown > currentTime)
                currentTime += delta;
            else
                fired = false;
        }

        public float GetCooldownFloat()
        {
            float floatCooldown = currentTime / cooldown;
            return (cooldown > 1) ? 1f : floatCooldown;
        }
    }
}
