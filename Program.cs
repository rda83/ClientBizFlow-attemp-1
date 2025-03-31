
using BizFlow.Core.Internal;
using BizFlow.Core.Services.DI;

namespace ClientBizFlow_attemp_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddBizFlow();
                       
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            //Использовать StartupFilter
            var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
            lifetime.ApplicationStarted.Register(() =>
            {
                var _serviceProvider = builder.Services.BuildServiceProvider();
                var jobsManager = _serviceProvider.GetRequiredService<IJobsManager>();

                jobsManager.CrerateJob("BizFlowJob",
                    "0/10 * * * * ?");
            });


            app.Run();


        }
    }
}
