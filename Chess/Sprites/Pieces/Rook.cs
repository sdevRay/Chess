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
				var otherPieces = pieces.Where(res => res != this);

				CheckLeft(otherPieces);
				CheckRight(otherPieces);
				CheckUp(otherPieces);
				CheckDown(otherPieces);
			}

			base.Update(gameTime, pieces, chessBoard);
		}

		private void CheckDown(IEnumerable<Piece> otherPieces)
		{
			for (var y = Location.Y; y <= Global.MAX_CELL_BOUNDARY; y++)
			{
				if (ProcessVerticalCells(otherPieces, y))
					break;
			}
		}

		private void CheckUp(IEnumerable<Piece> otherPieces)
		{
			for (var y = Location.Y; y >= Global.MIN_CELL_BOUNDARY; y--)
			{
				if (ProcessVerticalCells(otherPieces, y))
					break;
			}
		}

		private void CheckRight(IEnumerable<Piece> otherPieces)
		{
			for (var x = Location.X; x <= Global.MAX_CELL_BOUNDARY; x++)
			{
				if (ProcessHorizontalCells(otherPieces, x))
					break;
			}
		}

		private void CheckLeft(IEnumerable<Piece> otherPieces)
		{
			for (var x = Location.X; x >= Global.MIN_CELL_BOUNDARY; x--)
			{
				if (ProcessHorizontalCells(otherPieces, x))
					break;
			}
		}

		private bool ProcessVerticalCells(IEnumerable<Piece> otherPieces, int y)
		{
			var add = false;
			var skip = false;

			var otherPiece = otherPieces.FirstOrDefault(res => res.Location.X == Location.X && res.Location.Y == y);
			if (otherPiece != null)
			{
				add = true;
				skip = otherPiece.PieceColor.Equals(PieceColor);
			}

			if (!skip)
				AvailableLocations.Add(new Point(Location.X, y));

			return add;
		}

		private bool ProcessHorizontalCells(IEnumerable<Piece> otherPieces, int x)
		{
			var add = false;
			var skip = false;

			var otherPiece = otherPieces.FirstOrDefault(res => res.Location.X == x && res.Location.Y == Location.Y);
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
