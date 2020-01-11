using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Chess.Sprites.Cells
{
	public class CellCheckerService
	{
		public CellCheckerService(Piece piece)
		{
			System.Diagnostics.Debug.WriteLine(piece.ToString());

		}

		public List<Point> CheckDownLeft(int y, int x)
		{
			do
			{
				if (ProcessCells(y, x))
					break;

				x -= 1;
				y++;

			} while (x >= Global.MIN_CELL_BOUNDARY);
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
