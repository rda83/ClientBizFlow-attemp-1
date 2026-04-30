using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using Bogus;
using ClientBizFlow_attemp_1.Database;
using ClientBizFlow_attemp_1.Database.Entities.Common;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("load-product")]
    public class LoadProductWorker : IBizFlowWorker
    {
        private readonly AppDbContext _appDb;

        public LoadProductWorker(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task Run(WorkerContext ctx)
        {
            var opt = GetOptions<LoadProductWorkerOpt>(ctx.Options);

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence(10))
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000))
                .RuleFor(p => p.StockQuantity, f => f.Random.Int(0, 200))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
                .RuleFor(p => p.UpdatedAt, f => f.Date.Recent(30).OrNull(f, 0.2f));

            var products = productFaker.Generate(opt!.Quantity);

            int skip = 0;

            while (true)
            {
                if (ctx.CancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Сообщение от воркера: LoadProductWorker - операция отменена.");
                    break;
                }

                await Task.Delay(opt!.DelayMs);

                var entities = products.Skip(skip).Take(opt!.BatchSize).ToList();

                if (!entities.Any())
                {
                    break;
                }

                await _appDb.Products.AddRangeAsync(entities);
                await _appDb.SaveChangesAsync();
                skip += opt!.BatchSize;
            }
        }

        public Task<CheckOptionsResult> CheckOptions(JsonElement options)
        {
            var result = new CheckOptionsResult();

            try
            {
                var optionsObj = GetOptions<LoadProductWorkerOpt>(options);
                if (optionsObj == null)
                {
                    result.Success = false;
                    result.Message = "Options cannot be null";
                }
                else
                {
                    result.Success = true;
                }

                result.Success = true;
            }
            catch (JsonException ex)
            {
                result.Message = $"Invalid JSON format: {ex.Message}";
                result.Success = false;
            }
            catch (Exception ex)
            {
                result.Message = $"Validation failed: {ex.Message}";
                result.Success = false;
            }

            return Task.FromResult(result);
        }

        public T? GetOptions<T>(JsonElement? options) where T : class
        {
            return options?.Deserialize<T>() ?? null;
        }
    }

    public class LoadProductWorkerOpt
    {
        public int Quantity { get; set; }
        public int BatchSize { get; set; }
        public int DelayMs { get; set; }
    }
}
