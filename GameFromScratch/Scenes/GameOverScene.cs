using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFromScratch.Scenes
{
    class GameOverScene : GameScene
    {
        private GameScene mainScene;
        private SpriteFont font;

        public GameOverScene(GameMain game, SpriteBatch spriteBatch, GameScene mainScene) : base(game, spriteBatch)
        {
            this.mainScene = mainScene;
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (BetterKeyboardState.IsJustDown(Keys.Enter))
            {
                gameMain.ShowStartScene();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            mainScene.Draw(gameTime);

            spriteBatch.Begin();
            var text = $"GAME OVER{Environment.NewLine}" +
                $"Press Enter to continue";
            var x = Game.GraphicsDevice.Viewport.Width / 2 - font.MeasureString(text).X / 2;
            var y = Game.GraphicsDevice.Viewport.Height / 2 - font.MeasureString(text).Y / 2;
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.White);

            spriteBatch.End();
        }
    }
}
