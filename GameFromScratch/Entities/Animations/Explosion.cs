using GameFromScratch.Animation;
using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Entities.Animations
{
    public class Explosion : Entity
    {
        public Explosion(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            animator.Sequence =
                new Sequence(Game.Content.Load<Texture2D>("Sequences/Explosion"), new Point(64, 64), .2f);
            animator.Play();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (animator.Complete)
            {
                Destroy();
                return;
            }

            animator.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (Destroyed) return;

            spriteBatch.Begin();
            animator.Draw(Position.ToPoint(), Size.ToPoint());
            spriteBatch.End();
        }
    }
}