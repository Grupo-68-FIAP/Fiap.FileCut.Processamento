
using Fiap.FileCut.Infra.Api;
using Fiap.FileCut.Infra.Api.Configurations;

namespace Fiap.FileCut.Processamento.Api;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        await builder.ConfigureFileCutProcessamentoApi();

        var app = builder.Build();
        var scope = app.Services.CreateScope();
        await scope.ScopedFileCutProcessamentoApi();

        await app.InitializeFileCutProcessamentoApi();

        app.MapControllers();

        await app.RunAsync();
    }
}
