using Game1.Datastructures.ADT;
using Game1.Datastructures.Implementations;
using Game1.Entitys;
using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Patrik.GameProject
{
    public class BulletManager
    {
        private IList<Bullet> bullets;
        private IList<Bullet> deadBullets;
        private SimulationWorld world;

        public BulletManager(SimulationWorld world)
        {
            this.world = world;
            bullets = new LinkedList<Bullet>();
            deadBullets = new LinkedList<Bullet>();
        }
        public void ClearBullets()
        {
            bullets.Clear();
        }

        public void addBullet(Entity shooter, float offsetAngle, float damage, int size, Color color)
        {
            bullets.Add(new Bullet(shooter, offsetAngle, damage, size, color));
        }

        public void Update(float delta)
        {
            foreach (Bullet b in bullets)
            {
                b.Update(delta);

                System.Collections.Generic.ICollection<GameObject> colliders = world.GetColliders(b);
                if (colliders.Count > 0)
                {
                    foreach (var obj in colliders)
                    {
                        if (obj is Entity)
                        {
                            if (obj is BaseEnemy && b.GetOwner() is BaseEnemy)
                                continue;
                            if (obj is Player && b.GetOwner() is Player)
                                continue;
                            Entity e = (Entity)obj;
                            e.Damage(b.GetDamage());
                        }
                    }
                }


                if (b.Dead)
                {
                    deadBullets.Add(b);
                }
            }

            foreach (var deadBullet in deadBullets)
            {
                bullets.Remove(deadBullet);
            }
            deadBullets.Clear();
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Bullet b in bullets)
                b.Draw(batch);
        }

        public IList<Bullet> GetBullets() { return bullets; }
    }
}
