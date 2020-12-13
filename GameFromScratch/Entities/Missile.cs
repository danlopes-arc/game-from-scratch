using GameFromScratch.Entities.Animations;
using GameFromScratch.Extensions;
using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Entities
{
    public class Missile : Entity
    {
        public Missile(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            Size = new Vector2(40, 20);
            Velocity = new Vector2(300, 0);
            Texture = Game.Content.Load<Texture2D>("Images/Missile");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Move(gameTime);

            if (Position.X > ScreenSize.X)
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
            if (other is Asteroid || other is EnemyMissile || other is EnemyShip)
            {
                other.Kill();
                Destroy();
                // scene.AddEntity(new Explosion2(scene, spriteBatch)
                // {
                //     Position = Position - new Vector2(0, Size.Y),
                //     Size = new Vector2(60, 60)
                // });
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.Draw(Texture, Bounds, Color.White);
            // spriteBatch.DrawRectangle(GraphicsDevice, Bounds, Color.Green);

            spriteBatch.End();
        }
    }
}