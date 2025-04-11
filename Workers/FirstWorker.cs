using BizFlow.Core;

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
