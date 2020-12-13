using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlanetDefender.Entities;
using PlanetDefender.Scenes.Stages;

namespace PlanetDefender.Utils
{
    public class ShipSpawner : GameComponent
    {
        private EnemyShip ship;
        private Counter counter;
        private SpriteBatch spriteBatch;
        private Stage stage;
        private Player player;
        private Random r = new Random();
        public float Delay
        {
            get => counter.Total;
            set => counter.Total = value;
        }

        public bool Active { get; set; } = true;
        
        public ShipSpawner(Game game, Stage stage, SpriteBatch spriteBatch, Player player, float delay = 3) : base(game)
        {
            this.stage = stage;
            this.spriteBatch = spriteBatch;
            this.player = player;
            counter = new Counter(delay);
        }

        public override void Update(GameTime gameTime)
        {
            var seconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            if (!Active) return;
            
            if (ship != null)
            {
                if (ship.Destroyed)
                {
                    ship = null;
                    counter.Reset();
                }
                return;
            }
            
            if (!counter.Update(seconds))
            {
                return;
            }

            ship = new EnemyShip(stage, spriteBatch, player);
            var height = r.Next(stage.Bounds.Top ,stage.Bounds.Bottom - (int) ship.Size.Y);
            ship.Position = new Vector2(stage.Bounds.Width, height);
            stage.AddEntity(ship);
            counter.Reset();
        }
    }
}