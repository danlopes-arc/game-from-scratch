using GameFromScratch.Extensions;
using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Entities
{
    public class EnemyMissile : Entity
    {
        private Player player;

        public EnemyMissile(GameScene scene, SpriteBatch spriteBatch, Player player) : base(scene, spriteBatch)
        {
            this.player = player;
            Size = new Vector2(40, 20);
            Velocity = new Vector2(-200, 0);
            // Texture = Game.Content.Load<Texture2D>("Images/Missile");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Move(gameTime);

            var y = player.Position.Y + player.Size.Y / 2 - Size.Y / 2;

            Position = new Vector2(Position.X, y);

            if (Position.X + Size.X < 0)
            {
                Destroy();
            }

            // else if (Position.Y + Size.Y > ScreenSize.Y)
            // {
            //     Position = new Vector2(Position.X, ScreenSize.Y - Size.Y);
            // }
        }

        public override void OnCollision(Entity other)
        {
            if (other is Player)
            {
                player.Health = 0;
                Destroy();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            // spriteBatch.Draw(Texture, Bounds, Color.White);
            spriteBatch.DrawRectangle(GraphicsDevice, Bounds, Color.Green);

            spriteBatch.End();
        }
    }
}