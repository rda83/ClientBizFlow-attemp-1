using BizFlow.Abstractions;
using BizFlow.Abstractions.Model;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Workers
{
    //[TypeOperationId("delay-5min")] -- Сделай воркер для шести минут, запускай каждые пять минут, что бы поймать ситуацию когда пауза между циклом будет 1 секунда
    //    далее солотвественно можешь добавить второй воркер который будет отрабатывать быстрее и вовремя
    //    поставь - фильтр только ожибающие - и посмотри как просахатитьмся расписание, потом сделай пробуждение потока


    // pipeline_1 начинает на 10-ой секунде, каждые 10 секунд. Отрабатывает за  2 секунды
    // pipeline_2 начинает на 10-ой секунде, каждые 10 секунд. Отрабатывает за 11 секунд

    // pipeline_1            v   **       v  **       v  **       v  **       v  **       v
    //              0123456789   0123456789  0123456789  0123456789  0123456789  0123456789
    // pipeline_2            ^   **********  *
    //                                    ^



    // pipeline_1     V         
    //              0 1 2 3 4 5 6 7 8 9 
    //                    ^

    [TypeOperationId("delay-3min")]
    public class Delay3MinWorker : IWorker
    {
        public async Task ExecuteAsync(WorkerContext ctx, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            Console.WriteLine($"DELAY 3 MIN WORKER START: сurrent UTC time: {DateTime.UtcNow}.");

            await Task.Delay(120_000, ct);

            Console.WriteLine($"DELAY 3 MIN WORKER END: сurrent UTC time: {DateTime.UtcNow}.");
        }

        public T? GetOptions<T>(JsonElement? options) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
