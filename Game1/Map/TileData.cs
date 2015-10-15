using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
    public class TileData
    {
        public Texture2D Texture { get; private set; }
        public ETileType TileType { get; private set; }
        public Color Tint { get; private set;  }

        public TileData(ETileType type, Texture2D texture, Color tint)
        {
            this.TileType = type;
            this.Texture = texture;
            this.Tint = tint;
        }
    }
}
