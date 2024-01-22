using Microsoft.AspNetCore.RateLimiting;
using RateLimiting.FixedWindowLimiter.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter(RateLimitType.FixedWindow, options =>
    {
        // Allow 10 requests every 1 seconds
        // Add the next 10 requests to a queue
        // If the queue is full, oldest requests are processed first
        // Any other requests are rejected with status code 429
        options.Window = TimeSpan.FromSeconds(1);
        options.PermitLimit = 10;
        options.QueueLimit = 10;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    }).RejectionStatusCode = 429;
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
