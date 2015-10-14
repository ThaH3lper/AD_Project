using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Patrik.GameProject;

namespace Game1.Entitys
{
    public class BaseEnemy : Entity
    {
        Vector2 target;

        public BaseEnemy(Texture2D texture, Vector2 position, float speed, int size, SimulationWorld world) : base(texture, position, speed, size, world)
        {

        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (target != Vector2.Zero && (target - position).Length() > 2)
                position = Vector2.Lerp(position, target, delta * speed / 100);
            else
                RebuildPath();

            Face(world.Player.GetPosition());

            if (world.RayCast(GetPosition(), world.Player.GetPosition()))
            {
                Fire();
            }
        }

        private void RebuildPath()
        {
            var path = world.PathFinder.Pathfind(((position ) / Tile.SIZE).ToPoint(), ((world.Player.GetPosition() ) / Tile.SIZE).ToPoint()); //- new Vector2(Tile.SIZE/2f) , new Vector2(Tile.SIZE / 2f)
            if (path.Count > 1)
                target = path[1].ToVector2() * Tile.SIZE  + new Vector2(Tile.SIZE/2f);
        }
    }
}
