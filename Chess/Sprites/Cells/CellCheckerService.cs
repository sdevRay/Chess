using Chess.Types;
using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Cells
{
	public class CellCheckerService : ICellCheckerService
	{
		private int _locX;
		private int _locY;

		public CellCheckerService()
		{
		}

		public void CellChecker(Piece piece, List<Piece> otherPieces)
		{
			_locX = piece.Location.X;
			_locY = piece.Location.Y;

			if (piece.PieceType.Equals(PieceType.Bishop) || piece.PieceType.Equals(PieceType.Queen))
			{
				piece.AvailableLocations.AddRange(CheckDownRight(piece.Location, otherPieces));
				piece.AvailableLocations.AddRange(CheckDownLeft(piece.Location, otherPieces));
				piece.AvailableLocations.AddRange(CheckUpRight(piece.Location, otherPieces));
				piece.AvailableLocations.AddRange(CheckUpLeft(piece.Location, otherPieces));
			}

			if (piece.PieceType.Equals(PieceType.Rook) || piece.PieceType.Equals(PieceType.Queen))
			{
				piece.AvailableLocations.AddRange(CheckDown(piece.Location, otherPieces));
				piece.AvailableLocations.AddRange(CheckUp(piece.Location, otherPieces));
				piece.AvailableLocations.AddRange(CheckRight(piece.Location, otherPieces));
				piece.AvailableLocations.AddRange(CheckLeft(piece.Location, otherPieces));
			}
		}

		private List<Point> CheckDown(int _locY, int _locX, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var y = _locY; y <= Global.MAX_CELL_BOUNDARY; y++)
			{
				if (ProcessVerticalCells(y, _locX, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		private List<Point> CheckUp(int _locY, int _locX, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var y = _locY; y >= Global.MIN_CELL_BOUNDARY; y--)
			{
				if (ProcessVerticalCells(y, _locX, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		private List<Point> CheckRight(int _locY, int _locX, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var x = _locX; x <= Global.MAX_CELL_BOUNDARY; x++)
			{
				if (ProcessHorizontalCells(x, _locY, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		private List<Point> CheckLeft(int _locY, int _locX, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var x = _locX; x >= Global.MIN_CELL_BOUNDARY; x--)
			{
				if (ProcessHorizontalCells(x, _locY, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		private bool ProcessVerticalCells(int y, int _locX, List<Point> aLoc, List<Piece> otherPieces)
		{
			var add = false;

			var otherPiece = otherPieces.FirstOrDefault(res => res.Location.X == _locX && res.Location.Y == y);
			if (otherPiece != null)
				add = true;

			aLoc.Add(new Point(_locX, y));

			return add;
		}

		private bool ProcessHorizontalCells(int x, int _locY, List<Point> aLoc, List<Piece> otherPieces)
		{
			var add = false;

			var otherPiece = otherPieces.FirstOrDefault(res => res.Location.X == x && res.Location.Y == _locY);
			if (otherPiece != null)
				add = true;

			aLoc.Add(new Point(x, _locY));

			return add;
		}

		private List<Point> CheckDownLeft(int y, int x, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessCells(y, x, aLoc, otherPieces))
					break;

				x -= 1;
				y++;

			} while (x >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}


		private List<Point> CheckUpRight(int y, int x, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessCells(y, x, aLoc, otherPieces))
					break;

				x += 1;
				y--;

			} while (y >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		private List<Point> CheckUpLeft(int y, int x, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessCells(y, x, aLoc, otherPieces))
					break;

				x -= 1;
				y--;

			} while (y >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		private List<Point> CheckDownRight(int y, int x, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessCells(y, x, aLoc, otherPieces))
					break;

				x += 1;
				y++;

			} while (x <= Global.MAX_CELL_BOUNDARY);

			return aLoc;
		}

		private bool ProcessCells(int y, int x, List<Point> aLoc, List<Piece> otherPieces)
		{
			var add = false;

			var otherPiece = otherPieces.FirstOrDefault(res => res.Location.X == x && res.Location.Y == y);
			if (otherPiece != null)
				add = true;

			aLoc.Add(new Point(x, y));

			return add;
		}
	}
}
