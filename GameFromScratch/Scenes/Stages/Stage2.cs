using System;
using System.Collections.Generic;
using System.Linq;
using GameFromScratch.Components;
using GameFromScratch.Entities;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameFromScratch.Scenes.Stages
{
    public class Stage2 : Stage
    {
        private Player player;
        private SpriteFont font;
        private int destroyedAsteroids;
        private int destroyedShips;
        private int asteroidCount;
        private AsteroidSpawner asteroidSpawner;
        private ShipSpawner shipSpawner;
        private float stageTime = 30;
        private Counter stageCounter;
        private InfoBar infoBar;
        private int enemyMissileCount;
        private int enemyShipCount;
        private SoundEffectInstance alarmSound;

        private Counter titleTimer = new Counter(1.5f);

        public override int Score => destroyedAsteroids * 100 + destroyedShips * 500;

        public override Rectangle Bounds => new Rectangle(0, InfoBar.Height, GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height - InfoBar.Height);

        public Stage2(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            components.Add(new ScrollingBackground(game, spriteBatch));
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
            alarmSound = Game.Content.Load<SoundEffect>("SoundEffects/Alarm").CreateInstance();
            alarmSound.IsLooped = true;
            alarmSound.Volume = .5f;
            
            Title = "Stage 2";

            asteroidSpawner = new AsteroidSpawner(game, this, spriteBatch, .5f);
            components.Add(asteroidSpawner);

            player = new Player(this, spriteBatch)
            {
                Health = 3
            };
            player.Position = new Vector2(10, GraphicsDevice.Viewport.Height / 2f - player.Size.Y / 2);

            AddEntity(player);
            
            shipSpawner = new ShipSpawner(game, this, spriteBatch, player);
            components.Add(shipSpawner);

            stageCounter = new Counter(stageTime);
            infoBar = new InfoBar(game, spriteBatch);
            components.Add(infoBar);
        }

        public override void AddEntity(Entity entity)
        {
            if (entity is Asteroid)
            {
                asteroidCount++;
            }
            if (entity is EnemyMissile)
            {
                enemyMissileCount++;
            }
            if (entity is EnemyShip)
            {
                enemyShipCount++;
            }
            base.AddEntity(entity);
        }

        public override void RemoveEntity(Entity entity)
        {
            if (entity is Asteroid)
            {
                asteroidCount--;
                if (entity.Killed)
                {
                    destroyedAsteroids++;
                }
            }
            if (entity is EnemyMissile)
            {
                enemyMissileCount--;
            }
            if (entity is EnemyShip)
            {
                enemyShipCount--;
                if (entity.Killed)
                {
                    destroyedShips++;
                }
            }

            base.RemoveEntity(entity);
        }

        public override void Update(GameTime gameTime)
        {
            if (enemyMissileCount > 0)
            {
                if (alarmSound.State != SoundState.Playing) alarmSound.Play();
            }
            else
            {
                if (alarmSound.State == SoundState.Playing) alarmSound.Stop();
            }
            
            if (player.Health == 0)
            {
                gameMain.ShowGameOver(this);
                return;
            }

            if (stageCounter.Update((float) gameTime.ElapsedGameTime.TotalSeconds))
            {
                asteroidSpawner.Rate = 0;
                shipSpawner.Active = false;
                if (asteroidCount == 0 && enemyMissileCount == 0 && enemyShipCount == 0)
                {
                    gameMain.ShowSummary(this);
                    return;
                }
            }

            base.Update(gameTime);

            if (!stageCounter.Done)
            {
                var percentLevel = stageCounter.Current / stageCounter.Total;
                asteroidSpawner.Delay = 2 - percentLevel * 1.5f;
                asteroidSpawner.Rate = 1 + (int) (percentLevel * 3);
                asteroidSpawner.BaseVelocity = 100 + percentLevel * 100;
            }

            infoBar.Health = player.Health;
            infoBar.Time = (int) stageCounter.Remaining;
            infoBar.Score = Score;

            // if (destroyedAsteroids == 10)
            // {
            //     gameMain.ShowNextStage();
            //     return;
            // }

            //if (BetterKeyboardState.IsJustDown(Keys.A))
            //{
            //    var r = new Random();

            //    var asteroid = new Asteroid(this, spriteBatch)
            //    {
            //        Velocity = new Vector2(-r.Next(100, 150), 0)
            //    };

            //    var height = r.Next(GraphicsDevice.Viewport.Height - (int) asteroid.Size.Y);
            //    asteroid.Position = new Vector2(GraphicsDevice.Viewport.Width, height);
            //    AddEntity(asteroid);
            //}
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!titleTimer.Update((float) gameTime.ElapsedGameTime.TotalSeconds))
            {
                spriteBatch.Begin();

                var pos = font.MeasureString(Title);

                spriteBatch.DrawString(font, Title,
                    new Vector2(ScreenSize.X / 2f - pos.X / 2, ScreenSize.Y / 2f - pos.Y / 2), Color.White);

                spriteBatch.End();
            }
        }

        public override void Mute()
        {
            base.Mute();
            if (alarmSound != null && alarmSound.State == SoundState.Playing)
            {
                alarmSound.Pause();
            }
        }
        
        public override void Unmute()
        {
            base.Unmute();
            if (alarmSound != null && alarmSound.State == SoundState.Paused)
            {
                alarmSound.Play();
            }
        }
    }
}