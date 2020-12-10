﻿using System;
using System.Collections.Generic;
using GameFromScratch.Components;
using GameFromScratch.Entities;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Scenes.Stages
{
    public class Stage1 : Stage
    {
        private Player player;
        private SpriteFont font;
        private int destroyedAsteroids;
        private int missedAsteroids;
        private int asteroidCount;
        private AsteroidSpawner asteroidSpawner;
        private float stageTime = 30;
        private Counter stageCounter;
        private InfoBar infoBar;

        public override int Score => destroyedAsteroids * 100 - missedAsteroids * 50;

        public override Rectangle Bounds => new Rectangle(0, InfoBar.Height, GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height - InfoBar.Height);

        public Stage1(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
            Title = "Stage 1";

            asteroidSpawner = new AsteroidSpawner(game, this, spriteBatch, .5f);

            components.Add(asteroidSpawner);

            player = new Player(this, spriteBatch)
            {
                Health = 3
            };
            player.Position = new Vector2(10, GraphicsDevice.Viewport.Height / 2f - player.Size.Y / 2);

            AddEntity(player);

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
                else
                {
                    missedAsteroids++;
                }
            }

            base.RemoveEntity(entity);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (player.Health == 0)
            {
                gameMain.ShowGameOver(this);
                return;
            }

            if (stageCounter.Update((float) gameTime.ElapsedGameTime.TotalSeconds))
            {
                asteroidSpawner.Rate = 0;
                if (asteroidCount == 0)
                {
                    gameMain.ShowSummary(this);
                    return;
                }
            }

            if (!stageCounter.Done)
            {
                var percentLevel = stageCounter.Current / stageCounter.Total;
                asteroidSpawner.Delay = 2 - percentLevel * 1.5f;
                asteroidSpawner.Rate = 1 + (int) (percentLevel * 3);
                asteroidSpawner.BaseVelocity = 100 + percentLevel * 100;
            }

            infoBar.Destroyed = destroyedAsteroids;
            infoBar.Missed = missedAsteroids;
            infoBar.Health = player.Health;
            infoBar.Time = (int) stageCounter.Remaining;

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

            // spriteBatch.Begin();
            // var text =
            //     $"Health: {player.Health}{Environment.NewLine}" +
            //     $"Destroyed: {destroyedAsteroids}{Environment.NewLine}" +
            //     $"Missed: {missedAsteroids}{Environment.NewLine}" +
            //     $"Time: {(int) stageCounter.Remaining}s";
            // var x = ScreenSize.X - 180;
            // spriteBatch.DrawString(font, text, new Vector2(x, 10), Color.White);

            // spriteBatch.End();
        }
    }
}