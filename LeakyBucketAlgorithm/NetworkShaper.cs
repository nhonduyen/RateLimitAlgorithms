using System.Collections.Concurrent;

namespace LeakyBucketAlgorithm
{
    public class NetworkShaper
    {
        private readonly ConcurrentDictionary<string, LeakyBucket> deviceBuckets = new ConcurrentDictionary<string, LeakyBucket>();
        private readonly long capacity;
        private readonly long leakRate;

        public NetworkShaper(long capacity, long leakRate)
        {
            this.capacity = capacity;
            this.leakRate = leakRate;
        }

        public bool ShouldAllowPacket(string deviceId, long packetSize)
        {
            var bucket = deviceBuckets.GetOrAdd(deviceId, _ => new LeakyBucket(capacity, leakRate));
            return bucket.TryAdd(packetSize);
        }
    }
}
