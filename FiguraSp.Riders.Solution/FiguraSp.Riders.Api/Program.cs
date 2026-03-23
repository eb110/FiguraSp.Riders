using FiguraSp.Riders.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSharedServices(builder.Configuration)
    .AddCustomServices();

builder.Services.AddControllers();


//##############################################################MIDDLEWARE###############

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
