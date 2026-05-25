using BizFlow.Core.Contracts;
using BizFlow.Core.Services.DI;
using BizFlow.Extensions.DependencyInjection;
using BizFlow.Schedules.Interval;
using BizFlow.Storage.PostgreSQL;
using ClientBizFlow_attemp_1.Database;
using ClientBizFlow_attemp_1.Workers;
using Microsoft.EntityFrameworkCore;

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


            builder.Services.AddWorker("cleanup",
                _ => new PrintMessageWorkers(),
                _ => new IntervalSchedule(TimeSpan.FromSeconds(5)));


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
