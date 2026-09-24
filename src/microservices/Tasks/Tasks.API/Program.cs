using ServiceDefaults.Authentification;
﻿using Serilog;
using ServiceDefaults.CORS;
using ServiceDefaults.ErrorHandling;
using ServiceDefaults.Logging;
using ServiceDefaults.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

Serilog.Debugging.SelfLog.Enable(Console.Error);

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

builder.AddRateLimiting();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("public-api");

app.Run();
