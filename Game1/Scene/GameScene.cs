using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game1.Datastructures.ADT;
using Game1.Entitys;
using Game1.Datastructures.Implementations;
using Game1.Datastructures;
using System.Linq;
using System;
using Game1.Scene;

namespace Patrik.GameProject
{
    public class GameScene : StandardScene
    {

        SimulationWorld world;
        Hud hud;

        public GameScene(GraphicsDeviceManager gdm, MainGame game) : base(gdm, game)
        {
            this.world = new SimulationWorld(input);
            this.hud = new Hud(camera, hudCamera, input, world);
        }

      
        public override void Update(float delta)
        {
            world.Update(delta);
            UpdateCamera();

            base.Update(delta);

            hud.Update(delta);
        }

        private void UpdateCamera()
        {
            camera.Lerp(world.Player.GetPosition());
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            world.Map.Draw(batch);

            world.BulletManager.Draw(batch);

            world.Player.Draw(batch);

            foreach (var enemy in world.Enemies)
            {
                enemy.Draw(batch);
            }

            foreach (var power in world.Powers)
            {
                power.Draw(batch);
            }

            batch.End();

            batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, hudCamera.Transform);
            hud.Render(batch);
            batch.End();
        }
    }
}
