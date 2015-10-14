using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Patrik.GameProject
{
    class Player : Entity
    {
        public enum Commands
        {
               MoveLeft, MoveRight, MoveUp, MoveDown
        }

        private Inputs input;

        public Player(Vector2 position, Map map, Inputs input) : base(position, 250, 40, map)
        {
            this.input = input;
        }

        public override void Update(float delta)
        {
            Vector2 dir = new Vector2(0, 0);
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveDown)))
                dir.Y += 1;
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveUp)))
                dir.Y -= 1;
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveLeft)))
                dir.X -= 1;
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveRight)))
                dir.X += 1;
            if (dir.X != 0)
                dir.Normalize();
            Move(dir);

            posAim = input.GetPosCamera();

            base.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
