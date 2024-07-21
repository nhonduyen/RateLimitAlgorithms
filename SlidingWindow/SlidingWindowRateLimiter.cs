using System.Collections.Concurrent;

namespace SlidingWindow
{
    public class SlidingWindowRateLimiter
    {
        private readonly int _maxRequests;
        private readonly TimeSpan _windowSize;
        private readonly ConcurrentQueue<DateTime> _requestTimestamps;

        public SlidingWindowRateLimiter(int maxRequests, TimeSpan windowSize)
        {
            _maxRequests = maxRequests;
            _windowSize = windowSize;
            _requestTimestamps = new ConcurrentQueue<DateTime>();
        }

        public bool AllowRequest()
        {
            var now = DateTime.UtcNow;

            // Remove timestamps outside the current window
            while (_requestTimestamps.TryPeek(out DateTime oldestTimestamp)
                   && now - oldestTimestamp > _windowSize)
            {
                _requestTimestamps.TryDequeue(out _);
            }

            // Check if we're at the limit
            if (_requestTimestamps.Count >= _maxRequests)
            {
                return false;
            }

            // Allow the request and add the timestamp
            _requestTimestamps.Enqueue(now);
            return true;
        }

        public int GetRemainingRequests()
        {
            var now = DateTime.UtcNow;

            // Remove timestamps outside the current window
            while (_requestTimestamps.TryPeek(out DateTime oldestTimestamp)
                   && now - oldestTimestamp > _windowSize)
            {
                _requestTimestamps.TryDequeue(out _);
            }

            return Math.Max(0, _maxRequests - _requestTimestamps.Count);
        }
    }
}
