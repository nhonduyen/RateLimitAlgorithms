namespace LeakyBucketAlgorithm
{
    public class LeakyBucket
    {
        private readonly long capacity;
        private readonly long leakRate;
        private long currentSize;
        private long lastLeakTime;

        public LeakyBucket(long capacity, long leakRate)
        {
            this.capacity = capacity;
            this.leakRate = leakRate;
            this.currentSize = 0;
            this.lastLeakTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        private void Leak()
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long deltaTime = now - lastLeakTime;
            long leakedAmount = (deltaTime * leakRate) / 1000;
            currentSize = Math.Max(0, currentSize - leakedAmount);
            lastLeakTime = now;
        }

        public bool TryAdd(long numTokens)
        {
            Leak();
            if (currentSize + numTokens <= capacity)
            {
                currentSize += numTokens;
                return true;
            }
            return false;
        }
    }
}
