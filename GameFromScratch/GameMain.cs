using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameFromScratch
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<GameScene> stages = new List<GameScene>();
        private GameOverScene gameOverScene;
        private StartScene startScene;
        private CongratulationScene congratulationScene;
        private int stageIndex = 0;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this, spriteBatch);
            congratulationScene = new CongratulationScene(this, spriteBatch);
            Components.Add(startScene);
            Components.Add(congratulationScene);
            startScene.Show();
            ResetStages();
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
            stages = new List<GameScene>();
            var stage1 = new Stage1(this, spriteBatch);
            stages.Add(stage1);
            Components.Add(stage1);
        }

        public void ShowGameOverScene(GameScene mainScene)
        {
            HideAllScenes();
            gameOverScene = new GameOverScene(this, spriteBatch, mainScene);
            Components.Add(gameOverScene);
            gameOverScene.Show();
        }

        public void ShowStartScene()
        {
            HideAllScenes();
            if (gameOverScene != null)
            {
                Components.Remove(gameOverScene);
                gameOverScene.Hide();
                gameOverScene = null;
                ResetStages();
            }
            startScene.Show();
        }
        public void ShowCongratulationScene()
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
                ShowCongratulationScene();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            BetterKeyboardState.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
