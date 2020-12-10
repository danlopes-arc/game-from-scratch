using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Scenes.Stages
{
    public abstract class Stage : GameScene
    {
        private Counter titleTimer = new Counter(1.5f);
        
        public virtual int Score { get; } = 0;
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

        public override void Update(GameTime gameTime)
        {
            if (InputEnabled && BetterKeyboardState.IsJustDown(Keys.Escape))
            {
                gameMain.Pause(this);
            }
            base.Update(gameTime);
        }
    }
}