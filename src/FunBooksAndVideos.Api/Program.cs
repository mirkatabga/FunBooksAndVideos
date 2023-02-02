using FunBooksAndVideos.API;
using FunBooksAndVideos.API.Extensions;
using FunBooksAndVideos.Application;
using FunBooksAndVideos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddInfrastructure(GetInfrastructureConfig());
    builder.Services.AddApplication();
}

var app = builder.Build();
{
    app.MigrateDatabase();
    app.SeedDatabase();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler(opt => opt.UseCustomErrors(builder.Environment));
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

InfrastructureConfig GetInfrastructureConfig()
{
    return builder.Configuration
            .GetSection(nameof(InfrastructureConfig))
            .Get<InfrastructureConfig>();
}