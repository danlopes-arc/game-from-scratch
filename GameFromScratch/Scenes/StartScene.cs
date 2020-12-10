﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using GameFromScratch.Components;
using GameFromScratch.Utils;

namespace GameFromScratch.Scenes
{
    public class StartScene : GameScene
    {
        private SpriteFont font;
        private MainMenu menu;

        public StartScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
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
            base.Update(gameTime);

            if (InputEnabled && BetterKeyboardState.IsJustDown(Keys.Enter))
            {
                gameMain.ShowStage();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            var text = $"Press Up/W or Down/S to navigate{Environment.NewLine}" +
                       $"Press ENTER to select";
            // var x = Game.GraphicsDevice.Viewport.Width / 2f - font.MeasureString(text).X / 2;
            // var y = Game.GraphicsDevice.Viewport.Height / 2f - font.MeasureString(text).Y / 2;
            spriteBatch.DrawString(font, text,
                new Vector2(100, Game.GraphicsDevice.Viewport.Height - font.MeasureString(text).Y - 20), Color.White);

            spriteBatch.End();
        }
    }
}