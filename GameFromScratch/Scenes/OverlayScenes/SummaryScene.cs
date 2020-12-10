﻿using System;
using GameFromScratch.Extensions;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Scenes.OverlayScenes
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

            text = "Press ENTER to continue";
            size = font.MeasureString(text);
            spriteBatch.DrawString(font, text,
                new Vector2(Bounds.Right / 2f - size.X / 2, Bounds.Bottom - size.Y - 60), Color.White);

            spriteBatch.End();
        }
    }
}