using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using ClientBizFlow_attemp_1.Database;

namespace ClientBizFlow_attemp_1
{
    public class PipelineService : IPipelineService
    {
        private readonly List<Pipeline> _pipelineCollection;
        private readonly AppDbContext _context;
        public PipelineService(AppDbContext context)
        {
            _context = context;


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

        public async Task AddPipelineAsync(Pipeline pipelineItem, CancellationToken cancellationToken = default)
        {
            var pipelineEntity = new Database.Entities.BizFlow.Pipeline();
            pipelineEntity.Name = pipelineItem.Name;
            pipelineEntity.CronExpression = pipelineItem.CronExpression;
            pipelineEntity.Description = pipelineItem.Description;
            pipelineEntity.Blocked = pipelineItem.Blocked;
            pipelineEntity.PipelineItems = pipelineItem.PipelineItems.Select(i =>
            {
                var item = new Database.Entities.BizFlow.PipelineItem();
                item.TypeOperationId = i.TypeOperationId;
                item.SortOrder = i.SortOrder;
                item.Description = i.Description;
                item.Blocked = i.Blocked;
                item.Options = i.Options;
                return item;
            }).ToList();

            await _context.Pipelines.AddAsync(pipelineEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
