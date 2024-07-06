using OnlineChat.Application;
using OnlineChat.Infrastructure;
using OnlineChat.Persistence;
using OnlineChat.Application;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString().Replace("+", "_"));
                options.CustomOperationIds(apiDescription =>
                    apiDescription.TryGetMethodInfo(out var methodInfo)
                        ? methodInfo.Name
                        : null);
            });

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddOnlineChatPersistence(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x => x.DisplayOperationId());
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
