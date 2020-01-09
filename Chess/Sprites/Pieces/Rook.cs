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
				var horPieces = pieces.Where(res => res != this).Where(res => res.Location.Y == Location.Y);
				var verPieces = pieces.Where(res => res != this).Where(res => res.Location.X == Location.X);

				for (var x = Location.X; x >= Global.MIN_CELL_BOUNDARY; x--)
				{
					if (ProcessHorizontalCells(horPieces, x))
						break;
				}

				for (var x = Location.X; x <= Global.MAX_CELL_BOUNDARY; x++)
				{
					if (ProcessHorizontalCells(horPieces, x))
						break;
				}

				for (var y = Location.Y; y >= Global.MIN_CELL_BOUNDARY; y--)
				{
					if (ProcessVerticalCells(verPieces, y))
						break;
				}

				for (var y = Location.Y; y <= Global.MAX_CELL_BOUNDARY; y++)
				{
					if (ProcessVerticalCells(verPieces, y))
						break;
				}
			}

			base.Update(gameTime, pieces, chessBoard);
		}

		private bool ProcessVerticalCells(IEnumerable<Piece> verPieces, int y)
		{
			var add = false;
			var skip = false;

			foreach (var verPiece in verPieces)
			{
				if (verPiece.Location.Y == y)
				{
					add = true;
					skip = verPiece.PieceColor.Equals(PieceColor);
				}
			}

			if (!skip)
				AvailableLocations.Add(new Point(Location.X, y));

			return add;
		}

		private bool ProcessHorizontalCells(IEnumerable<Piece> horPieces, int x)
		{
			var add = false;
			var skip = false;

			foreach (var horPiece in horPieces)
			{
				if (horPiece.Location.X == x)
				{
					add = true;
					skip = horPiece.PieceColor.Equals(PieceColor);
				}
			}

			if (!skip)
				AvailableLocations.Add(new Point(x, Location.Y));

			return add;
		}
	}
}
