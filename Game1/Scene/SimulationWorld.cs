using Game1.Datastructures;
using Game1.Datastructures.ADT;
using Game1.Datastructures.Algorithms;
using Game1.Datastructures.Implementations;
using Game1.Entitys;
using Game1.Entitys.Powers;
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

        public IList<BasePower> Powers { get; set; }

        private IList<BaseEnemy> deadEnemies;

        public PathFinder PathFinder { get; set; }

        private SpatialHashGrid collisionCuller;
        private Inputs input;

        public int PlayerScore { get; set; }

        public SimulationWorld(Inputs input)
        {
            this.input = input;
            this.Map = Globals.map;
            this.Player = new Player(new Vector2(100, 100), this, input);
            this.BulletManager = new BulletManager(this);
            this.Enemies = new LinkedList<BaseEnemy>();
            this.Powers = new LinkedList<BasePower>();
            this.deadEnemies = new LinkedList<BaseEnemy>();
            this.PathFinder = new PathFinder(this);
            this.collisionCuller = new SpatialHashGrid();
            this.collisionCuller.Setup(Map.GetWidth() * Tile.SIZE, Map.GetHeight() * Tile.SIZE, (Map.GetWidth() * Tile.SIZE) / 6);
        }

        public void ClearBullets()
        {
            BulletManager.ClearBullets();
        }
        public void ClearEnemys()
        {
            Enemies.Clear();
        }

        public void SpawnEnemy()
        {
            var spawns = new LinkedList<Tile>();
            foreach (var item in Map.getTileMap())
            {
                if (item.GetTileType() == ETileType.SPAWN)
                {
                    spawns.Add(item);
                }
            }

            var rand = new Random();

            Tile tile = spawns[rand.Next(0, spawns.Count)];

            BaseEnemy e = null;
            switch (rand.Next(0, 3))
            {
                case 0:
                    e = new FastEnemy(new Vector2(tile.GetRecHit().X + Tile.SIZE / 2f, tile.GetRecHit().Y + Tile.SIZE / 2f), this);
                    break;
                case 2:
                    e = new SlowEnemy(new Vector2(tile.GetRecHit().X + Tile.SIZE / 2f, tile.GetRecHit().Y + Tile.SIZE / 2f), this);
                    break;
                default:
                    e = new NormalEnemy(new Vector2(tile.GetRecHit().X + Tile.SIZE / 2f, tile.GetRecHit().Y + Tile.SIZE / 2f), this);
                    break;
            }
            Enemies.Add(e);
        }

        public System.Collections.Generic.ICollection<GameObject> GetColliders(GameObject obj)
        {
            IList<GameObject> colliders = new LinkedList<GameObject>();

            // Get all possible colliders exept myself and bullets
            colliders.AddRange(collisionCuller.GetPossibleColliders(obj).Where(x => x != obj));

            // Add tiles collisions
            var tileColliders = Map.GetPossibleColliders(obj.GetHitRectangle());
            colliders.AddRange(tileColliders);

            // At last only return those who intersects
            return colliders.Where(x => x.GetHitRectangle().Intersects(obj.GetHitRectangle()) && (x.Blocks(obj) || obj.Blocks(x))).ToList();

        }

        public void Update(float delta)
        {
            UpdateCollisionCuller();
            UpdatePlayer(delta);
            UpdateEnemies(delta);
            UpdateBullets(delta);
            UpdatePowers(delta);
        }

        public bool RayCast(GameObject origin, GameObject target)
        {
            var segment = new Segment(origin.GetPosition(), target.GetPosition());

            for (int y = 0; y < Map.getTileMap().GetLength(0); y++)
            {
                for (int x = 0; x < Map.getTileMap().GetLength(1); x++)
                {
                    if (Map.getTileMap()[x, y].GetTileType() != ETileType.WALL) // && Map.getTileMap()[x, y].GetType() != ETileType.CREATE)
                        continue;

                    if (segment.Collide(Map.getTileMap()[x, y].GetRecHit()))
                        return false;
                }
            }

            // Should enemies block raycast?
            foreach (var enemy in Enemies.Where(x => x != origin))
            {
                if (segment.Collide(enemy.GetHitRectangle()))
                    return false;
            }
            return true;
        }

        public void SpawnBullet(Entity owner, float offsetAngle, float damage, int size, Color color)
        {
            BulletManager.addBullet(owner, offsetAngle, damage, size, color);
        }

        /// <summary>
        /// Clear collision map and re-add all to ensure that everyone is in the right bucket.
        /// </summary>
        private void UpdateCollisionCuller()
        {
            collisionCuller.ClearBuckets();
            collisionCuller.AddObject(Player);
            collisionCuller.AddObject(Enemies);
            collisionCuller.AddObject(BulletManager.GetBullets());
        }

        private void UpdateEnemies(float delta)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Update(delta);
                if (enemy.Dead)
                    deadEnemies.Add(enemy);
            }

            foreach (var deadEnemy in deadEnemies)
            {
                Enemies.Remove(deadEnemy);
                PlayerScore += (int)(deadEnemy.GetMaxRadius() * 10);
            }
            deadEnemies.Clear();
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

        private void UpdatePowers(float delta)
        {
            foreach (var power in Powers)
            {
                power.Update(delta);
                if (Player.GetHitRectangle().Intersects(power.GetHitRectangle()))
                {
                    power.onPickup(Player);
                    Powers.Remove(power);
                    break;
                }
            }

            Random rnd = new Random();
            if (Powers.Count < 1 && rnd.NextDouble() < 0.004)
            {
                while (true)
                {
                    int x = rnd.Next(1, Map.GetWidth());
                    int y = rnd.Next(1, Map.GetHeight());

                    if (!Map.getTileMap()[x,y].Blocks(Player))
                    {
                        switch (rnd.Next(0, 1))
                        {
                            case 0:
                                Powers.Add(new WeaponCase(new Vector2(x * Tile.SIZE + Tile.SIZE/2, y * Tile.SIZE + Tile.SIZE / 2), 32, this));
                                break;
                            default:
                                Powers.Add(new WeaponCase(new Vector2(x * Tile.SIZE + Tile.SIZE / 2, y * Tile.SIZE + Tile.SIZE / 2), 32, this));
                                break;
                        }
                        break;
                    }
                }
            }
        }

    }
}
