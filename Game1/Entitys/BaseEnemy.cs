using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Patrik.GameProject;

namespace Game1.Entitys
{
    class BaseEnemy : Entity
    {
        public BaseEnemy(Texture2D texture, Vector2 position, float speed, int size, Map map) : base(texture, position, speed, size, map)
        {

        }
    }
}
