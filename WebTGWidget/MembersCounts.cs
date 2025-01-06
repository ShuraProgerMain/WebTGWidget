namespace WebTGWidget
{
	internal readonly struct MembersCounts(int totalMemberCount, int onlineMemberCount)
	{
		public readonly int TotalMemberCount = totalMemberCount;
		public readonly int OnlineMemberCount = onlineMemberCount;
	}
}