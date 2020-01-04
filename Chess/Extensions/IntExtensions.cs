namespace Chess.Extensions
{
	public static class IntExtensions
	{
		public static int MAXIMUM { get; } = 7;
		public static int MINUMUM { get; } = 0;

		public static int BoundryClamp(this int value)
		{
			if (value > MAXIMUM)
				value = MAXIMUM;

			if (value < MINUMUM)
				value = MINUMUM;

			return value;
		}
	}
}
