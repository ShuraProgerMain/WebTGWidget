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

		return StringFormat.UpdateString(memberCount.ToString());
	}

}