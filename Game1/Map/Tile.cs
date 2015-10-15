using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
    public class Tile
    {
        public static int SIZE = 64;

        ETileType type;
        Rectangle recHit;
        Texture2D texture;
        Color color;
        bool passable; //todo

        public Tile(int x, int y, TileData data)
        {
            texture = data.Texture;
            type = data.TileType;
            color = data.Tint;

            recHit = new Rectangle(x, y, SIZE, SIZE);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, recHit, color);
        }

        public Rectangle GetRecHit() { return recHit; }
        public ETileType GetTileType() { return type; }
    }
}
