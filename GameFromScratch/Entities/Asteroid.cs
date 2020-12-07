﻿using GameFromScratch.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFromScratch
{
    class Asteroid : Entity
    {
        public Asteroid(GameScene scene, SpriteBatch spriteBatch) : base(scene, spriteBatch)
        {
            Size = new Vector2(50, 50);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Move(gameTime);

            if (Position.X + Size.X < 0)
            {
                scene.RemoveEntity(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawFillRectangle(GraphicsDevice, Bounds, Color.Blue);

            spriteBatch.End();
        }

        public override void OnCollision(Entity other)
        {
            if (other is Player)
            {
                var player = other as Player;
                player.Health--;

                scene.RemoveEntity(this);
            }
        }
    }
}
