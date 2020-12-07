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
            Velocity = new Vector2(100, 0);
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            Move(gameTime);

            if (Position.X > ScreenSize.X)
            {
                scene.RemoveEntity(this);
            }
            // else if (Position.Y + Size.Y > ScreenSize.Y)
            // {
            //     Position = new Vector2(Position.X, ScreenSize.Y - Size.Y);
            // }

        }

        public override void OnCollision(Entity other)
        {
            if (other is Asteroid)
            {
                other.Kill();
                Destroy();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawFillRectangle(GraphicsDevice, Bounds, Color.Green);

            spriteBatch.End();
        }
    }
}