using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("SecondOperation")]
    public class SecondWorker : IBizFlowWorker
    {
        public async Task Run(WorkerContext ctx)
        {
            Console.WriteLine("SecondOperation");
        }

        public Task<CheckOptionsResult> CheckOptions(JsonElement Options)
        {
            return Task.FromResult(new CheckOptionsResult() { Success = true, Message = string.Empty });
        }
    }
}
