using System;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Extensions
{
    public static class SpriteBatchExtension
    {
        public static void DrawFillRectangle(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice,
            Rectangle rectangle, Color color)
        {
            var tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData(new[] {Color.White});

            spriteBatch.Draw(tex, rectangle, color);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice,
            Rectangle rectangle, Color color, int borderWidth = 1)
        {
            var tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData(new[] {Color.White});

            spriteBatch.Draw(tex, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, borderWidth), color);
            spriteBatch.Draw(tex,
                new Rectangle(rectangle.X + rectangle.Width - borderWidth, rectangle.Y, borderWidth, rectangle.Height),
                color);
            spriteBatch.Draw(tex,
                new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - borderWidth, rectangle.Width, borderWidth),
                color);
            spriteBatch.Draw(tex, new Rectangle(rectangle.X, rectangle.Y, borderWidth, rectangle.Height), color);
        }

        public static void DrawRichString(this SpriteBatch spriteBatch, SpriteFont font, RichString richString,
            Vector2 position)
        {
            var (x, y) = position;
            for (int i = 0; i < richString.Count; i++)
            {
                var width = font.MeasureString(richString.Strings[i]).X;
                var color = richString.Colors[i];
                
                var texts = richString.Strings[i]
                    .Split(new[] {Environment.NewLine, "\n", "\r\n"}, StringSplitOptions.None);
                for (var j = 0; j < texts.Length; j++)
                {
                    var text = texts[j];
                    spriteBatch.DrawString(font, text, new Vector2(x, y), color);
                    if (texts.Length > 1 && j < texts.Length - 1)
                    {
                        y += font.LineSpacing;
                        x = position.X;
                    }
                    else
                    {
                        x += font.MeasureString(text).X;
                    }
                }
            }
        }
    }
}