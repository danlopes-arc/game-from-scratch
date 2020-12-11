using System;
using GameFromScratch.Extensions;
using GameFromScratch.Utils;
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
        public int Score { get; set; }

        public InfoBar(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;

            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();

            var shape = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, Height);

            spriteBatch.DrawFillRectangle(GraphicsDevice, shape, Color.Black * .2f);

            const int y = 8;

            var richText = new RichString() + "Health: " + (Health.ToString(), Color.Pink);
            spriteBatch.DrawRichString(font, richText, new Vector2(10, y));

            richText = new RichString() + "Score: " + (Score.ToString());
            var pos = font.MeasureString(richText.ToString());
            spriteBatch.DrawRichString(font, richText, new Vector2(GraphicsDevice.Viewport.Width / 2f - pos.X / 2, y));

            richText = new RichString() + "Time: " + (Time.ToString(), Color.Cyan) + "s";
            pos = font.MeasureString(richText.ToString());
            spriteBatch.DrawRichString(font, richText, new Vector2(GraphicsDevice.Viewport.Width - pos.X - 10, y));

            spriteBatch.End();
        }
    }
}