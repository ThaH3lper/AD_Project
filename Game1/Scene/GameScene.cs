using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Patrik.GameProject
{
    class GameScene : StandardScene
    {
        Player e;
        Map map;
        BulletManager bulletManager;


        public GameScene(GraphicsDeviceManager gdm, MainGame game) : base(gdm, game)
        {
            map = Globals.map;
            e = new Player(new Vector2(100, 100), map, input);
            bulletManager = new BulletManager();
        }

        public override void Update(float delta)
        {
            e.Update(delta);
            camera.Lerp(e.GetPosition());
            bulletManager.Update(delta);
            if (input.LeftClick())
                bulletManager.addBullet(e);
            base.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            map.Draw(batch);
            bulletManager.Draw(batch);
            e.Draw(batch);
            batch.End();
        }
    }
}
