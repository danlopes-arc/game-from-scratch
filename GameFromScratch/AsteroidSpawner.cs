using System;
using GameFromScratch.Entities;
using GameFromScratch.Scenes.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch
{
    public class AsteroidSpawner : GameComponent
    {
        private const int NormalMinVelocity = 100;
        private const int NormalMaxVelocity = 150;
        private const int FastMinVelocity = 250;
        private const int FastMaxVelocity = 300;
        
        private Stage stage;
        private float counter;
        private SpriteBatch spriteBatch;
        private Random r = new Random();

        public float SpawnTime { get; set; } = 0;
        public float SpawnRate { get; set; } = 0;

        public AsteroidSpawner(Game game, Stage stage, SpriteBatch spriteBatch) : base(game)
        {
            this.stage = stage;
            this.spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            counter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (counter < SpawnTime) return;
            
            for (int i = 0; i < SpawnRate; i++)
            {
                var xVelocity = -r.Next(NormalMinVelocity, NormalMaxVelocity);
                if (r.Next(0, 4) == 0)
                {
                    xVelocity = -r.Next(FastMinVelocity, FastMaxVelocity);
                }
                var asteroid = new Asteroid(stage, spriteBatch)
                {
                    Velocity = new Vector2(xVelocity, 0)
                };

                var height = r.Next(stage.GraphicsDevice.Viewport.Height - (int) asteroid.Size.Y);
                asteroid.Position = new Vector2(stage.GraphicsDevice.Viewport.Width, height);
                stage.AddEntity(asteroid);
            }

            counter = 0;
        }
    }
}