using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

        public ICollection<Tile> GetPossibleColliders(Rectangle rectangle)
        {
            ICollection<Tile> colliders = new LinkedList<Tile>();

            int tilesWidth = 1 + (rectangle.Width) / Tile.SIZE;
            int tilesHeight = 1 + (rectangle.Height) / Tile.SIZE;


            for (int y = 0; y <= tilesHeight; y++)
            {
                for (int x = 0; x <= tilesWidth; x++)
                {
                    int posX = MathHelper.Clamp(x + rectangle.Left / Tile.SIZE, 0, GetWidth() - 1);
                    int posY = MathHelper.Clamp(y + rectangle.Top / Tile.SIZE, 0, GetHeight() - 1);



                    if (tileMap[posX, posY].GetTileType() != ETileType.WALL && tileMap[posX, posY].GetTileType() != ETileType.CRATE)
                        continue;

                    if (tileMap[posX, posY].GetRecHit().Intersects(rectangle))
                        colliders.Add(tileMap[posX, posY]);
                }
            }

            return colliders;
        }

        public Tile[,] getTileMap() { return tileMap; }

        public int GetWidth() { return tileMap.GetLength(0); }

        public int GetHeight() { return tileMap.GetLength(1); }

    }
}
