using Microsoft.Xna.Framework;

namespace Chess.Models
{
	public class Location
	{
		public Point Grid;
		public string AlgebraicNotation;
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		//System.Diagnostics.Debug.WriteLine(_mouseState.X.ToString() + " " + _mouseState.Y.ToString());


		public override bool Equals(object obj)
		{
			if (!(obj is Location location))
				return false;

			if (Grid.X != location.Grid.X || Grid.Y != location.Grid.Y || AlgebraicNotation != location.AlgebraicNotation)
				return false;

			return true;
		}
	}
}
