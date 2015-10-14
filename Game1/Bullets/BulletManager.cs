using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class BulletManager
    {
        List<Bullet> bullets;
        public BulletManager()
        {
            bullets = new List<Bullet>();
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
