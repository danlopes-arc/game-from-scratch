using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetDefender.Components;
using PlanetDefender.Extensions;
using PlanetDefender.Utils;

namespace PlanetDefender.Scenes.Stages
{
    public class AboutScene : GameScene
    {
        private SpriteFont font;

        public AboutScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            components.Add(new ScrollingBackground(game, spriteBatch));
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputEnabled && BetterKeyboardState.IsJustDown(Keys.Escape))
            {
                gameMain.ShowStart();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            var richText = new RichString() +
                           "Created by\n\n" +
                           "Daniel Lopes Arcanjo\n" +
                           "Paulo Barbosa";
            
            var x = Game.GraphicsDevice.Viewport.Width / 2f - font.MeasureString(richText.ToString()).X / 2;
            var y = Game.GraphicsDevice.Viewport.Height / 2f - font.MeasureString(richText.ToString()).Y / 2;
            spriteBatch.DrawRichString(font, richText, new Vector2(x, y));
            
            richText = new RichString() +
                           "Press " +
                           ("ESC ", Color.Cyan) +
                           "to go back";

            x = Game.GraphicsDevice.Viewport.Width / 2f - font.MeasureString(richText.ToString()).X / 2;
            y = Game.GraphicsDevice.Viewport.Height - font.MeasureString(richText.ToString()).Y -20;
            spriteBatch.DrawRichString(font, richText, new Vector2(x, y));

            spriteBatch.End();
        }
    }
}