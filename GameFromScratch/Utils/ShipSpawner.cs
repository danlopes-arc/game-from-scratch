using System;
using GameFromScratch.Entities;
using GameFromScratch.Scenes.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Utils
{
    public class ShipSpawner : GameComponent
    {
        private EnemyShip ship;
        private Counter shipTimer;
        private SpriteBatch spriteBatch;
        private Stage stage;
        private Random r = new Random();
        
        public ShipSpawner(Game game, Stage stage, SpriteBatch spriteBatch) : base(game)
        {
            this.stage = stage;
            this.spriteBatch = spriteBatch;
            shipTimer = new Counter(3);
        }

        public override void Update(GameTime gameTime)
        {
            var seconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);

            if (ship != null)
            {
                if (ship.Destroyed)
                {
                    ship = null;
                    shipTimer.Reset();
                }
                return;
            }
            
            if (!shipTimer.Update(seconds))
            {
                return;
            }

            ship = new EnemyShip(stage, spriteBatch);
            var height = r.Next(stage.Bounds.Top ,stage.Bounds.Bottom - (int) ship.Size.Y);
            ship.Position = new Vector2(stage.Bounds.Width, height);
            stage.AddEntity(ship);
        }
    }
}