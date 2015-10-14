using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
    public class Map
    {
        private Tile[,] tileMap;
        public Map(int width, int height, char[,] charMap)
        {
            tileMap = new Tile[width, height];
            for (int y = 0; y < tileMap.GetLength(0); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                    tileMap[x, y] = new Tile(x * Tile.SIZE, y * Tile.SIZE, Globals.tileTable.Get(charMap[x, y]));
            }
        }

        public void Draw(SpriteBatch batch)
        {
            for (int y = 0; y < tileMap.GetLength(0); y++)
            {
                for (int x = 0; x < tileMap.GetLength(1); x++)
                    tileMap[x, y].Draw(batch);
            }
        }

        public Tile GetCollidingTile(Rectangle rectangle)
        {
            for (int y = 0; y < tileMap.GetLength(0); y++)
            {
                for (int x = 0; x < tileMap.GetLength(1); x++)
                {
                    if (tileMap[x, y].GetTileType() != ETileType.WALL && tileMap[x, y].GetTileType() != ETileType.CRATE)
                        continue;
                    if (tileMap[x, y].GetRecHit().Intersects(rectangle))
                        return tileMap[x, y];
                }
            }
            return null;
        }

        public Tile[,] getTileMap() { return tileMap; }

        public int GetWidth() { return tileMap.GetLength(0); }

        public int GetHeight() { return tileMap.GetLength(1); }

    }
}
