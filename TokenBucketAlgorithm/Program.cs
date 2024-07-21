// See https://aka.ms/new-console-template for more information
using TokenBucketAlgorithm;

Console.WriteLine("Token bucket");

var rateLimiter = new RateLimiter(10, 5); // 10 tokens, refills at 5 tokens/second

string userId = "user123";

for (int i = 0; i < 20; i++)
{
    bool allowed = rateLimiter.ShouldAllowRequest(userId);
    Console.WriteLine($"Request {i + 1}: {(allowed ? "Allowed" : "Denied")}");
    System.Threading.Thread.Sleep(200); // Wait 200ms between requests
}