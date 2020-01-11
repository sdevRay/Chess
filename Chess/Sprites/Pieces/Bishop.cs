using Chess.Sprites.Cells;
using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Bishop : Piece
	{
		private CellCheckerService _cellCheckerService;
		public Bishop(Texture2D texture) : base(texture)
		{
			_cellCheckerService = new CellCheckerService(this);
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{	
				int y = Location.Y, x = Location.X;
				AvailableLocations = _cellCheckerService.CheckDownLeft(x, y);

				CheckUpLeft(y, x);
				CheckUpRight(y, x);
				CheckDownRight(y, x);
				CheckDownLeft(y, x);
			}

			base.Update(gameTime, pieces, chessBoard);
		}

		private void CheckDownLeft(int y, int x)
		{
			do
			{
				if (ProcessCells(y, x))
					break;

				x -= 1;
				y++;

			} while (x >= Global.MIN_CELL_BOUNDARY);
		}

		private void CheckDownRight(int y, int x)
		{
			do
			{
				if (ProcessCells(y, x))
					break;

				x += 1;
				y++;

			} while (x <= Global.MAX_CELL_BOUNDARY);
		}

		private void CheckUpRight(int y, int x)
		{
			do
			{
				if (ProcessCells(y, x))
					break;

				x += 1;
				y--;

			} while (y >= Global.MIN_CELL_BOUNDARY);
		}

		private void CheckUpLeft(int y, int x)
		{
			do
			{
				if (ProcessCells(y, x))
					break;

				x -= 1;
				y--;

			} while (y >= Global.MIN_CELL_BOUNDARY);
		}

		private bool ProcessCells(int y, int x)
		{
			var add = false;

			var otherPiece = OtherPieces.FirstOrDefault(res => res.Location.X == x && res.Location.Y == y);
			if (otherPiece != null)
				add = true;
			

			AvailableLocations.Add(new Point(x, y));

			return add;
		}
	}
}
