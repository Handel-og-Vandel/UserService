using NLog;
using Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();

logger.Info("Starting HaaV User service");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton<IUserRepository, MongoUserRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
} catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
} finally
{
    NLog.LogManager.Shutdown();
}