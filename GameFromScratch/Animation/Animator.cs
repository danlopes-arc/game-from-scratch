using GameFromScratch.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFromScratch.Animation
{
    public class Animator
    {
        private SpriteBatch spriteBatch;
        private Counter counter;
        private Sequence sequence;
        public Sequence Sequence
        {
            get => sequence;
            set
            {
                sequence = value;
                counter = new Counter(sequence.FrameDuration);
            }
        }

        public bool Loop { get; set; }
        public int LoopLimit { get; set; }
        public int LoopCount { get; private set; }
        public bool Complete { get; private set; }
        public bool Playing { get; private set; }
        public int Index { get; private set; }

        public Animator(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Update(float deltaTime)
        {
            if (!Playing || Complete || Sequence == null) return;
            
            if (counter.Update(deltaTime))
            {
                if (++Index == Sequence.Count)
                {
                    if (!Loop || ++LoopCount == LoopLimit)
                    {
                        Complete = true;
                        Playing = false;
                    }
                    else
                    {
                        Index = 0;
                    }
                }
                counter.Reset();
            }
            
        }

        public void Draw(Point position, Point size)
        {
            if (!Playing || Complete || Sequence == null) return;
            var destRect = new Rectangle(position, size);
            
            spriteBatch.Draw(Sequence.Texture, destRect, Sequence[Index], Color.White);
        }

        public bool Play()
        {
            if (Playing)
            {
                return false;
            }

            Playing = true;
            return true;
        }
        
        public bool Pause()
        {
            if (!Playing || Complete)
            {
                return false;
            }

            Playing = false;
            return true;
        }

        public void Reset()
        {
            Playing = false;
            Complete = false;
            Index = 0;
            counter.Reset();
        }
    }
}