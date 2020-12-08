namespace GameFromScratch.Utils
{
    public class Counter
    {
        private float count;
        public float Delay { get; set; }
        public bool Done { get; private set; }
        
        public Counter(float delay)
        {
            Delay = delay;
        }

        public bool Update(float deltaTime)
        {
            if (Done) return true;
            count += deltaTime;
            if (count < Delay) return false;

            Done = true;
            return true;
        }

        public void Reset()
        {
            Done = false;
            count = 0;
        }
    }
}