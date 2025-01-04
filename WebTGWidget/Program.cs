var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Открываем доступ к приложению на всех интерфейсах
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);  // Устанавливаем порт 5000 (или любой другой)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
	{
		var jsonResponse = new
		{
			Message = "Hello, World!",
			Status = "Success",
			Timestamp = DateTime.UtcNow
		};
		
		return jsonResponse;
	})
	.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}