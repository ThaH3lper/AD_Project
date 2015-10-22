using Game1.Scene;
using Microsoft.Xna.Framework;


namespace Game1.Entitys
{
    class NormalEnemy : BaseEnemy
    {
        public NormalEnemy(Vector2 position, SimulationWorld world) : base(Globals.player, position, 100, 40, world)
        {
            health = 150;
            maxHealth = 150;
            color = Color.DarkOliveGreen;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
        }
    }
}
