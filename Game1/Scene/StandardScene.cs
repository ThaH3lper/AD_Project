using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    public class StandardScene
    {
        protected Camera camera;
        protected Camera hudCamera;
        protected Inputs input;
        protected GraphicsDeviceManager gdm;
        protected MainGame game;

        public StandardScene(GraphicsDeviceManager gdm, MainGame game)
        {
            this.game = game;
            this.gdm = gdm;
            camera = new Camera(gdm.GraphicsDevice.Viewport, new Vector2(0, 0));
            hudCamera = new Camera(gdm.GraphicsDevice.Viewport, new Vector2(0, 0));
            input = new Inputs(camera, hudCamera);
        }

        public virtual void Update(float delta)
        {
            camera.Update(gdm);
            hudCamera.Update(gdm);
            input.Update();
        }

        public virtual void Draw(SpriteBatch batch)
        {

        }

    }
}
