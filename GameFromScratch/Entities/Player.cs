using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace GameFromScratch
{
    class Player : Entity
    {
        private const float MoveSpeed = 200;

        public int Health { get; set; }
        public Player(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            Size = new Vector2(60, 40);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if (Keyboard.GetState().IsKeyDown(Keys.W))
            //{
            //    Velocity = new Vector2(Velocity.X, -MoveSpeed);
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    Velocity = new Vector2(Velocity.X, +MoveSpeed);
            //}
            //else
            //{
            //    Velocity = new Vector2(Velocity.X, 0);
            //}
            //Move(gameTime);

            Position = new Vector2(Position.X, Mouse.GetState().Y - Size.Y / 2);

            if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, 0);
            }
            else if (Position.Y + Size.Y > ScreenSize.Y)
            {
                Position = new Vector2(Position.X, ScreenSize.Y - Size.Y);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawFillRectangle(GraphicsDevice, Bounds, Color.Red);

            spriteBatch.End();
        }
    }
}
