using BizFlow.Abstractions;
using BizFlow.Abstractions.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("not-process-cancellation-token-1")]
    public class NotProcessCancellationToken1Worker : IWorker
    {
        public async Task ExecuteAsync(WorkerContext ctx, CancellationToken ct)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"not-process-cancellation-token-1 [ - {i} - ] Current UTC time: {DateTime.UtcNow}.");
                await Task.Delay(1000);
            }
        }

        public T? GetOptions<T>(JsonElement? options) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
