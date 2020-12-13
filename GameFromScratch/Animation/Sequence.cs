using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlanetDefender.Animation
{
    public class Sequence
    {
        private Texture2D texture;
        private Point size;
        private int startIndex;
        private int count;
        private int rowCount;
        private int colCount;
        private Rectangle[] frames;
        private float totalDuration;

        public Sequence(Texture2D texture, Point size, float totalDuration, int startIndex, int count)
        {
            this.texture = texture;
            this.size = size;
            this.startIndex = startIndex;
            this.totalDuration = totalDuration;
            this.count = Math.Max(count, rowCount * colCount);

            colCount = texture.Width / size.X;
            rowCount = texture.Height / size.Y;

            frames = new Rectangle[count];
            for (int i = 0; i < Count; i++)
            {
                var index = i + StartIndex;
                frames[i] = new Rectangle(new Point((index % ColCount) * Size.X, (index / ColCount) * Size.Y), size);
            }
        }

        public Sequence(Texture2D texture, Point size, float totalDuration, int startIndex = 0)
            : this(texture, size, totalDuration, startIndex, texture.Width / size.X * texture.Height / size.Y)
        {
        }

        public Rectangle this[int index]
        {
            get => frames[index];
        }
        public int Count => count;
        public Texture2D Texture => texture;
        public Point Size => size;
        public int StartIndex => startIndex;
        public int RowCount => rowCount;
        public int ColCount => colCount;

        public float TotalDuration => totalDuration;
        public float FrameDuration
        {
            get => totalDuration / Count;
        }
    }
}