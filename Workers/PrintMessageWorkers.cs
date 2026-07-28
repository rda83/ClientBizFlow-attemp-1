using BizFlow.Abstractions;
using BizFlow.Abstractions.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("print-message")]
    public class PrintMessageWorkers : IWorker
    {
        public async Task ExecuteAsync(WorkerContext ctx, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Current UTC time: {DateTime.UtcNow}.");
        }

        public T? GetOptions<T>(JsonElement? options) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
