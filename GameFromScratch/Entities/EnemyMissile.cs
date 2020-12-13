using System;
using GameFromScratch.Entities.Animations;
using GameFromScratch.Extensions;
using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Entities
{
    public class EnemyMissile : Entity
    {
        private Player player;
        private SoundEffect explosionSound;

        public EnemyMissile(GameScene scene, SpriteBatch spriteBatch, Player player) : base(scene, spriteBatch)
        {
            this.player = player;
            Size = new Vector2(60, 15);
            Velocity = new Vector2(-200, 0);
            Texture = Game.Content.Load<Texture2D>("Images/MissileRedStripe");
            explosionSound = Game.Content.Load<SoundEffect>("SoundEffects/AsteroidExplosion");
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
            spriteBatch.Draw(Texture, Bounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            spriteBatch.End();
        }

        public override void Kill()
        {
            var side = Math.Max(Size.X, Size.Y);
            var posY = Position.Y + Size.Y / 2 - side / 2;
            scene.AddEntity(new Explosion2(scene, spriteBatch)
            {
                Position = new Vector2(Position.X, posY) - new Vector2(side) / 2,
                Size = new Vector2(side) * 2
            });
            explosionSound.Play(.5f, 0, 0);
            base.Kill();
        }
    }
}