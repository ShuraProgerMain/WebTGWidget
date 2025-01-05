using System.Text.RegularExpressions;
using Telegram.Bot;

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

app.MapGet("/getgif/animated.gif", async (HttpContext context) =>
{
	string gifPath = Path.Combine(Directory.GetCurrentDirectory(), "animated.gif");
    byte[] gifBytes = await File.ReadAllBytesAsync(gifPath);

    context.Response.ContentType = "image/gif";
    await context.Response.Body.WriteAsync(gifBytes);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal sealed partial class TelegramChecker
{
	private readonly string _channelId = "shuraprogerTGC";
	
	public string ChannelId => _channelId;

	public async Task<string> GetMembersCount()
	{ 
		var bot = new TelegramBotClient("7269572623:AAEP_LR91SmY69tDf1T4jcmMG2Hs28Qz2hw");
		int memberCount = await bot.GetChatMemberCount(_channelId);

		return UpdateString(memberCount.ToString());
		// string channelUrl = $"https://t.me/{_channelId}";
		//
		// using HttpClient client = new HttpClient();
		// try
		// {
		// 	string html = await client.GetStringAsync(channelUrl);
		//
		// 	Regex regex = MyRegex();
		// 	Match match = regex.Match(html);
		//
		// 	if (match.Success)
		// 	{
		// 		string subscribers = match.Groups[1].Value;
		// 		Console.WriteLine($"Member's count: {subscribers}");
		// 		
		// 		return UpdateString(subscribers);
		// 	}
		//
		// 	Console.WriteLine("Can't get members count");
		// }
		// catch (Exception ex)
		// {
		// 	Console.WriteLine($"Exception: {ex.Message}");
		// }
		//
		// return "0";
	}

    [GeneratedRegex(@"<div class=""tgme_page_extra"">\s*([\d,]+)\s+subscribers\s*</div>")]
    private static partial Regex MyRegex();
	
	private static string UpdateString(string membersCount)
	{
		int index = 4;
		char[] chars = ['0', '0', '0', '0', '0'];
		char[] charsArray = membersCount.ToCharArray();
		
		for (int i = charsArray.Length - 1; i >= 0; i--)
		{
			chars[index] = charsArray[i];
			index--;
		}
		
		return new string(chars);
	}
}
