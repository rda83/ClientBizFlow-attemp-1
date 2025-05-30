using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("FirstOperation")]
    public class FirstWorker : IBizFlowWorker
    {
        public Task Run(WorkerContext ctx)
        {
            Console.WriteLine("FirstOperation");
            return Task.CompletedTask;
        }

        public Task<CheckOptionsResult> CheckOptions(JsonElement Options)
        {
            return Task.FromResult(new CheckOptionsResult() { Success = true, Message = string.Empty });
        }
    }
}
