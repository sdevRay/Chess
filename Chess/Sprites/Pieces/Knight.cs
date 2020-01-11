using Chess.Sprites.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Pieces
{
	public class Knight : Piece
	{
		private readonly int _movementRange = 2;
		public Knight(Texture2D texture) : base(texture)
		{ 
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				{
					CheckVerticalRange();
					CheckHorizontalRange();
				}
			}

			base.Update(gameTime, pieces, chessBoard);
		}

		private void CheckHorizontalRange()
		{
			var rangePos = Location.X + _movementRange;
			var rangeNeg = Location.X + -_movementRange;
			var locYadd = Location.Y + 1;
			var locYsub = Location.Y - 1;

			AvailableLocations.Add(new Point(rangePos, locYadd));
			AvailableLocations.Add(new Point(rangePos, locYsub));
			AvailableLocations.Add(new Point(rangeNeg, locYadd));
			AvailableLocations.Add(new Point(rangeNeg, locYsub));
		}

		private void CheckVerticalRange()
		{
			var rangePos = Location.Y + _movementRange;
			var rangeNeg = Location.Y + -_movementRange;
			var locXadd = Location.X + 1;
			var locXsub = Location.X - 1;

			AvailableLocations.Add(new Point(locXadd, rangePos));
			AvailableLocations.Add(new Point(locXsub, rangePos));
			AvailableLocations.Add(new Point(locXadd, rangeNeg));
			AvailableLocations.Add(new Point(locXsub, rangeNeg));

		}
	}
}
