namespace Chess.Extensions
{
	public static class IntExtensions
	{
		public static int MAX { get; } = 7;
		public static int MIN { get; } = 0;

		public static int Clamp(this int value)
		{
			if (value > MAX)
				value = MAX;

			if (value < MIN)
				value = MIN;

			return value;
		}
	}
}
