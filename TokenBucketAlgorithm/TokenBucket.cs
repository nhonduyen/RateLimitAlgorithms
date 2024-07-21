namespace TokenBucketAlgorithm
{
    public class TokenBucket
    {
        private readonly long capacity;
        private readonly long refillRate;
        private long tokens;
        private long lastRefillTime;

        public TokenBucket(long capacity, long refillRate)
        {
            this.capacity = capacity;
            this.refillRate = refillRate;
            this.tokens = capacity;
            this.lastRefillTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        private void Refill()
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long deltaTime = now - lastRefillTime;
            long newTokens = (deltaTime * refillRate) / 1000;
            tokens = Math.Min(capacity, tokens + newTokens);
            lastRefillTime = now;
        }

        public bool TryConsume(long numTokens)
        {
            Refill();
            if (tokens >= numTokens)
            {
                tokens -= numTokens;
                return true;
            }
            return false;
        }
    }
}
