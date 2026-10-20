using BizFlow.Abstractions;
using BizFlow.Abstractions.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    public class Delay5MinWorker : IWorker
    {
        public async Task ExecuteAsync(WorkerContext ctx, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            Console.WriteLine($"DELAY 5 MIN WORKER START: сurrent UTC time: {DateTime.UtcNow}.");

            await Task.Delay(300_000, ct);

            Console.WriteLine($"DELAY 5 MIN WORKER END: сurrent UTC time: {DateTime.UtcNow}.");
        }

        public T? GetOptions<T>(JsonElement? options) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
