using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFromScratch.Scenes
{
    class StartScene : GameScene
    {
        private SpriteFont font;
        public StartScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (BetterKeyboardState.IsJustDown(Keys.Space))
            {
                gameMain.ShowStage();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            var text = $"SATRT GAME{Environment.NewLine}" +
                $"Press Space to Start";
            var x = Game.GraphicsDevice.Viewport.Width / 2 - font.MeasureString(text).X / 2;
            var y = Game.GraphicsDevice.Viewport.Height / 2 - font.MeasureString(text).Y / 2;
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.White);

            spriteBatch.End();
        }
    }
}
