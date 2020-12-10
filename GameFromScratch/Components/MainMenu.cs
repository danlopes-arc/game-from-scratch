using System.Collections.Generic;
using System.Linq;
using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Components
{
    public class MainMenu : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private List<string> items;
        private List<Rectangle> boundList;
        private Vector2 position;
        private int width = -1;


        public int Displacement { get; set; }
        public int Index { get; set; }

        public int Width
        {
            get
            {
                if (width >= 0) return width;

                foreach (var item in items)
                {
                    var x = font.MeasureString(item).X;
                    if (x > width) width = (int) x;
                }

                return width;
            }
        }

        public MainMenu(SpriteBatch spriteBatch, Game game, List<string> items, Vector2 position) : base(game)
        {
            this.items = items;
            this.position = position;
            this.spriteBatch = spriteBatch;
            font = Game.Content.Load<SpriteFont>("Fonts/ScreenInfo");
        }

        private void CreateList()
        {
            var heignt = font.LineSpacing;
            var width = 0;
            foreach (var item in items)
            {
                var x = font.MeasureString(item).X;
                if (x > width) width = (int) x;
            }

            boundList = new List<Rectangle>();
            for (int i = 0; i < items.Count; i++)
            {
                boundList.Add(new Rectangle(position.ToPoint(), new Point(width + Displacement, heignt * i)));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (BetterKeyboardState.IsJustDown(Keys.Down) || BetterKeyboardState.IsJustDown(Keys.S))
            {
                if (++Index > items.Count - 1) Index = 0;
            }

            if (BetterKeyboardState.IsJustDown(Keys.Up) || BetterKeyboardState.IsJustDown(Keys.W))
            {
                if (--Index < 0) Index = items.Count - 1;
            }

            if (BetterMouseState.Displacement != Point.Zero)
            {
                if (BetterMouseState.Current.X >= position.X && BetterMouseState.Current.X < position.X + Width)
                {
                    var i = (int) ((BetterMouseState.Current.Y - position.Y) / font.LineSpacing);
                    if (i >= 0 && i < items.Count)
                        Index = i;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();

            for (int i = 0; i < items.Count; i++)
            {
                var color = Color.White;
                if (i == Index)
                {
                    color = Color.DarkBlue;
                    spriteBatch.DrawString(font, items[i], position + new Vector2(Displacement, font.LineSpacing * i + Displacement), Color.Black * .33f);
                }
                spriteBatch.DrawString(font, items[i], position + new Vector2(0, font.LineSpacing * i), color);
            }

            spriteBatch.End();
        }
    }
}