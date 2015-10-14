using Game1.Datastructures.ADT;
using Game1.Datastructures.Implementations;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
   public class BulletManager
    {
        private IList<Bullet> bullets;

        public BulletManager()
        {
            bullets = new LinkedList<Bullet>();
        }

        public void addBullet(Entity shooter)
        {
            bullets.Add(new Bullet(shooter));
        }

        public void Update(float delta)
        {
            foreach(Bullet b in bullets)
                b.Update(delta);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Bullet b in bullets)
                b.Draw(batch);
        }
    }
}
