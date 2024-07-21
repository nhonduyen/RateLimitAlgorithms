// See https://aka.ms/new-console-template for more information
using SlidingWindow;

Console.WriteLine("Slide windows");
// Create a rate limiter that allows 5 requests per 10 seconds
var rateLimiter = new SlidingWindowRateLimiter(5, TimeSpan.FromSeconds(10));

for (int i = 0; i < 10; i++)
{
    bool allowed = rateLimiter.AllowRequest();
    Console.WriteLine($"Request {i + 1}: {(allowed ? "Allowed" : "Blocked")}");
    Console.WriteLine($"Remaining requests: {rateLimiter.GetRemainingRequests()}");

    // Simulate some time passing
    System.Threading.Thread.Sleep(1000);
}
