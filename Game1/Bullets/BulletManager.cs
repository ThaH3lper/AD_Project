using Game1.Datastructures.ADT;
using Game1.Datastructures.Implementations;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
    public class BulletManager
    {
        private IList<Bullet> bullets;
        private IList<Bullet> deadBullets;

        public BulletManager()
        {
            bullets = new LinkedList<Bullet>();
            deadBullets = new LinkedList<Bullet>();
        }

        public void addBullet(Entity shooter)
        {
            bullets.Add(new Bullet(shooter));
        }

        public void Update(float delta)
        {
            foreach (Bullet b in bullets)
            {
                b.Update(delta);

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
    }
}
