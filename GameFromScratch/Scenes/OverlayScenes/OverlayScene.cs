using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Scenes.OverlayScenes
{
    public abstract class OverlayScene : GameScene
    {
        public GameScene MainScene { get; set; }
        
        public OverlayScene(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }
        
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            MainScene?.Draw(gameTime);
        }
    }
}