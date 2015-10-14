using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    public class Camera
    {
        private Matrix transform;
        private Vector2 position;
        private float rotation, zoomX, zoomY, lerp = 0.1f;
        public Viewport viewPort { get; private set; }

        public Camera (Viewport viewPort, Vector2 position)
        {
            this.viewPort = viewPort;
            this.position = position;
        }

        public void Update(GraphicsDeviceManager graphics)
        {
            int width = graphics.GraphicsDevice.Viewport.Width;
            int height = graphics.GraphicsDevice.Viewport.Height;
            zoomX = ((float)width / viewPort.Width);
            zoomY = ((float)height / viewPort.Height);
            
            transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                        Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateScale(zoomX, zoomY, 1) *
                        Matrix.CreateTranslation(new Vector3(width / 2, height / 2, 0));
        }
        public void Lerp(Vector2 newPos)
        {
            position.X += (newPos.X - position.X) * lerp;
            position.Y += (newPos.Y - position.Y) * lerp;
        }

        public Vector2 UnProject(Vector2 vector)
        {
            return Vector2.Transform(vector, Matrix.Invert(transform));
        }
        public Vector2 Project(Vector2 vector)
        {
            return Vector2.Transform(vector, transform);
        }
        public Matrix Transform { 
            get { return transform; } 
        }
        public Vector2 Position { 
            get { return position; } 
            set { position = value; } 
        }
        public float X {
            get { return position.X;}
            set { position.X = value;}       
        }
        public float Y{
            get { return position.Y; }
            set { position.Y = value; }
        }
        public float Rotation{
            get { return rotation; }
            set { rotation = value; }
        }
        public float ZoomX{
            get { return zoomX; }
            set { zoomX = value; }
        }
        public float ZoomY{
            get { return zoomY; }
            set { zoomY = value; }
        }
    }
}
