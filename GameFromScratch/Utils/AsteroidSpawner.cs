using System;
using GameFromScratch.Entities;
using GameFromScratch.Scenes.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Utils
{
    public class AsteroidSpawner : GameComponent
    {
        private const float NormalMinVelocity = .8f;
        private const float NormalMaxVelocity = 1.2f;
        private const float FastMinVelocity = 1.6f;
        private const float FastMaxVelocity = 2;

        private Stage stage;
        private SpriteBatch spriteBatch;
        private Random r = new Random();
        private Counter counter;

        public float Delay
        {
            get => counter.Total;
            set => counter.Total = value;
        }

        public int Rate { get; set; }
        public float BaseVelocity { get; set; }
        
        public AsteroidSpawner(Game game, Stage stage, SpriteBatch spriteBatch, float spawnTime) : base(game)
        {
            this.stage = stage;
            this.spriteBatch = spriteBatch;
            counter = new Counter(spawnTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (!counter.Update((float)gameTime.ElapsedGameTime.TotalSeconds)) return;

            for (int i = 0; i < Rate; i++)
            {
                var xVelocity = -r.Next((int)(BaseVelocity * NormalMinVelocity), (int) (BaseVelocity *
                    NormalMinVelocity));
                if (r.Next(0, 4) == 0)
                {
                    xVelocity = -r.Next((int)(BaseVelocity * FastMinVelocity), (int) (BaseVelocity *
                        FastMinVelocity));
                }

                var asteroid = new Asteroid(stage, spriteBatch)
                {
                    Velocity = new Vector2(xVelocity, 0)
                };

                var height = r.Next(stage.GraphicsDevice.Viewport.Height - (int) asteroid.Size.Y);
                asteroid.Position = new Vector2(stage.GraphicsDevice.Viewport.Width, height);
                stage.AddEntity(asteroid);
            }
            
            counter.Reset();
        }
    }
}