// See https://aka.ms/new-console-template for more information
using LeakyBucketAlgorithm;

Console.WriteLine("Leaky bucket");

var networkShaper = new NetworkShaper(100, 20); // 100 tokens capacity, leaks at 20 tokens/second

string deviceId = "device123";

long[] packetSizes = { 10, 20, 30, 40, 10, 50 };

foreach (var packetSize in packetSizes)
{
    bool allowed = networkShaper.ShouldAllowPacket(deviceId, packetSize);
    Console.WriteLine($"Packet of size {packetSize}: {(allowed ? "Allowed" : "Denied")}");
    System.Threading.Thread.Sleep(500); // Wait 500ms between packets
}
