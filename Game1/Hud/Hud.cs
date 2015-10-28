using Game1.Hud;
using Game1.Scene;
using Game1.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        MainMenu mainMenu;
        bool menu;
        Spawner spawner;
        Inputs input;

        public Hud(Camera camera, Camera cameraHud, Inputs input, SimulationWorld world)
        {
            this.camera = camera;
            this.cameraHud = cameraHud;
            this.world = world;
            this.aim = new Aim(world.Player, this);
            this.mainMenu = new MainMenu();
            this.input = input;
            menu = true;
        }
        public void NewGame()
        {
            menu = false;
            spawner = new Spawner(world);
            scoreLerp = 0;
        }

        public void GameOver()
        {
            world.ClearBullets();
            world.ClearEnemys();
            world.Player.HealFullHealth();
            world.Powers.Clear();

            menu = true;
            spawner = null;
        }

        public void Update(float delta)
        {
            aim.Update(delta);
            aim.setAimCooldown(world.Player.GetWeapon().GetCooldownFloat());
            if (world.Player.Dead && !menu)
                GameOver();
            if (spawner != null)
                spawner.Update(delta);
            if (menu)
            {
                mainMenu.Update(WorldToHudPosition(world.Player.GetPosition()), delta);
                if (input.KeyClick(Keys.Y))
                    NewGame();
                if (input.KeyClick(Keys.N))
                    MainGame.game.Exit();
            }
        }
        public Vector2 WorldToHudPosition(Vector2 position)
        {
            Vector2 temp = camera.Project(position);
            return temp - new Vector2(cameraHud.viewPort.Width/2, cameraHud.viewPort.Height/2);
        }

        public void Render(SpriteBatch batch)
        {
            aim.Draw(batch);
            DrawScore(batch);
            if (menu)
            {
                mainMenu.Render(batch);
            }
        }

        private float scoreLerp;
        private void DrawScore(SpriteBatch batch)
        {
            scoreLerp = MathHelper.Lerp(scoreLerp, world.PlayerScore + 1, 0.06f);
            batch.DrawString(Globals.font, "Score: " + (int)scoreLerp, new Vector2(-60,-350), Color.Red, 0, new Vector2(0,0), 1.5f, SpriteEffects.None, 1f);
        }
    }
}
