using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetDefender.Components;
using PlanetDefender.Extensions;
using PlanetDefender.Utils;

namespace PlanetDefender.Scenes
{
    public class StartScene : GameScene
    {
        private SpriteFont font;
        private MainMenu menu;
        private Texture2D shipTexture;
        private const float MaxShipY = 180;
        private const float MinShipY = 160;
        private const float ShipX = 500;
        private const float ShipSpeed = 7;
        private float shipY;
        private bool raising;

        public StartScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            components.Add(new ScrollingBackground(game, spriteBatch));
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
            shipTexture = Game.Content.Load<Texture2D>("Images/YellowShip");
            menu = new MainMenu(spriteBatch,
                game,
                new List<string>() {"Play", "Help", "About", "Quit"},
                new Vector2(100, 100))
            {
                Displacement = 2
            };
            components.Add(menu);
        }

        public override void Update(GameTime gameTime)
        {
            var seconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);

            if (InputEnabled && BetterKeyboardState.IsJustDown(Keys.Enter) ||
                BetterMouseState.IsJustDown(MouseButton.Left) && menu.Intersects(BetterMouseState.Current.Position))
            {
                switch (menu.Index)
                {
                    case 0:
                        gameMain.ShowStage();
                        break;
                    case 1:
                        gameMain.ShowHelp();
                        break;
                    case 3:
                        Game.Exit();
                        break;
                }
            }

            if (raising)
            {
                shipY += ShipSpeed * seconds;
                if (shipY > MaxShipY)
                {
                    shipY = MaxShipY;
                    raising = false;
                }
            }
            else
            {
                shipY -= ShipSpeed * seconds;
                if (shipY < MinShipY)
                {
                    shipY = MinShipY;
                    raising = true;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            var richText = new RichString() +
                           "Press " +
                           ("Up/Down ", Color.Cyan) +
                           "or use the " +
                           ("MOUSE ", Color.Cyan) +
                           "to navigate\nPress " +
                           ("ENTER ", Color.Cyan) +
                           "or " +
                           ("CLICK ", Color.Cyan) +
                           "to select";

            spriteBatch.DrawRichString(font, richText,
                new Vector2(100, Game.GraphicsDevice.Viewport.Height - font.MeasureString(richText.ToString()).Y - 40));

            var destRect = new Rectangle((int) ShipX, (int) shipY, (int) (shipTexture.Width * 1.5f),
                (int) (shipTexture.Height * 1.5f));
            spriteBatch.Draw(shipTexture, destRect, Color.White);

            spriteBatch.End();
        }
    }
}