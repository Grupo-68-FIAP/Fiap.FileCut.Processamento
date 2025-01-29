
using Fiap.FileCut.Infra.Api;
using Fiap.FileCut.Infra.Api.Configurations;

namespace Fiap.FileCut.Processamento.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.ConfigureFileCutProcessamentoApi();

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            scope.ScopedFileCutProcessamentoApi();

            app.InitializeFileCutProcessamentoApi();

            app.MapControllers();

            app.Run();
        }
    }
}
