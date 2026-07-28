using BizFlow.Core.Contracts;
using BizFlow.Core.Services.DI;
using BizFlow.Extensions.DependencyInjection;
using BizFlow.Schedules.Cron;
using BizFlow.Schedules.Interval;
using BizFlow.Storage.PostgreSQL;
using ClientBizFlow_attemp_1.Database;
using ClientBizFlow_attemp_1.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ClientBizFlow_attemp_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            //builder.Services.AddPostgreSQLBizFlowStorage(connectionString!);
            //builder.Services.AddBizFlow(typeof(Program).Assembly);


            //builder.Services.AddWorker("cleanup_interval_short",
            //    _ => new PrintMessageWorkers(),
            //    _ => new IntervalSchedule(TimeSpan.FromSeconds(5)));

            //builder.Services.AddWorker("cleanup_interval_long",
            //    _ => new PrintMessageWorkers(),
            //    _ => new IntervalSchedule(TimeSpan.FromSeconds(50)));


            //Allowed values    Allowed special characters Comment

            //┌───────────── second(optional)       0 - 59 * , - / H
            //│ ┌───────────── minute                0 - 59 * , - / H
            //│ │ ┌───────────── hour                0 - 23 * , - / H
            //│ │ │ ┌───────────── day of month      1 - 31 * , - / H L W ?                
            //│ │ │ │ ┌───────────── month           1 - 12 or JAN-DEC * , - / H
            //│ │ │ │ │ ┌───────────── day of week   0 - 6  or SUN-SAT * , - / H # L ?              Both 0 and 7 means SUN
            //│ │ │ │ │ │
            //* * * * * *





            //builder.Services.AddWorker("cleanup_cron",
            //    _ => new PrintMessageWorkers(),
            //    _ => new CronSchedule("*/5 * * * *", TimeZoneInfo.Utc));


            //builder.Services.AddWorker("cleanup_cron_13",
            //    _ => new PrintMessageWorkers(),
            //    _ => new CronSchedule("* 13 * * * ", TimeZoneInfo.Utc));

            //builder.Services.AddWorker("cleanup_cron_14",
            //    _ => new PrintMessageWorkers(),
            //    _ => new CronSchedule("* 14 * * * ", TimeZoneInfo.Utc));

            //builder.Services.AddWorker("cleanup_cron_15",
            //    _ => new PrintMessageWorkers(),
            //    _ => new CronSchedule("* 15 * * * ", TimeZoneInfo.Utc));


            //builder.Services.AddWorker("cleanup_cron_13",
            //    _ => new PrintMessageWorkers(),
            //    _ => new CronSchedule("50 2 * * * ", TimeZoneInfo.Utc));


            

            builder.Services.AddBizFlowWorkers(typeof(Program).Assembly); // Регистрация воркеров


            

            // Регистрация пайплайнов в цикле через builder.Services.AddPipeline


            builder.Services.AddBizFlowScheduler();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.Migrate();
                    Console.WriteLine("Database migrations applied successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                    throw;
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
