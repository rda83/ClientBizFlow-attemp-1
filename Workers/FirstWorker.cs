using BizFlow.Core.Contracts;
using BizFlow.Core.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    [TypeOperationId("FirstOperation")]
    public class FirstWorker : IBizFlowWorker
    {
        public Task Run(WorkerContext ctx)
        {
            var opt = GetOptions<FirstWorkerOpt>(ctx.Options);

            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"<FirstOperation {i}");
                Console.WriteLine($"{ctx.PipelineName} - {ctx.CronExpression} - {ctx.TypeOperationId}");
                Console.WriteLine($"{opt.Path} {opt.Count} {opt.ActiveOnly}");
                Console.WriteLine($">FirstOperation {i}");
                Thread.Sleep(10000);
            }

            return Task.CompletedTask;
        }

        public Task<CheckOptionsResult> CheckOptions(JsonElement options)
        {
            var result = new CheckOptionsResult();

            try
            {
                var optionsObj = GetOptions<FirstWorkerOpt>(options);
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

        private class FirstWorkerOpt
        {
            public string Path { get; set; }
            public int Count { get; set; }
            public bool ActiveOnly { get; set; }
        }
    }
}
