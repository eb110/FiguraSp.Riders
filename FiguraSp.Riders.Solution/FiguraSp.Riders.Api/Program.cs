using FiguraSp.Riders.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSharedDbConnection(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddSharedJwtScheme(builder.Configuration);


//##############################################################MIDDLEWARE###############

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
