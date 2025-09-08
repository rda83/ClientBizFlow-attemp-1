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

        public async Task SetExecutedAsync(long id, string msg = "", CancellationToken cancellationToken = default)
        {
            var request = _context.CancelPipelineRequests.Where(i => i.Id == id && i.Executed == false)
                .FirstOrDefault();

            if (request != null)
            {
                request.Executed = true;
                request.ClosingTime = DateTime.Now;
                request.ClosedAfterExpirationDate = request.ExpirationTime <= request.ClosingTime;

                if (!string.IsNullOrEmpty(msg))
                    request.Description = $"[{msg}]{request.Description}";

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new InvalidOperationException($"Запрос на отмену пайплайна: '{id}' не найден"); // TODO:i18n
            }
        }
    }
}
