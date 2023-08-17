using Microsoft.AspNetCore.RateLimiting;
using RateLimiting.FixedWindowLimiter.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("FixedPolicy", options =>
    {
        // Allow 3 requests every 10 seconds
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 3;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

WeatherForecastEndPoints.Map(app);
EmployeeEndPoints.Map(app);


app.Run();
