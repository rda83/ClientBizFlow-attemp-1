using BizFlow.Core;

namespace ClientBizFlow_attemp_1
{
    public class PipelineService : IPipelineService
    {
        public IReadOnlyCollection<Pipeline> GetPipelines()
        {
            IReadOnlyCollection<Pipeline> result = new List<Pipeline>()
            {
                new Pipeline()
                {
                    Name = "BizFlowJob_10_sec",
                    CronExpression = "0/10 * * * * ?"
                },
                new Pipeline()
                {
                    Name = "BizFlowJob_15_sec",
                    CronExpression = "0/15 * * * * ?"
                }
            };
            return result;
        }
    }
}
