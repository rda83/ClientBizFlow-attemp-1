using BizFlow.Core.Contracts;
using BizFlow.Core.Model;

namespace ClientBizFlow_attemp_1
{
    public class PipelineService : IPipelineService
    {
        private readonly List<Pipeline> _pipelineCollection;

        public PipelineService()
        {
            _pipelineCollection = new List<Pipeline>()
            {
                new Pipeline()
                {
                    Name = "BizFlowJob_10_sec",
                    CronExpression = "0/10 * * * * ?",
                    PipelineItems = new List<PipelineItem>(){ new PipelineItem() { TypeOperationId = "FirstOperation" } }
                },
                //new Pipeline()
                //{
                //    Name = "BizFlowJob_15_sec",
                //    CronExpression = "0/15 * * * * ?"
                //}
            };
        }

        public IReadOnlyCollection<Pipeline> GetPipelines()
        {
            return _pipelineCollection;
        }


        public Pipeline GetPipeline(string pipelineName)
        {
            var result = _pipelineCollection.Where(i => i.Name == pipelineName)
                .FirstOrDefault();

            return result;
        }
    }
}
