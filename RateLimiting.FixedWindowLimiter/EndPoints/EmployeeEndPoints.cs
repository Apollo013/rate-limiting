namespace RateLimiting.FixedWindowLimiter.EndPoints
{
    public class EmployeeEndPoints
    {
        private static List<String> employees = new() { "John", "Mary", "Henry", "Lorraine" };

        public static void Map(WebApplication app)
        {
            app.MapGet("/employees", () =>
            {
                return Results.Ok(employees);
            })
            .WithName("GetEmployees")
            .WithOpenApi()
            .RequireRateLimiting(RateLimitType.FixedWindow);
        }
    }
}
