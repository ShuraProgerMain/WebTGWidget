using System.Text.RegularExpressions;
using WebTGWidget;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Открываем доступ к приложению на всех интерфейсах
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);  // Устанавливаем порт 5000 (или любой другой)
});

DiscordChecker discordChecker = new DiscordChecker();
await discordChecker.Init();

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

app.MapGet("/telegramMembers", async () =>
	{
		TelegramChecker telegramChecker = new TelegramChecker();
		var jsonResponse = new
		{
			Message = "Hello, World!",
			Status = "Success",
			DispData = await telegramChecker.GetMembersCount()
		};
		
		return jsonResponse;
	})
	.WithName("GetWeatherForecast");

app.MapGet("/discordOnlineMembers", async () =>
	{
		if (!discordChecker.IsReady) return null;
		
		int onlineMembersCount = await discordChecker.GetOnlineMembersCountAsync();
		
		var jsonResponse = new
		{
			Message = "Hello, World!",
			Status = "Success",
			DispData = onlineMembersCount
		};
		
		return jsonResponse;
	})
	.WithName("DiscordOnlineMembers");

app.MapGet("/discordAllMembers", async () =>
	{
		if (!discordChecker.IsReady) return null;
		
		int totalMembersCount = await discordChecker.GetTotalMembersCountAsync();
		var jsonResponse = new
		{
			Message = "Hello, World!",
			Status = "Success",
			DispData = totalMembersCount
		};
		
		return jsonResponse;
	})
	.WithName("DiscordAllMembers");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}