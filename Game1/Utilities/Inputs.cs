using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using Game1.Datastructures.Implementations;
using Game1.Datastructures.ADT;
using System.Linq;

namespace Patrik.GameProject
{
    public class Inputs
    {
        private Camera camera, cameraHud;
        private KeyboardState keyState, oldKeyState = Keyboard.GetState();
        private MouseState mouseState, oldMouseState = Mouse.GetState();

        public Inputs(Camera camera, Camera cameraHud)
        {
            this.camera = camera;
            this.cameraHud = cameraHud;
        }
        public bool KeyClick(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }

        public bool KeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public bool KeyPressed(System.Collections.Generic.IEnumerable<Keys> keys)
        {
            return keys.Any(x => keyState.IsKeyDown(x));
        }

        public IList<Keys> GetKeyPressed()
        {
            IList<Keys> keys = new LinkedList<Keys>();
            foreach (Keys key in keyState.GetPressedKeys())
            {
                if (oldKeyState.IsKeyUp(key))
                    keys.Add(key);
            }
            return keys;
        }
        public bool LeftClick()
        {
            return mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
        }
        public bool LeftPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }
        public bool RightClick()
        {
            return mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
        }
        public bool RightPressed()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }
        public Vector2 GetPosCamera()
        {
            return camera.UnProject(new Vector2(mouseState.X, mouseState.Y));
        }
        public Vector2 GetPosScreen()
        {
            return camera.Project(new Vector2(mouseState.X, mouseState.Y));
        }
        public Vector2 GetPosCameraHud()
        {
            return cameraHud.UnProject(new Vector2(mouseState.X, mouseState.Y));
        }

        public void Update()
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
        }
    }
}