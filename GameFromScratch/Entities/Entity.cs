﻿using GameFromScratch.Animation;
using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Entities
{
    public abstract class Entity : DrawableGameComponent
    {
        public const float DefaultGravity = 10;

        protected SpriteBatch spriteBatch;
        protected GameScene scene;
        protected Animator animator;

        public Texture2D Texture { get; set; } = null;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = Vector2.Zero;
        public bool Killed { get; protected set; }
        public bool Destroyed { get; protected set; }
        public bool IgnoreCollision { get; protected set; }
        public Rectangle Bounds
        {
            get => new Rectangle(Position.ToPoint(), Size.ToPoint());
        }

        public Point ScreenSize {
            get => new Point(Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
        }


        public Entity(GameScene scene, SpriteBatch spriteBatch) : base(scene.Game)
        {
            this.spriteBatch = spriteBatch;
            this.scene = scene;
            animator = new Animator(spriteBatch);
        }

        public virtual void OnCollision(Entity other)
        {

        }

        public virtual void Move(GameTime gameTime)
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Destroy()
        {
            Destroyed = true;
            scene.RemoveEntity(this);
        }
        
        public virtual void Kill()
        {
            Killed = true;
            Destroy();
        }
    }
}
