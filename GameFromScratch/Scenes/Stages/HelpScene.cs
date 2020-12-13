using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetDefender.Components;
using PlanetDefender.Extensions;
using PlanetDefender.Utils;

namespace PlanetDefender.Scenes.Stages
{
    public class HelpScene : GameScene
    {
        
        private SpriteFont helpFont;
        private SpriteFont escFont;

        public HelpScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            components.Add(new ScrollingBackground(game, spriteBatch));
            helpFont = Game.Content.Load<SpriteFont>("Fonts/SmallScreenInfo");
            escFont = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
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
                           "You are part of the Planet Defender Team, and as such you must protect Earth from\n" +
                           "any sort of threat.\n" +
                           "\n" +
                           ("> ", Color.Cyan) + "Control the spaceship with the mouse and shoot missiles with left mouse button\n" +
                           ("> ", Color.Cyan) + "You must survive for 30s and destroy/avoid all enemies to complete the level\n" +
                           ("> ", Color.Cyan) + "You start every stage with 3 health points(HP). It's game over if you reach 0 HP\n" +
                           "\n" +
                           ("Stage 1:\n", Color.Cyan) +
                           ("> ", Color.Cyan) + "There is a group of asteroids going towards earth!\n" +
                           ("> ", Color.Cyan) + "If an asteroid hits you, you lose 1 HP\n" +
                           "\n" +
                           ("Stage 2:\n", Color.Cyan) +
                           ("> ", Color.Cyan) + "It turns out that an alien fleet was the responsible for the asteroids and they\n" +
                           "want to invade Earth!\n" +
                           ("> ", Color.Cyan) + "Alongside with the asteroids, alien ships shoot at you\n" +
                           ("> ", Color.Cyan) + ("Caution!", Color.OrangeRed)+ " The alien missile can kill you instantly!\n" +
                           "\n" +
                           ("Score:\n", Color.Cyan) +
                           ("> ", Color.Cyan) + "Asteroids:   100 pts\n" +
                           ("> ", Color.Cyan) + "Alien Ships: 500 pts";
            
            var x = Game.GraphicsDevice.Viewport.Width / 2f - helpFont.MeasureString(richText.ToString()).X / 2;
            var y = Game.GraphicsDevice.Viewport.Height / 2f - helpFont.MeasureString(richText.ToString()).Y / 2;
            spriteBatch.DrawRichString(helpFont, richText, new Vector2(x, y));
            
            richText = new RichString() +
                           "Press " +
                           ("ESC ", Color.Cyan) +
                           "to go back";

            x = Game.GraphicsDevice.Viewport.Width - escFont.MeasureString(richText.ToString()).X - 20;
            y = Game.GraphicsDevice.Viewport.Height - escFont.MeasureString(richText.ToString()).Y -20;
            spriteBatch.DrawRichString(escFont, richText, new Vector2(x, y));

            spriteBatch.End();
        }
    }
}