using Game1.Datastructures;
using Game1.Datastructures.ADT;
using Game1.Datastructures.Implementations;
using Game1.Entitys;
using Microsoft.Xna.Framework;
using Patrik.GameProject;
using System;
using System.Linq;
using System.Text;

namespace Game1.Scene
{
    public class SimulationWorld
    {
        public Player Player { get; set; }

        public Map Map { get; set; }

        public BulletManager BulletManager { get; set; }

        public IList<BaseEnemy> Enemies { get; set; }

        private SpatialHashGrid collisionCuller;
        private Inputs input;

        public SimulationWorld(Inputs input)
        {
            this.input = input;
            this.Map = Globals.map;
            this.Player = new Player(new Vector2(100, 100), this, input);
            this.BulletManager = new BulletManager();
            this.Enemies = new LinkedList<BaseEnemy>();
            this.collisionCuller = new SpatialHashGrid();
            this.collisionCuller.Setup(Map.GetWidth() * Tile.SIZE, Map.GetHeight() * Tile.SIZE, (Map.GetWidth() * Tile.SIZE) / 6);

            InitEnemies();
        }

        private void InitEnemies()
        {
            BaseEnemy test = new BaseEnemy(Globals.player, new Vector2(300, 300), 240, 40, this);
            Enemies.Add(test);

            test = new BaseEnemy(Globals.player, new Vector2(300, 600), 240, 40, this);
            Enemies.Add(test);
        }

        public void Update(float delta)
        {
            UpdateCollisionCuller();
            UpdatePlayer(delta);
            UpdateEnemies(delta);
            UpdateBullets(delta);

            //var nearby = collisionCuller.GetPossibleColliders(player);
            var canSee = RayCast(Enemies[1].GetPosition(), Player.GetPosition());
            Console.WriteLine(canSee);
        }

        public bool RayCast(Vector2 origin, Vector2 target)
        {
            var segment = new Segment(origin, target);

            for (int y = 0; y < Map.getTileMap().GetLength(0); y++)
            {
                for (int x = 0; x < Map.getTileMap().GetLength(1); x++)
                {
                    if (Map.getTileMap()[x, y].GetType() != ETileType.WALL) // && Map.getTileMap()[x, y].GetType() != ETileType.CREATE)
                        continue;

                    if (segment.Collide(Map.getTileMap()[x, y].GetRecHit()))
                        return false;
                }
            }

            // Should enemies block raycast?
            foreach (var enemy in Enemies)
            {
                //  if (segment.Collide(enemy.GetHitRectangle()))
                //     return false;
            }
            return true;
        }

        public void SpawnBullet(Entity owner)
        {
            BulletManager.addBullet(owner);
        }

        /// <summary>
        /// Clear collision map and re-add all to ensure that everyone is in the right bucket.
        /// </summary>
        private void UpdateCollisionCuller()
        {
            collisionCuller.ClearBuckets();
            collisionCuller.AddObject(Player);
            collisionCuller.AddObject(Enemies);
        }

        private void UpdateEnemies(float delta)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Update(delta);
            }
        }

        private void UpdatePlayer(float delta)
        {
            Player.Update(delta);
        }

        private void UpdateBullets(float delta)
        {
            BulletManager.Update(delta);

            if (input.LeftPressed())
                Player.Fire();
        }

    }
}
