using ScienceArchive.Application;
using ScienceArchive.Infrastructure;
using ScienceArchive.Web.Api.Auth;
using ScienceArchive.Web.Api.Middleware;

using ConfigurationManager = ScienceArchive.Web.Api.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
var persistenceOptions = ConfigurationManager.GetPersistenceOptions(builder);
var connectivityOptions = ConfigurationManager.GetConnectivityOptions(builder);

// Register built-in services
builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

// Register application-specific services
builder.Services
    .RegisterInfrastructureServices(persistenceOptions, connectivityOptions)
    .RegisterApplicationLayer();

// Register presentation layer services
builder.Services.RegisterAuth(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseAuthentication()
    .UseAuthorization();
app.MapControllers();

// Register middlewares
app
    .UseMiddleware<RequestResponseLoggingMiddleware>()
    .UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();