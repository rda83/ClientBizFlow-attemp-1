using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using ClientBizFlow_attemp_1.Database;
using Microsoft.EntityFrameworkCore;

namespace ClientBizFlow_attemp_1
{
    public class PipelineService : IPipelineService
    {
        private readonly AppDbContext _context;
        public PipelineService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Pipeline>> GetPipelinesAsync(CancellationToken cancellationToken = default)
        {
            var result = new List<Pipeline>();

            try
            {
                return await _context.Pipelines
                    .AsNoTracking()
                    .Select(entity => new Pipeline
                    {
                        Name = entity.Name,
                        CronExpression = entity.CronExpression,
                        Description = entity.Description,
                        Blocked = entity.Blocked
                    })
                    .ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Pipeline?> GetPipelineAsync(string pipelineName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(pipelineName))
            {
                throw new ArgumentException("Имя пайплайна не может быть пустым", nameof(pipelineName)); // TODO:i18n
            }

            var query = _context.Pipelines.Where(i => i.Name == pipelineName)
                .Include(i => i.PipelineItems)
                .AsSplitQuery()
                .AsNoTracking();

            var entity = await query.FirstOrDefaultAsync(cancellationToken);

            if (entity == null) { return null; }

            var pipeline = new Pipeline(); // TODO: Builder
            pipeline.Name = entity.Name;
            pipeline.CronExpression = entity.CronExpression;
            pipeline.Description = entity.Description;
            pipeline.Blocked = entity.Blocked;
            pipeline.PipelineItems = entity.PipelineItems.Select(i =>
            {
                var item = new PipelineItem();
                item.TypeOperationId = i.TypeOperationId;
                item.SortOrder = i.SortOrder;
                item.Description = i.Description;
                item.Blocked = i.Blocked;
                item.Options = i.Options;
                return item;
            }).ToList();

            return pipeline;
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

        public Task<bool> PipelineNameExist(string pipelineName, CancellationToken cancellationToken = default)
        {
            return _context.Pipelines.AnyAsync(i => i.Name == pipelineName, cancellationToken);
        }

        public async Task DeletePipelineAsync(string pipelineName, CancellationToken cancellationToken = default)
        {
            var pipeLine = await _context.Pipelines
                .Include(i => i.PipelineItems)
                .FirstOrDefaultAsync(i => i.Name == pipelineName, cancellationToken)
                    ?? throw new InvalidOperationException($"Pipeline with name '{pipelineName}' not found."); // TODO:i18n;

            _context.Remove(pipeLine);
            await _context.SaveChangesAsync();
        }
    }
}
