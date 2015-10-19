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

        protected float cooldown, damage;
        protected int size;
        protected string name;

        private float currentTime;
        private bool fired;

        protected Entity owner;

        public Weapon(SimulationWorld world, Entity owner)
        {
            this.world = world;
            this.owner = owner;
            this.size = 10; //default size
        }

        public virtual bool Fire()
        {
            if (fired == true)
                return false;
            fired = true;
            currentTime = 0;
            return true;
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
            return (floatCooldown > 1) ? 1f : floatCooldown;
        }

        public float GetDamage() { return damage; }
    }
}
