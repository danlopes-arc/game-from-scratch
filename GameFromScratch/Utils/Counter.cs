namespace GameFromScratch.Utils
{
    public class Counter
    {
        public float Current { get; private set; }
        public float Total { get; set; }

        public float Remaining => Total - Current;

        public bool Done { get; private set; }

        public Counter(float total)
        {
            Total = total;
        }

        public bool Update(float deltaTime)
        {
            if (Done) return true;
            Current += deltaTime;
            if (Current < Total) return false;

            Done = true;
            return true;
        }

        public void Reset()
        {
            Done = false;
            Current = 0;
        }
    }
}