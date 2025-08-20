using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using ClientBizFlow_attemp_1.Database;
using Microsoft.EntityFrameworkCore;

namespace ClientBizFlow_attemp_1
{
    public class BizFlowJournal : IBizFlowJournal
    {
        private readonly AppDbContext _context;
        public BizFlowJournal(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRecordAsync(BizFlowJournalRecord record, CancellationToken cancellationToken = default)
        {
            var entity = new Database.Entities.BizFlow.BizFlowJournalRecord();
            entity.Period = record.Period;
            entity.PipelineName = record.PipelineName;
            entity.ItemDescription = record.ItemDescription;
            entity.ItemSortOrder = record.ItemSortOrder;
            entity.TypeAction = record.TypeAction;
            entity.TypeOperationId = record.TypeOperationId;
            entity.LaunchId = record.LaunchId;
            entity.Message = record.Message;
            entity.Trigger = record.Trigger;
            entity.IsStartNowPipeline = record.IsStartNowPipeline;
            entity.ItemId = record.ItemId;

            await _context.BizFlowJournalRecords.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<BizFlowJournalRecord>> GetPagedAsync(
            int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Номер страницы должен быть больше 0", nameof(pageNumber));

            if (pageSize < 1)
                throw new ArgumentException("Размер страницы должен быть больше 0", nameof(pageSize));

            var query = _context.BizFlowJournalRecords
                .OrderBy(x => x.Id);

            var records = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = records.Select(r => new BizFlowJournalRecord
            {
                Period = r.Period,
                PipelineName = r.PipelineName,
                ItemDescription = r.ItemDescription,
                ItemSortOrder = r.ItemSortOrder,
                TypeAction = r.TypeAction,
                TypeOperationId = r.TypeOperationId,
                LaunchId = r.LaunchId,
                Message = r.Message,
                Trigger = r.Trigger,
                IsStartNowPipeline = r.IsStartNowPipeline,
                ItemId = r.ItemId,
            }).ToList();

            return dtos;
        }

        public async Task<IEnumerable<BizFlowJournalRecord>> GetJournalRecordByLaunchId(string launchId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(launchId))
                throw new ArgumentException("Необходимо передать идентификатор запуска пайплайна", nameof(launchId));

            var resut = await _context.BizFlowJournalRecords
                .Where(i => i.LaunchId == launchId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var dtos = resut.Select(r => new BizFlowJournalRecord
            {
                Period = r.Period,
                PipelineName = r.PipelineName,
                ItemDescription = r.ItemDescription,
                ItemSortOrder = r.ItemSortOrder,
                TypeAction = r.TypeAction,
                TypeOperationId = r.TypeOperationId,
                LaunchId = r.LaunchId,
                Message = r.Message,
                Trigger = r.Trigger,
                IsStartNowPipeline = r.IsStartNowPipeline,
                ItemId = r.ItemId,
            }).ToList();

            return dtos;
        }

        public async Task<string?> GetLastLaunchId(string pipelineName,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pipelineName))
                throw new ArgumentException("Необходимо передать имя пайплайна", nameof(pipelineName));

            var lastAction = await _context.BizFlowJournalRecords
                .Where(i => i.PipelineName == pipelineName)
                .OrderByDescending(i => i.Period)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (lastAction == null) { return null; }

            return lastAction.LaunchId;
        }
    }
}
