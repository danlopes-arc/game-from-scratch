using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlanetDefender.Components
{
    public class ScrollingBackground : DrawableGameComponent
    {
        private const float PlanetSpeed = 20;
        private const float StarsSpeed = 10;
        private const int PlanetSize = 150;

        private readonly int width;
        private readonly int height;
        private readonly float starScale;
        private readonly int starCount;

        private SpriteBatch spriteBatch;
        private Texture2D starTexture;
        private Texture2D[] planetTextures;

        private Vector2[] starPositions;
        private int currentStar;

        private Vector2 planetPosition;
        private int currentPlanet;
        
        private Random r = new Random();
        public ScrollingBackground(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            starTexture = game.Content.Load<Texture2D>("Images/StarsBackground");

            width = game.GraphicsDevice.Viewport.Width;
            height = game.GraphicsDevice.Viewport.Height;
            starScale = (float) height / starTexture.Height;
            starCount = (int) Math.Ceiling(width / (starTexture.Width * starScale)) +1;

            starPositions = new Vector2[starCount];

            for (int i = 0; i < starCount; i++)
            {
                starPositions[i] = new Vector2(i * starTexture.Width * starScale, 0);
            }

            planetTextures = new[]
            {
                game.Content.Load<Texture2D>("Images/TerranPlanet"),
                game.Content.Load<Texture2D>("Images/BarrenPlanet"),
                game.Content.Load<Texture2D>("Images/IcePlanet"),
                game.Content.Load<Texture2D>("Images/LavaPlanet"),
            };
            
            planetPosition = new Vector2(width, r.Next(height - PlanetSize));
        }

        public override void Update(GameTime gameTime)
        {
            var seconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            for (int i = 0; i < starPositions.Length; i++)
            {
                var position = starPositions[i];
                starPositions[i] = new Vector2(position.X - StarsSpeed * seconds, 0);
                if (currentStar == i && starPositions[i].X + starTexture.Width * starScale < 0)
                {
                    var previousIndex = i - 1;
                    if (previousIndex < 0) previousIndex = starCount - 1;
                    
                    starPositions[i] = new Vector2(starPositions[previousIndex].X + starTexture.Width * starScale, 0);
                    if (++currentStar == starCount) currentStar = 0;
                }
            }

            planetPosition += new Vector2(-PlanetSpeed * seconds, 0);
            if (planetPosition.X + PlanetSize < 0)
            {
                if (++currentPlanet == planetTextures.Length) currentPlanet = 0;
                
                planetPosition = new Vector2(width, r.Next(height - PlanetSize));
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            
            spriteBatch.Begin();
            
            foreach (var position in starPositions)
            {
                var destRect = new Rectangle(position.ToPoint(), (starTexture.Bounds.Size.ToVector2() * starScale).ToPoint());
                spriteBatch.Draw(starTexture, destRect, Color.White);
            }
            
            var planetDestRect = new Rectangle(planetPosition.ToPoint(), new Point(PlanetSize));
            spriteBatch.Draw(planetTextures[currentPlanet], planetDestRect, new Color(50,50,50));
            
            spriteBatch.End();
        }
    }
}