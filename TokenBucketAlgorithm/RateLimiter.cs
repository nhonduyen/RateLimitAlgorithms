using System.Collections.Concurrent;

namespace TokenBucketAlgorithm
{
    public class RateLimiter
    {
        private readonly ConcurrentDictionary<string, TokenBucket> userBuckets = new ConcurrentDictionary<string, TokenBucket>();
        private readonly long capacity;
        private readonly long refillRate;

        public RateLimiter(long capacity, long refillRate)
        {
            this.capacity = capacity;
            this.refillRate = refillRate;
        }

        public bool ShouldAllowRequest(string userId)
        {
            var bucket = userBuckets.GetOrAdd(userId, _ => new TokenBucket(capacity, refillRate));
            return bucket.TryConsume(1);
        }
    }
}
