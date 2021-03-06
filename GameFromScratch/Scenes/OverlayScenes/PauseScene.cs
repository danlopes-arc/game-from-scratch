﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetDefender.Extensions;
using PlanetDefender.Utils;

namespace PlanetDefender.Scenes.OverlayScenes
{
    public class PauseScene : OverlayScene
    {
        private SpriteFont font;

        public PauseScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputEnabled && (BetterKeyboardState.IsJustDown(Keys.Enter) ||
                                 BetterKeyboardState.IsJustDown(Keys.Escape)))
            {
                gameMain.Resume();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawFillRectangle(GraphicsDevice, Bounds, Color.Black * .5f);
            var text = $"PAUSED{Environment.NewLine}" +
                       $"Press ENTER or ESC to resume";
            var x = Game.GraphicsDevice.Viewport.Width / 2f - font.MeasureString(text).X / 2;
            var y = Game.GraphicsDevice.Viewport.Height / 2f - font.MeasureString(text).Y / 2;
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.White);

            spriteBatch.End();
        }
    }
}