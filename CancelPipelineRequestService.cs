using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using ClientBizFlow_attemp_1.Database;
using Microsoft.EntityFrameworkCore;

namespace ClientBizFlow_attemp_1
{
    public class CancelPipelineRequestService: ICancelPipelineRequestService
    {
        private readonly AppDbContext _context;

        public CancelPipelineRequestService(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<CancellationRequest?> GetActiveRequest(string pipelineName, CancellationToken cancellationToken = default)
        //{
        //    var entitiy = await _context.CancelPipelineRequests
        //        .Where(i => i.Executed == false)
        //        .Where(i => i.PipelineName == pipelineName)
        //        .Where(i => i.ExpirationTime > DateTime.Now)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    if (entitiy == null)
        //    {
        //        return null;
        //    }

        //    var reuslt = new CancellationRequest()
        //    {
        //        Id = entitiy.Id,
        //        PipelineName = entitiy.PipelineName,
        //        ExpirationTime = entitiy.ExpirationTime,
        //        Description = entitiy.Description,
        //        ClosingByExpirationTimeOnly = entitiy.ClosingByExpirationTimeOnly,
        //        Created = entitiy.Created,
        //        Executed = entitiy.Executed,
        //        ClosingTime = entitiy.ClosingTime,
        //        ClosedAfterExpirationDate = entitiy.ClosingByExpirationTimeOnly,
        //    };

        //    return reuslt;
        //}
    }
}
