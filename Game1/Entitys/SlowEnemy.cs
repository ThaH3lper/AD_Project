using Game1.Scene;
using Microsoft.Xna.Framework;


namespace Game1.Entitys
{
    class SlowEnemy : BaseEnemy
    {
        public SlowEnemy(Vector2 position, SimulationWorld world) : base(Globals.player, position, 60, 50, world)
        {
            health = 250;
            maxHealth = 250;
            color = Color.Red;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
        }
    }
}
