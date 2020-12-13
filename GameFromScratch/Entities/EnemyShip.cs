using System;
using GameFromScratch.Entities.Animations;
using GameFromScratch.Extensions;
using GameFromScratch.Scenes;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Entities
{
    public class EnemyShip : Entity
    {
        private const float MoveSpeed = 120;
        private const float MoveDistance = 150;
        
        private SoundEffect explosionSound;
        private Player player;
        
        private bool invincible = true;
        private float invincibleCounter;

        private Counter waitTimer = new Counter(2);
        public bool HasShot { get; private set; }
        public bool Waiting { get; private set; }

        public EnemyShip(GameScene scene, SpriteBatch spriteBatch, Player player) : base(scene, spriteBatch)
        {
            this.player = player;
            Size = new Vector2(128, 80);
            Texture = Game.Content.Load<Texture2D>("Images/EnemyShip");
            
            explosionSound = Game.Content.Load<SoundEffect>("SoundEffects/AsteroidExplosion");
        }

        private void Shoot()
        {
            var missile = new EnemyMissile(scene, spriteBatch, player);
            var y = player.Position.Y + player.Size.Y / 2 - missile.Size.Y / 2;
            missile.Position = new Vector2(Position.X - missile.Size.X, y);
            scene.AddEntity(missile);
            HasShot = true;
        }
        public override void Update(GameTime gameTime)
        {
            var seconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);

            if (invincible) invincibleCounter += seconds;
            
            if (!Waiting)
            {
                Velocity = !HasShot ? new Vector2(-MoveSpeed, 0) : new Vector2(MoveSpeed, 0);
            }
            else
            {
                Velocity = Vector2.Zero;
            }
            Move(gameTime);

            if (Position.X + Size.X < ScreenSize.X)
            {
                invincible = false;
            }
            
            if (!HasShot)
            {
                var posY = player.Position.Y + player.Size.Y / 2 - Size.Y / 2;
                Position = new Vector2(Position.X, posY);
            }
            
            if (!HasShot && Position.X < ScreenSize.X - MoveDistance)
            {
                Waiting = true;
            }
            
            if (HasShot && Position.X > ScreenSize.X)
            {
                Destroy();
            }

            if (Waiting && waitTimer.Update((float) gameTime.ElapsedGameTime.TotalSeconds))
            {
                if (!HasShot)
                {
                    Shoot();
                    waitTimer.Reset();
                }
                else
                {
                    Waiting = false;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            var color = Color.White;

            if (invincible && ((int) (invincibleCounter % 1 * 10)) % 2 == 0)
            {
                color *= 0;
            }
            
            spriteBatch.Draw(Texture, Bounds, null, color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            // spriteBatch.DrawRectangle(GraphicsDevice, Bounds, color);

            spriteBatch.End();
        }
        
        public override void Kill()
        {
            if (invincible) return;
            
            if (!HasShot)
            {
                Shoot();
            }
            var side = Math.Max(Size.X, Size.Y);
            var posY = Position.Y + Size.Y / 2 - side / 2;
            scene.AddEntity(new Explosion2(scene, spriteBatch)
            {
                Position = new Vector2(Position.X, posY) - new Vector2(side) / 2,
                Size = new Vector2(side) * 2
            });
            explosionSound.Play(1, 0, 0);
            base.Kill();
        }
    }
}