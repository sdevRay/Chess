using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Rook : Piece
	{
		public Rook(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				CheckLeft();
				CheckRight();
				CheckUp();
				CheckDown();
			}

			base.Update(gameTime, pieces, chessBoard);
		}

		private void CheckDown()
		{
			for (var y = Location.Y; y <= Global.MAX_CELL_BOUNDARY; y++)
			{
				if (ProcessVerticalCells(y))
					break;
			}
		}

		private void CheckUp()
		{
			for (var y = Location.Y; y >= Global.MIN_CELL_BOUNDARY; y--)
			{
				if (ProcessVerticalCells(y))
					break;
			}
		}

		private void CheckRight()
		{
			for (var x = Location.X; x <= Global.MAX_CELL_BOUNDARY; x++)
			{
				if (ProcessHorizontalCells(x))
					break;
			}
		}

		private void CheckLeft()
		{
			for (var x = Location.X; x >= Global.MIN_CELL_BOUNDARY; x--)
			{
				if (ProcessHorizontalCells(x))
					break;
			}
		}

		private bool ProcessVerticalCells(int y)
		{
			var add = false;
			var skip = false;

			var otherPiece = OtherPieces.FirstOrDefault(res => res.Location.X == Location.X && res.Location.Y == y);
			if (otherPiece != null)
			{
				add = true;
				skip = otherPiece.PieceColor.Equals(PieceColor);
			}

			if (!skip)
				AvailableLocations.Add(new Point(Location.X, y));

			return add;
		}

		private bool ProcessHorizontalCells(int x)
		{
			var add = false;
			var skip = false;

			var otherPiece = OtherPieces.FirstOrDefault(res => res.Location.X == x && res.Location.Y == Location.Y);
			if (otherPiece != null)
			{
				add = true;
				skip = otherPiece.PieceColor.Equals(PieceColor);
			}

			if (!skip)
				AvailableLocations.Add(new Point(x, Location.Y));

			return add;
		}
	}
}
