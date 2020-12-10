using System;
using GameFromScratch.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Components
{
    public class InfoBar : DrawableGameComponent
    {
        public const int Height = 40;

        private SpriteBatch spriteBatch;
        private SpriteFont font;
        
        public int Health { get; set; }
        public int Destroyed { get; set; }
        public int Missed { get; set; }
        public int Time { get; set; }
        public int  Score { get; set; }
        
        public InfoBar(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            
            var shape = new Rectangle(0,0,GraphicsDevice.Viewport.Width, Height);
            
            spriteBatch.DrawFillRectangle(GraphicsDevice, shape, Color.Black);
            
            // var text =
            //     $"Health: {player.Health}{Environment.NewLine}" +
            //     $"Destroyed: {destroyedAsteroids}{Environment.NewLine}" +
            //     $"Missed: {missedAsteroids}{Environment.NewLine}" +
            //     $"Time: {(int) stageCounter.Remaining}s";

            const int y = 8;
            var healthText = Health.ToString().PadLeft(2, '_');
            var destroyedText = Destroyed.ToString().PadLeft(2, '_');
            var missedText = Missed.ToString().PadLeft(2, '_');
            var timeText = Time.ToString().PadLeft(2, '_');
            var scoreText = Score.ToString().PadLeft(4, '0');
            var text =
                $"Health {healthText}  " +
                $"Destroyed {destroyedText}  " +
                $"Missed {missedText}  " +
                $"Time {timeText}s  " +
                $"Score {scoreText}";
            
            spriteBatch.DrawString(font, text, new Vector2(10, y), Color.White);
            
            spriteBatch.End();
        }
    }
}