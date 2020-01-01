namespace Chess.Models
{
	public class Location
	{
		public int Column;
		public int Row;
		public string AlgebraicNotation;

		public override bool Equals(object obj)
		{
			var location = obj as Location;

			if (location == null)
				return false;

			if(Column != location.Column || Row != location.Row || AlgebraicNotation != location.AlgebraicNotation)
				return false;

			return true;
		}
	}
}
