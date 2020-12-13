using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetDefender.Extensions;
using PlanetDefender.Utils;

namespace PlanetDefender.Scenes.OverlayScenes
{
    public class SummaryScene : OverlayScene
    {
        private SpriteFont font;

        public int StageScore { get; set; }
        public int TotalScore { get; set; }

        public SummaryScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (InputEnabled && BetterKeyboardState.IsJustDown(Keys.Enter))
            {
                gameMain.ShowNextStage();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawFillRectangle(GraphicsDevice, Bounds, Color.Black * .5f);

            var text = $"{MainScene.Title} Completed!";
            var size = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2(Bounds.Right / 2f - size.X / 2, 60), Color.White);

            text = $"Stage Score: {StageScore}{Environment.NewLine}" +
                   $"Total Score: {TotalScore}";
            size = font.MeasureString(text);
            spriteBatch.DrawString(font, text,
                new Vector2(Bounds.Right / 2f - size.X / 2, Bounds.Bottom / 2f - size.Y / 2), Color.White);

            var richText = new RichString() +
                           "Press " +
                           ("ENTER ", Color.Cyan) +
                           "to continue";
            
            var x = Game.GraphicsDevice.Viewport.Width / 2f - font.MeasureString(richText.ToString()).X / 2;
            spriteBatch.DrawRichString(font, richText, new Vector2(x, Bounds.Bottom - size.Y - 60));

            spriteBatch.End();
        }
    }
}