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
    public class Player : Entity
    {
        private const float MoveSpeed = 200;
        private bool invincible;
        private SoundEffect explosionSound;
        private SoundEffect hitSound;
        public bool Invincible
        {
            get => invincible;
            set
            {
                invincible = value;
                if (value)
                {
                    invincibleTimer.Reset();
                }
            }
        }

        private Explosion2 explosionAnimation;
        
        private Counter invincibleTimer = new Counter(1);
        public float ShotInterval
        {
            get => shotTimer.Total;
            set => shotTimer.Total = value;
        }

        private Counter shotTimer;
        private int health;

        public int Health
        {
            get => health;
            set
            {
                var oldHealth = health;
                if (value >= health || !Invincible)
                {
                    health = value;
                }

                if (health < oldHealth)
                {
                    Invincible = true;
                    explosionAnimation = new Explosion2(scene, spriteBatch)
                    {
                        Position = Position - Size / 2,
                        Size = Size * 2
                    };
                    scene.AddEntity(explosionAnimation);
                    if (health == 0)
                    {
                        explosionSound.Play();
                    }
                    else
                    {
                        hitSound.Play();
                    }
                }
            }
        }

        public Player(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            Size = new Vector2(80, 80);
            shotTimer = new Counter(.3f);
            Texture = Game.Content.Load<Texture2D>("Images/SpaceShipTopView");
            explosionSound = Game.Content.Load<SoundEffect>("SoundEffects/ShipExplosion");
            hitSound = Game.Content.Load<SoundEffect>("SoundEffects/ShipHit");
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
            if (explosionAnimation != null)
            {
                if (!explosionAnimation.Destroyed)
                {
                    explosionAnimation.Position = Position - Size / 2;
                }
                else
                {
                    explosionAnimation = null;
                }
            }

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

            if (Invincible && invincibleTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                Invincible = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            var color = Color.White;
            var newBounds = Bounds;
            if (Invincible && (int)(invincibleTimer.Current / invincibleTimer.Total * 10) % 2 == 0)
            {
                color = Color.Red;
                newBounds = new Rectangle(new Point(Bounds.Location.X + 2, Bounds.Location.Y), Bounds.Size);
            }
            spriteBatch.Draw(Texture, newBounds, color);
            // spriteBatch.DrawRectangle(GraphicsDevice, Bounds, Color.Red);

            spriteBatch.End();
        }
    }
}
