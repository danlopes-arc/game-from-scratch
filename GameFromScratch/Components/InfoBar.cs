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

            const int y = 8;
            
            var healthText = Health.ToString();
            var text = $"Health: {healthText}";
            spriteBatch.DrawString(font, text, new Vector2(10, y), Color.White);
            
            
            var scoreText = Score.ToString().PadLeft(4, '0');
            text = $"Score: {scoreText}";
            var pos = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2(GraphicsDevice.Viewport.Width / 2f - pos.X / 2, y), Color.White);

            var timeText = Time.ToString().PadLeft(2, '0');
            text = $"Time {timeText}s";
            pos = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2(GraphicsDevice.Viewport.Width - pos.X - 10, y), Color.White);
            
            spriteBatch.End();
        }
    }
}