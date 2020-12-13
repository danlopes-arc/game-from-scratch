using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetDefender.Components;
using PlanetDefender.Extensions;
using PlanetDefender.Utils;

namespace PlanetDefender.Scenes
{
    public class CongratulationScene : GameScene
    {
        private SpriteFont font;

        public CongratulationScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            components.Add(new ScrollingBackground(game, spriteBatch));
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputEnabled && BetterKeyboardState.IsJustDown(Keys.Enter))
            {
                gameMain.ShowStart();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            var richText = new RichString() +
                           "CONGRATULATIONS!!!\n\nYou beat the game!\n\nPress " +
                           ("ENTER ", Color.Cyan) +
                           "to continue";
            
            var x = Game.GraphicsDevice.Viewport.Width / 2f - font.MeasureString(richText.ToString()).X / 2;
            var y = Game.GraphicsDevice.Viewport.Height / 2f - font.MeasureString(richText.ToString()).Y / 2;
            spriteBatch.DrawRichString(font, richText, new Vector2(x, y));

            spriteBatch.End();
        }
    }
}