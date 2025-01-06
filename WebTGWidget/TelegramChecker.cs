using Telegram.Bot;

namespace WebTGWidget;

internal sealed class TelegramChecker
{
	private readonly string _channelId = "@shuraprogerTGC";
	
	public string ChannelId => _channelId;

	public async Task<string> GetMembersCount()
	{ 
		var bot = new TelegramBotClient("7269572623:AAEP_LR91SmY69tDf1T4jcmMG2Hs28Qz2hw");
		int memberCount = await bot.GetChatMemberCount(_channelId);

		return UpdateString(memberCount.ToString());
	}

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