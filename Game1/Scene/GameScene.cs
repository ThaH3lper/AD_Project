using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game1.Datastructures.ADT;
using Game1.Entitys;
using Game1.Datastructures.Implementations;
using Game1.Datastructures;
using System.Linq;
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

            InitEnemies();
        }

        private void InitEnemies()
        {
            BaseEnemy test = new BaseEnemy(Globals.player, new Vector2(300, 300), 240, 40, map);
            enemies.Add(test);

            test = new BaseEnemy(Globals.player, new Vector2(300, 600), 240, 40, map);
            enemies.Add(test);
        }

        public override void Update(float delta)
        {
            UpdateCollisionCuller();
            UpdatePlayer(delta);
            UpdateCamera();
            UpdateBullets(delta);

            //var nearby = collisionCuller.GetPossibleColliders(player);
            var canSee = RayCast(enemies[1].GetPosition(), player.GetPosition());
            Console.WriteLine(canSee);

            base.Update(delta);
        }

        public bool RayCast(Vector2 origin, Vector2 target)
        {
            var segment = new Segment(origin, target);

            for (int y = 0; y < map.getTileMap().GetLength(0); y++)
            {
                for (int x = 0; x < map.getTileMap().GetLength(1); x++)
                {
                    if (map.getTileMap()[x, y].GetType() != ETileType.WALL && map.getTileMap()[x, y].GetType() != ETileType.CREATE)
                        continue;

                    if (segment.Collide(map.getTileMap()[x, y].GetRecHit()))
                        return false;
                }
            }

            foreach (var enemy in enemies)
            {
              //  if (segment.Collide(enemy.GetHitRectangle()))
               //     return false;
            }
            return true;
        }

        /// <summary>
        /// Clear collision map and re-add all to ensure that everyone is in the right bucket.
        /// </summary>
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
