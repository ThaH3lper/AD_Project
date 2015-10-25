using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Patrik.GameProject
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private static StandardScene currentScene;
        public static MainGame game;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Globals.GAME_HEIGHT;
            graphics.PreferredBackBufferWidth = Globals.GAME_WIDTH;
            graphics.ApplyChanges();

            game = this;
            Window.Title = Globals.TITLE;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Globals.LoadContent(this);
            base.Initialize();
            setScene(EScene.GAMESCENE);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentScene != null)
                currentScene.Update(delta);

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (currentScene != null)
                currentScene.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void setScene(EScene scene)
        {
            switch (scene)
            {
                case EScene.MAINSCENE:
                    break;
                case EScene.GAMESCENE:
                    currentScene = new GameScene(graphics, this);
                    break;
            }
        }
    }
}
