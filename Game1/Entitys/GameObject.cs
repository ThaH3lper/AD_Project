using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Patrik.GameProject
{
    public class GameObject
    {
        protected float rotation, scale;
        protected Vector2 direction, position, originHit, originDraw;
        protected Rectangle recHit, recDraw;
        protected Texture2D texture;
        protected Color color;

        public GameObject(Texture2D texture, Vector2 position, int size, Rectangle recDraw)
        {
            this.color = Color.White;
            this.position = position;
            this.recDraw = recDraw;
            this.texture = texture;
            recHit = new Rectangle(0, 0, size, size);

            originHit = new Vector2(recHit.Width / 2, recHit.Height / 2);
            originDraw = new Vector2(recDraw.Width / 2, recDraw.Height / 2);

            scale = (size / (float)recDraw.Width);

            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);
            rotation = 0;
        }

        public virtual void Update(float delta)
        {
            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, recDraw, color, rotation, originDraw, scale, SpriteEffects.None, 1.0f);

            //batch.Draw(Globals.dot, recHit, Color.FromNonPremultiplied(new Vector4(1, 0, 0, 0.3f)));
        }

        public virtual bool Blocks(GameObject other)
        {
            return false;
        }

        public Vector2 GetPosition() { return position; }
        public void SetPosition(Vector2 position) { this.position = position; }

        public Vector2 GetOrigin() { return originHit; }

        public float GetScale() { return scale; }

        public float GetMaxRadius()
        {
            return Math.Max(recHit.Width, recHit.Height) / 2f;
        }

        public Rectangle GetHitRectangle() { return recHit;  }

        public float GetRotation() { return rotation; }

        public Color GetColor() { return color; }
    }
}
