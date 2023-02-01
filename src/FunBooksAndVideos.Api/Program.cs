using FunBooksAndVideos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddInfrastructure(GetInfrastructureConfig());
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

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