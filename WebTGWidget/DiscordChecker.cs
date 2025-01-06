using Discord;
using Discord.WebSocket;

namespace WebTGWidget
{
	internal sealed class DiscordChecker
	{
		public bool IsReady { get; private set; }
		private DiscordSocketClient _client;

		public async Task Init()
		{
			_client = new DiscordSocketClient(new DiscordSocketConfig()
			{
				GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences
			});

			_client.Ready += OnReady;
			_client.Log += Log;

			string token = "";
			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();
			
			// await Task.Delay(-1);
		}

		private async Task OnReady()
		{
			ulong guildId = 1318147797609877514;
			SocketGuild? guild = _client.GetGuild(guildId);

			if (guild == null)
			{
				Console.WriteLine("No guild found");
				return;
			}

			await guild.DownloadUsersAsync();
			IReadOnlyCollection<SocketGuildUser>? members = guild.Users;
			
			int totalMembers = members.Count;
			int onlineMembers = members.Count(u => u.Status == UserStatus.Online);
			
			Console.WriteLine($"Total members: {totalMembers}");
			Console.WriteLine($"Online members: {onlineMembers}");
			
			IsReady = true;
		}

		public async Task<int> GetTotalMembersCountAsync()
		{
			IReadOnlyCollection<SocketGuildUser> members = await GetMembers();
			return members.Count;
		}
		
		public async Task<int> GetOnlineMembersCountAsync()
		{
			IReadOnlyCollection<SocketGuildUser> members = await GetMembers();
			return members.Count(u => u.Status == UserStatus.Online);
		}

		public async Task<MembersCounts> GetMembersCounts()
		{
			ulong guildId = 1318147797609877514;
			SocketGuild? guild = _client.GetGuild(guildId);

			if (guild == null)
			{
				Console.WriteLine("No guild found");
				return new MembersCounts();
			}

			await guild.DownloadUsersAsync();
			IReadOnlyCollection<SocketGuildUser>? members = guild.Users;
			
			int totalMembers = members.Count;
			int onlineMembers = members.Count(u => u.Status == UserStatus.Online);
			
			Console.WriteLine($"Total members: {totalMembers}");
			Console.WriteLine($"Online members: {onlineMembers}");

			return new MembersCounts(totalMembers, onlineMembers);
		}

		private async Task<IReadOnlyCollection<SocketGuildUser>> GetMembers()
		{
			ulong guildId = 1318147797609877514;
			SocketGuild? guild = _client.GetGuild(guildId);

			if (guild == null)
			{
				Console.WriteLine("No guild found");
				return [];
			}

			await guild.DownloadUsersAsync();
			
			return guild.Users;
		}
		

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}