using System;
using System.Collections.Generic;
using GameFromScratch.Entities;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Scenes.Stages
{
    class Stage1 : Stage
    {
        private Player player;
        private SpriteFont font;
        private int destroyedAsteroids = 0;
        private int asteroidCount;
        private AsteroidSpawner asteroidSpawner;

        public Stage1(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
            
            asteroidSpawner = new AsteroidSpawner(game, this, spriteBatch)
            {
                SpawnRate = 1,
                SpawnTime = 0.5f
            };
            components.Add(asteroidSpawner);
            
            player = player = new Player(this, spriteBatch)
            {
                Health = 3
            };
            player.Position = new Vector2(10, GraphicsDevice.Viewport.Height / 2 - player.Size.Y / 2);

            AddEntity(player);
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
            }
            base.RemoveEntity(entity);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (player.Health == 0)
            {
                gameMain.ShowGameOver(this);
            }

            if (destroyedAsteroids == 10)
            {
                gameMain.ShowNextStage();
            }

            if (BetterKeyboardState.IsJustDown(Keys.A))
            {
                var r = new Random();

                var asteroid = new Asteroid(this, spriteBatch)
                {
                    Velocity = new Vector2(-r.Next(100, 150), 0)
                };

                var height = r.Next(GraphicsDevice.Viewport.Height - (int)asteroid.Size.Y);
                asteroid.Position = new Vector2(GraphicsDevice.Viewport.Width, height);
                AddEntity(asteroid);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            var text = $"Asteroids: {asteroidCount} >> Destroyed: {destroyedAsteroids} >> Health: {player.Health}";
            spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);

            spriteBatch.End();
        }
    }
}
