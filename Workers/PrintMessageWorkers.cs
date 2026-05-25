using BizFlow.Abstractions;

namespace ClientBizFlow_attemp_1.Workers
{
    public class PrintMessageWorkers : IWorker
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Current UTC time: {DateTime.UtcNow}.");
        }
    }
}
