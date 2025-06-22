using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("SecondOperation")]
    public class SecondWorker : IBizFlowWorker
    {
        public Task Run(WorkerContext ctx)
        {
            var opt = GetOptions<SecondWorkerOpt>(ctx.Options);

            Console.WriteLine($"{ctx.PipelineName} - {ctx.CronExpression} - {ctx.TypeOperationId}");

            Console.WriteLine($"{opt.Name}");
            Console.WriteLine($"{opt.Age}");
            Console.WriteLine($"{string.Join(",", opt.Ids)}");

            return Task.CompletedTask;
        }

        public Task<CheckOptionsResult> CheckOptions(JsonElement options)
        {
            var result = new CheckOptionsResult();

            try
            {
                var optionsObj = GetOptions<SecondWorkerOpt>(options);
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

        private class SecondWorkerOpt
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<int> Ids { get; set; } = new List<int>();
        }
    }
}
