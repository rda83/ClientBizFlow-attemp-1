using BizFlow.Core.Contracts;
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
