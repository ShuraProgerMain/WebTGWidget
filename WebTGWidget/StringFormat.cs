namespace WebTGWidget
{
	public static class StringFormat
	{
		public static string UpdateString(string membersCount)
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
}