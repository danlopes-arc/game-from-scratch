using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Scenes.OverlayScenes
{
    public abstract class OverlayScene : GameScene
    {
        protected GameScene mainScene;
        
        public OverlayScene(GameMain game, SpriteBatch spriteBatch, GameScene mainScene) : base(game, spriteBatch)
        {
            this.mainScene = mainScene;
        }
        
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            mainScene.Draw(gameTime);
        }
    }
}