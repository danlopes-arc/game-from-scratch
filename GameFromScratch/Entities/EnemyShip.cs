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
        private const float MoveSpeed = 100;
        private const float MoveDistance = 100;
        
        private SoundEffect explosionSound;
        private Explosion2 explosionAnimation;

        private Counter waitTimer = new Counter(2);
        public bool HasShot { get; private set; }
        public bool Waiting { get; private set; }

        public EnemyShip(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            Size = new Vector2(80, 80);
            // Texture = Game.Content.Load<Texture2D>("Images/SpaceShipTopView");
            // explosionSound = Game.Content.Load<SoundEffect>("SoundEffects/ShipExplosion");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Waiting)
            {
                Velocity = !HasShot ? new Vector2(-MoveSpeed, 0) : new Vector2(MoveSpeed, 0);
            }
            else
            {
                Velocity = Vector2.Zero;
            }
            Move(gameTime);

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
                    // TODO: shoot
                    HasShot = true;
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

            // spriteBatch.Draw(Texture, Bounds, Color.White);
            spriteBatch.DrawRectangle(GraphicsDevice, Bounds, Color.Red);

            spriteBatch.End();
        }
    }
}