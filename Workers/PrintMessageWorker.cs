using BizFlow.Abstractions;
using BizFlow.Abstractions.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("print-message")]
    public class PrintMessageWorker : IWorker
    {
        public async Task ExecuteAsync(WorkerContext ctx, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"print-message [ - {i} - ] Current UTC time: {DateTime.UtcNow}.");
                await Task.Delay(1000, ct);
            }                   
        }

        public T? GetOptions<T>(JsonElement? options) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
