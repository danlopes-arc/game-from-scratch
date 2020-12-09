using System;
using GameFromScratch.Extensions;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Scenes.OverlayScenes
{
    public class PauseScene : OverlayScene
    {
        private SpriteFont font;

        public PauseScene(GameMain game, SpriteBatch spriteBatch, GameScene mainScene) : base(game, spriteBatch,
            mainScene)
        {
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (BetterKeyboardState.IsJustDown(Keys.Enter) || BetterKeyboardState.IsJustDown(Keys.Escape))
            {
                gameMain.Resume(mainScene, this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawFillRectangle(GraphicsDevice, Bounds, Color.Black * .5f);
            var text = $"PAUSED{Environment.NewLine}" +
                       $"Press ENTER or ESC to resume";
            var x = Game.GraphicsDevice.Viewport.Width / 2 - font.MeasureString(text).X / 2;
            var y = Game.GraphicsDevice.Viewport.Height / 2 - font.MeasureString(text).Y / 2;
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.White);

            spriteBatch.End();
        }
    }
}