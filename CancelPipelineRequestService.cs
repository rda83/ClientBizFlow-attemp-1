using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using ClientBizFlow_attemp_1.Database;

namespace ClientBizFlow_attemp_1
{
    public class CancelPipelineRequestService: ICancelPipelineRequestService
    {
        private readonly AppDbContext _context;

        public CancelPipelineRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddAsync(CancelPipelineRequest request, CancellationToken cancellationToken = default)
        {
            var requestEntity = new Database.Entities.BizFlow.CancelPipelineRequest()
            {
                PipelineName = request.PipelineName,
                ExpirationTime = request.ExpirationTime,
                Description = request.Description,
                ClosingByExpirationTimeOnly = request.ClosingByExpirationTimeOnly,
                Created = request.Created,
            };

            await _context.CancelPipelineRequests.AddAsync(requestEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return requestEntity.Id;
        }
    }
}
