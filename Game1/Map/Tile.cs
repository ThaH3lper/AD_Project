using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
    public class Tile : GameObject
    {
        public static int SIZE = 64;

        ETileType type;

        public Tile(int x, int y, TileData data): base(data.Texture, new Vector2(x + 32, y+ 32), SIZE, new Rectangle(0, 0, 64, 64))
        {
            type = data.TileType;
            color = data.Tint;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            //batch.Draw(texture, recHit, color);
        }

        public override bool Blocks(GameObject other)
        {
            if (type == ETileType.WALL || type == ETileType.CRATE)
            {
                if (other is Bullet)
                    ((Bullet)other).CheckKill(this);

                return true;
            }
            return false;
        }

        public Rectangle GetRecHit() { return recHit; }
        public ETileType GetTileType() { return type; }
    }
}
