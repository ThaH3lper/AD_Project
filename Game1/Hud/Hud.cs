using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    class Hud
    {
        Camera cameraHud;
        Camera camera;
        SimulationWorld world;
        Aim aim;

        public Hud(Camera camera, Camera cameraHud, SimulationWorld world)
        {
            this.camera = camera;
            this.cameraHud = cameraHud;
            this.world = world;
            this.aim = new Aim(world.Player, this);
        }

        public void Update(float delta)
        {
            aim.Update(delta);
            aim.setAimCooldown(world.Player.GetWeapon().GetCooldownFloat());
        }
        public Vector2 WorldToHudPosition(Vector2 position)
        {
            Vector2 temp = camera.Project(position);
            return temp - new Vector2(cameraHud.viewPort.Width/2, cameraHud.viewPort.Height/2);
            //return cameraHud.UnProject(position);
        }

        public void Render(SpriteBatch batch)
        {
            aim.Draw(batch);
        }
    }
}
