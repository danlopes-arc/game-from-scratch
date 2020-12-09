using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFromScratch.Scenes.Stages
{
    public abstract class Stage : GameScene
    {
        public string Title { get; set; }
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