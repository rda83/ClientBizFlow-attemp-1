using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using Bogus;
using ClientBizFlow_attemp_1.Database;
using ClientBizFlow_attemp_1.Database.Entities.Common;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("load-sales")]
    public class LoadSalesWorker : IBizFlowWorker
    {
        private readonly AppDbContext _appDb;

        public LoadSalesWorker(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task Run(WorkerContext ctx)
        {
            var opt = GetOptions<LoadProductWorkerOpt>(ctx.Options);

            var existingProducts = _appDb.Products.ToList();

            var saleFaker = new Faker<Sale>()
                .RuleFor(s => s.ProductId, f => f.PickRandom(existingProducts).Id)
                .RuleFor(s => s.CustomerName, f => f.Name.FullName())
                .RuleFor(s => s.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(s => s.UnitPrice, (f, sale) =>
                    {
                        var product = existingProducts.First(p => p.Id == sale.ProductId);
                        return product.Price;
                    })
                .RuleFor(s => s.SaleDate, f => f.Date.Between(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow))
                .RuleFor(s => s.Note, f => f.Lorem.Sentence().OrNull(f, 0.3f));

            var sales = saleFaker.Generate(opt!.Quantity);

            int skip = 0;
            while (true)
            {
                if (ctx.CancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Сообщение от воркера: LoadSalesWorker - операция отменена.");
                    break;
                }

                await Task.Delay(opt!.DelayMs);

                var entities = sales.Skip(skip).Take(opt!.BatchSize).ToList();

                if (!entities.Any())
                {
                    break;
                }

                await _appDb.Sales.AddRangeAsync(entities);
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

    public class LoadSalesWorkerOpt
    {
        public int Quantity { get; set; }
        public int BatchSize { get; set; }
        public int DelayMs { get; set; }
    }
}
