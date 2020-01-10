using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Bishop : Piece
	{
		public Bishop(Texture2D texture) : base(texture)
		{
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{	
				int y = Location.Y, x = Location.X;

				var otherPieces = pieces.Where(res => res != this);

				CheckUpLeft(y, x, otherPieces);
				CheckUpRight(y, x, otherPieces);
				CheckDownRight(y, x, otherPieces);
				CheckDownLeft(y, x, otherPieces);
			}

			base.Update(gameTime, pieces, chessBoard);
		}

		private void CheckDownLeft(int y, int x, IEnumerable<Piece> otherPieces)
		{
			do
			{
				if (ProcessCells(y, x, otherPieces))
					break;

				x -= 1;
				y++;

			} while (x >= Global.MIN_CELL_BOUNDARY);
		}

		private void CheckDownRight(int y, int x, IEnumerable<Piece> otherPieces)
		{
			do
			{
				if (ProcessCells(y, x, otherPieces))
					break;

				x += 1;
				y++;

			} while (x <= Global.MAX_CELL_BOUNDARY);
		}

		private void CheckUpRight(int y, int x, IEnumerable<Piece> otherPieces)
		{
			do
			{
				if (ProcessCells(y, x, otherPieces))
					break;

				x += 1;
				y--;

			} while (y >= Global.MIN_CELL_BOUNDARY);
		}

		private void CheckUpLeft(int y, int x, IEnumerable<Piece> otherPieces)
		{
			do
			{
				if (ProcessCells(y, x, otherPieces))
					break;

				x -= 1;
				y--;

			} while (y >= Global.MIN_CELL_BOUNDARY);
		}

		private bool ProcessCells(int y, int x, IEnumerable<Piece> otherPieces)
		{
			var skip = false;
			var add = false;

			var otherPiece = otherPieces.FirstOrDefault(res => res.Location.X == x && res.Location.Y == y);
			if (otherPiece != null)
			{
				add = true;
				skip = otherPiece.PieceColor.Equals(PieceColor);
			}

			if (!skip)
				AvailableLocations.Add(new Point(x, y));

			return add;
		}
	}
}
