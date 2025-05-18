using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("SecondWorker")]
    public class SecondWorker : IBizFlowWorker
    {
        public Task Run(WorkerContext ctx)
        {
            throw new NotImplementedException();
        }

        public Task<CheckOptionsResult> CheckOptions(JsonElement Options)
        {
            return Task.FromResult(new CheckOptionsResult() { Success = false, Message = "Ошибка параметров SecondWorker" });
        }
    }
}
