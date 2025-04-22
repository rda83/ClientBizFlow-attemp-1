using BizFlow.Core.Contracts;
using BizFlow.Core.Model;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("FirstOperation")]
    public class FirstWorker : IBizFlowWorker
    {
        public Task Run(WorkerContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
