using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
    struct TileData
    {
        public Texture2D Texture { get; private set; }
        public ETileType Type { get; private set; }
        public Color Tint { get; private set;  }

        public TileData(ETileType type, Texture2D texture, Color tint)
        {
            Type = type;
            Texture = texture;
            Tint = tint;
        }
    }
}
