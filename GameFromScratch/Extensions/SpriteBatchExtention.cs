using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Extensions
{
    public static class SpriteBatchExtension
    {
        public static void DrawFillRectangle(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Rectangle rectangle, Color color)
        {
            var tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData(new[] { Color.White });

            spriteBatch.Draw(tex, rectangle, color);
        }
        public static void DrawRectangle(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Rectangle rectangle, Color color, int borderWidth = 1)
        {
            var tex = new Texture2D(graphicsDevice, 1, 1);
            tex.SetData(new[] { Color.White });

            spriteBatch.Draw(tex, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, borderWidth), color);
            spriteBatch.Draw(tex, new Rectangle(rectangle.X + rectangle.Width - borderWidth, rectangle.Y, borderWidth, rectangle.Height), color);
            spriteBatch.Draw(tex, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - borderWidth, rectangle.Width, borderWidth), color);
            spriteBatch.Draw(tex, new Rectangle(rectangle.X, rectangle.Y, borderWidth, rectangle.Height), color);
        }
    }
}
