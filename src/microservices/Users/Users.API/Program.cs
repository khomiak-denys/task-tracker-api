using ServiceDefaults.Authentification;
using ServiceDefaults.CORS;
using ServiceDefaults.ErrorHandling;
using ServiceDefaults.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddErrorHandling();

builder.Logging.ClearProviders();
builder.Host.AddSerilogLogging();

var corsOptions = builder.Configuration
    .GetRequiredSection(CorsOptions.SectionName)
    .Get<CorsOptions>()!;

builder.AddCors(corsOptions);

builder.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
