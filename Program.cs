using BizFlow.Core.Contracts;
using BizFlow.Core.Services.DI;
using ClientBizFlow_attemp_1.Database;
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
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPipelineService, PipelineService>();
            builder.Services.AddScoped<IBizFlowJournal, BizFlowJournal>();
            builder.Services.AddScoped<ICancelPipelineRequestService, CancelPipelineRequestService>();
            builder.Services.AddBizFlow(typeof(Program).Assembly);

            var app = builder.Build();

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
