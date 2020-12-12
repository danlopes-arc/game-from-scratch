using GameFromScratch.Animation;
using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Entities.Animations
{
    public class AsteroidExplosion : Entity
    {
        public AsteroidExplosion(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            animator.Sequence =
                new Sequence(Game.Content.Load<Texture2D>("Sequences/RockExplosion"), new Point(274), .2f, 2, 5);
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