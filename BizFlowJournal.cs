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

        public async Task<IEnumerable<JournalRecord>> GetJournalRecordByLaunchId(string launchId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(launchId))
                throw new ArgumentException("Необходимо передать идентификатор запуска пайплайна", nameof(launchId));

            var resut = await _context.BizFlowJournalRecords
                .Where(i => i.LaunchId == launchId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var dtos = resut.Select(r => new JournalRecord
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
                IsStartNow = r.IsStartNowPipeline,
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
