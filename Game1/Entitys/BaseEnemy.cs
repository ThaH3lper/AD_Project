using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Patrik.GameProject;

namespace Game1.Entitys
{
    public class BaseEnemy : Entity
    {
        public BaseEnemy(Texture2D texture, Vector2 position, float speed, int size, SimulationWorld world) : base(texture, position, speed, size, world)
        {

        }

        public override void Update(float delta)
        {
            base.Update(delta);

            Face(world.Player.GetPosition());

            if (world.RayCast(GetPosition(), world.Player.GetPosition()))
            {
                world.SpawnBullet(this);
            }
        }
    }
}
