using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Scenes.Stages
{
    public class Stage : GameScene
    {
        public Stage(GameMain game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }
        
        public virtual void Pause()
        {
            Enabled = true;
        }

        public virtual void Resume()
        {
            Enabled = false;
        }
    }
}