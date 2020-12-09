using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFromScratch.Entities;
using GameFromScratch.Utils;

namespace GameFromScratch.Scenes
{
    public abstract class GameScene : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected List<GameComponent> components = new List<GameComponent>();
        protected CollisionManager collisionManager = new CollisionManager();
        protected GameMain gameMain;

        public bool InputEnabled { get; set; }

        public virtual Rectangle Bounds => new Rectangle(0,0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        
        public Point ScreenSize {
            get => new Point(Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
        }

        public GameScene(GameMain game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            gameMain = game;
            Hide();
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
        }
        
        public override void Update(GameTime gameTime)
        {
            foreach (var component in components.ToList())
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
            collisionManager.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var component in components)
            {
                if (component is DrawableGameComponent)
                {
                    var drawableComponent = component as DrawableGameComponent;
                    if (drawableComponent.Visible)
                    {
                        drawableComponent.Draw(gameTime);
                    }
                }
            }
        }


        public virtual void AddEntity(Entity entity)
        {
            collisionManager.AddEntity(entity);
            components.Add(entity);
        }
        public virtual void RemoveEntity(Entity entity)
        {
            collisionManager.RemoveEntity(entity);
            components.Remove(entity);
        }
    }
}
