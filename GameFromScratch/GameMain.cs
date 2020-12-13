using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PlanetDefender.Scenes;
using PlanetDefender.Scenes.OverlayScenes;
using PlanetDefender.Scenes.Stages;
using PlanetDefender.Utils;

namespace PlanetDefender
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private List<Stage> stages = new List<Stage>();
        
        private StartScene startScene;
        private CongratulationScene congratulationScene;
        private HelpScene helpScene;
        
        private GameOverScene gameOverScene;
        private PauseScene pauseScene;
        private SummaryScene summaryScene;

        private GameScene pausedScene;
        
        private int stageIndex;
        private int totalScore;

        private Song chillSong;
        private bool isPlayingChillSong;

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
            chillSong = Content.Load<Song>("Music/ChillSong");
            startScene = new StartScene(this, spriteBatch);
            congratulationScene = new CongratulationScene(this, spriteBatch);
            helpScene = new HelpScene(this, spriteBatch);
            
            gameOverScene = new GameOverScene(this, spriteBatch);
            pauseScene = new PauseScene(this, spriteBatch);
            summaryScene = new SummaryScene(this, spriteBatch);

            Components.Add(startScene);
            Components.Add(congratulationScene);
            Components.Add(helpScene);
            
            Components.Add(gameOverScene);
            Components.Add(pauseScene);
            Components.Add(summaryScene);

            ResetStages();
            ShowStart();
            
            MediaPlayer.IsRepeating = true;
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
            totalScore = 0;
            
            var stage1 = new Stage1(this, spriteBatch);
            stages.Add(stage1);
            Components.Add(stage1);
            
            var stage2 = new Stage2(this, spriteBatch);
            stages.Add(stage2);
            Components.Add(stage2);
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
            if (!isPlayingChillSong)
            {
                MediaPlayer.Play(chillSong);
                isPlayingChillSong = true;
            }
        }
        
        public void ShowHelp()
        {
            HideAllScenes();
            helpScene.Show();
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
            
            MediaPlayer.Play(Content.Load<Song>("Music/BattleSong"));
            isPlayingChillSong = false;
        }

        public void ShowStage()
        {
            HideAllScenes();
            stages[stageIndex].Show();
            MediaPlayer.Play(Content.Load<Song>("Music/BattleSong"));
            isPlayingChillSong = false;
        }

        public void ShowSummary(Stage stage)
        {
            totalScore += stage.Score;
            summaryScene.StageScore = stage.Score;
            summaryScene.TotalScore = totalScore;
            summaryScene.MainScene = stage;
            HideAllScenes();
            summaryScene.Show();
            MediaPlayer.Play(Content.Load<Song>("Music/CongratsSong"));
            isPlayingChillSong = false;
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
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}