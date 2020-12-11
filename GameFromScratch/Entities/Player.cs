using GameFromScratch.Extensions;
using GameFromScratch.Scenes;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Entities
{
    public class Player : Entity
    {
        private const float MoveSpeed = 200;

        public float ShotInterval
        {
            get => shotTimer.Total;
            set => shotTimer.Total = value;
        }

        private Counter shotTimer;
        
        public int Health { get; set; }
        public Player(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            Size = new Vector2(80, 80);
            shotTimer = new Counter(.3f);
            Texture = Game.Content.Load<Texture2D>("Images/SpaceShipTopView");
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

            if (Position.Y < scene.Bounds.Top)
            {
                Position = new Vector2(Position.X, scene.Bounds.Top);
            }
            else if (Position.Y + Size.Y > scene.Bounds.Bottom)
            {
                Position = new Vector2(Position.X, scene.Bounds.Bottom - Size.Y);
            }

            if (shotTimer.Done || shotTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                if (BetterMouseState.IsDown(MouseButton.Left))
                {
                    var missile = new Missile(scene, spriteBatch);
                    missile.Position = new Vector2(Position.X + Size.X, Position.Y + Size.Y / 2 - missile.Size.Y / 2);
                    scene.AddEntity(missile);
                    shotTimer.Reset();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            
            spriteBatch.Draw(Texture, Bounds, Color.White);
            // spriteBatch.DrawRectangle(GraphicsDevice, Bounds, Color.Red);

            spriteBatch.End();
        }
    }
}
