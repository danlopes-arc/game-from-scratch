using GameFromScratch.Animation;
using GameFromScratch.Entities.Animations;
using GameFromScratch.Extensions;
using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Entities
{
    public class Asteroid : Entity
    {
        private float rotation;

        public Asteroid(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            Size = new Vector2(50, 50);
            Texture = Game.Content.Load<Texture2D>("Images/Asteroid1");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Move(gameTime);
            rotation += .05f;
            animator.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
            if (Position.X + Size.X < 0)
            {
                Destroy();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            var rec = new Rectangle(Bounds.Location + (Bounds.Size.ToVector2() / 2).ToPoint(), Bounds.Size);
            spriteBatch.Draw(Texture, rec, null, Color.White, rotation,
                new Vector2(Texture.Width / 2f, Texture.Height / 2f), SpriteEffects.None, 0);
            // spriteBatch.DrawRectangle(GraphicsDevice, Bounds, Color.Blue);

            spriteBatch.End();
        }

        public override void OnCollision(Entity other)
        {
            if (other is Player player)
            {
                player.Health--;

                Kill();
            }
        }

        public override void Kill()
        {
            scene.AddEntity(new AsteroidExplosion(scene, spriteBatch)
            {
                Position = Position - Size / 2,
                Size = Size * 2
            });
            base.Kill();
        }
    }
}