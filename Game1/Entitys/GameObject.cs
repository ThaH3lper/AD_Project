using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Patrik.GameProject
{
    class GameObject
    {
        protected float speed, rotation, scale;
        protected Vector2 direction, position, originHit, originDraw;
        protected Rectangle recHit, recDraw;
        protected Texture2D texture;

        public GameObject(Texture2D texture, Vector2 position, float speed, int size, Rectangle recDraw)
        {
            this.speed = speed;
            this.position = position;
            this.recDraw = recDraw;
            this.texture = texture;
            recHit = new Rectangle(0, 0, size, size);

            originHit = new Vector2(recHit.Width / 2, recHit.Height / 2);
            originDraw = new Vector2(recDraw.Width / 2, recDraw.Height / 2);

            scale = (size / (float)recDraw.Width);
            rotation = 0;
        }

        public virtual void Update(float delta)
        {
            position += direction * speed * delta;
            recHit = new Rectangle((int)(position.X - originHit.X), (int)(position.Y - originHit.Y), recHit.Width, recHit.Height);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, recDraw, Color.Red, rotation, new Vector2(32, 32), scale, SpriteEffects.None, 1.0f);

            //Debug collision:
            //batch.Draw(Globals.dot, recHit, Color.FromNonPremultiplied(new Vector4(0f, 0f, 0f, 0.5f)));
        }

        public Vector2 GetPosition() { return position; }
        public void SetPosition(Vector2 position) { this.position = position; }

        public Vector2 GetOrigin() { return originHit; }

        public float GetScale() { return scale; }

        public float GetRotation() { return rotation; }
    }
}
