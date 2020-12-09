using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GameFromScratch.Scenes.OverlayScenes;
using GameFromScratch.Scenes.Stages;
using GameFromScratch.Utils;

namespace GameFromScratch
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private List<Stage> stages = new List<Stage>();
        
        private StartScene startScene;
        private CongratulationScene congratulationScene;
        
        private GameOverScene gameOverScene;
        private PauseScene pauseScene;

        private GameScene pausedScene;
        
        private int stageIndex;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            startScene = new StartScene(this, spriteBatch);
            congratulationScene = new CongratulationScene(this, spriteBatch);
            
            gameOverScene = new GameOverScene(this, spriteBatch);
            pauseScene = new PauseScene(this, spriteBatch);

            Components.Add(startScene);
            Components.Add(congratulationScene);
            
            Components.Add(gameOverScene);
            Components.Add(pauseScene);

            ResetStages();
            startScene.Show();
        }

        private void HideAllScenes()
        {
            foreach (var component in Components)
            {
                if (component is GameScene)
                {
                    var scene = component as GameScene;
                    scene.Hide();
                }
            }
        }

        private void ResetStages()
        {
            stages.ForEach(s => Components.Remove(s));
            stages = new List<Stage>();
            var stage1 = new Stage1(this, spriteBatch);
            stages.Add(stage1);
            Components.Add(stage1);
        }

        public void ShowGameOver(GameScene mainScene)
        {
            HideAllScenes();
            ResetStages();
            gameOverScene.MainScene = mainScene;
            gameOverScene.Show();
        }
        
        public void Pause(GameScene mainScene)
        {
            HideAllScenes();
            pauseScene.MainScene = mainScene;
            pausedScene = mainScene;
            pauseScene.Show();
        }
        
        public void Resume()
        {
            HideAllScenes();
            pauseScene.MainScene = null;
            pausedScene.Show();
            pausedScene = null;
        }

        public void ShowStart()
        {
            HideAllScenes();
            startScene.Show();
        }

        public void ShowCongratulation()
        {
            HideAllScenes();
            congratulationScene.Show();
            ResetStages();
        }

        public void ShowNextStage()
        {
            HideAllScenes();
            if (++stageIndex == stages.Count)
            {
                ShowCongratulation();
                stageIndex = 0;
                return;
            }

            stages[stageIndex].Show();
        }

        public void ShowStage()
        {
            HideAllScenes();
            stages[stageIndex].Show();
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
            BetterKeyboardState.Update();
            BetterMouseState.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}