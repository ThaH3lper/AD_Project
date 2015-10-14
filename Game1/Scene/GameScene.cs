using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Game1.Datastructures.ADT;
using Game1.Entitys;
using Game1.Datastructures.Implementations;
using Game1.Datastructures;
using System;

namespace Patrik.GameProject
{
    class GameScene : StandardScene
    {
        Player player;
        Map map;
        BulletManager bulletManager;
        IList<BaseEnemy> enemies;

        SpatialHashGrid collisionCuller;


        public GameScene(GraphicsDeviceManager gdm, MainGame game) : base(gdm, game)
        {
            this.map = Globals.map;
            this.player = new Player(new Vector2(100, 100), map, input);
            this.bulletManager = new BulletManager();
            this.enemies = new LinkedList<BaseEnemy>();
            this.collisionCuller = new SpatialHashGrid();
            this.collisionCuller.Setup(map.GetWidth() * Tile.SIZE, map.GetHeight() * Tile.SIZE, (map.GetWidth() * Tile.SIZE) / 6);
        }

        public override void Update(float delta)
        {
            UpdateCollisionCuller();
            UpdatePlayer(delta);
            UpdateCamera();
            UpdateBullets(delta);

            base.Update(delta);
        }

        private void UpdateCollisionCuller()
        {
            collisionCuller.ClearBuckets();
            collisionCuller.AddObject(player);
            collisionCuller.AddObject(enemies);
        }

        private void UpdatePlayer(float delta)
        {
            player.Update(delta);
        }

        private void UpdateCamera()
        {
            camera.Lerp(player.GetPosition());
        }

        private void UpdateBullets(float delta)
        {
            bulletManager.Update(delta);

            if (input.LeftClick())
                bulletManager.addBullet(player);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            map.Draw(batch);

            bulletManager.Draw(batch);

            player.Draw(batch);

            foreach (var enemy in enemies)
            {
                enemy.Draw(batch);
            }

            batch.End();
        }
    }
}
