using Chess.Sprites;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.LocationChecker
{
	public class LocationCheckerService : ILocationCheckerService
	{
		public List<Point> CheckPawnRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor, bool initialMove)
		{
			var aLoc = new List<Point>() { loc };
			var otherPieces = GetOtherPieces(pieces).ToList();

			var forward = new Point(loc.X, loc.Y += movementRange);
			var forLeft = new Point(loc.X + 1, loc.Y);
			var forRight = new Point(loc.X - 1, loc.Y);

			var locY = forward.Y;

			if (initialMove)
			{
				var doubleForward = new Point(forward.X, locY += movementRange);

				if (!otherPieces.Any(res => res.Location.Equals(doubleForward)) && !otherPieces.Any(res => res.Location.Equals(forward)))
					aLoc.Add(doubleForward);
			}

			if (!otherPieces.Any(res => res.Location.Equals(forward)))
				aLoc.Add(forward);

			otherPieces.ForEach(res =>
			{
				if (res.Location.Equals(forLeft) || res.Location.Equals(forRight))
				{
					if (!res.PieceColor.Equals(pieceColor))
						aLoc.Add(res.Location);
				}
			});

			RemoveOutOfBounds(aLoc);

			return aLoc;
		}

		public List<Point> CheckKingRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>() { loc };

			var rangePos = loc.X + movementRange;
			var rangeVerAdd = loc.Y + movementRange;
			var rangeVerSub = loc.Y + -movementRange;
			var rangeNeg = loc.X + -movementRange;

			aLoc.AddRange(new List<Point>
			{
				new Point(rangePos, loc.Y),
				new Point(rangePos, rangeVerAdd),
				new Point(rangePos, rangeVerSub),
				new Point(loc.X, rangeVerAdd),
				new Point(loc.X, rangeVerSub),
				new Point(rangeNeg, loc.Y),
				new Point(rangeNeg, rangeVerAdd),
				new Point(rangeNeg, rangeVerSub),
			});

			RemoveOutOfBounds(aLoc);

			return RemoveColorMatchingPieceLocations(pieces, pieceColor, aLoc);
		}

		public List<Point> CheckKnightRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>() { loc };

			// CHECK HORIZONTAL RANGE
			var rangePos = loc.X + movementRange;
			var rangeNeg = loc.X + -movementRange;
			var locYadd = loc.Y + 1;
			var locYsub = loc.Y - 1;

			aLoc.AddRange(new List<Point>
			{
				new Point(rangePos, locYadd),
				new Point(rangePos, locYsub),
				new Point(rangeNeg, locYadd),
				new Point(rangeNeg, locYsub)
			});

			//CHECK VERTICAL RANGE
			rangePos = loc.Y + movementRange;
			rangeNeg = loc.Y + -movementRange;
			var locXadd = loc.X + 1;
			var locXsub = loc.X - 1;

			aLoc.AddRange(new List<Point>
			{
				new Point(locXadd, rangePos),
				new Point(locXsub, rangePos),
				new Point(locXadd, rangeNeg),
				new Point(locXsub, rangeNeg)
			});

			RemoveOutOfBounds(aLoc);

			return RemoveColorMatchingPieceLocations(pieces, pieceColor, aLoc);
		}

		private static void RemoveOutOfBounds(List<Point> aLoc)
		{
			aLoc.RemoveAll(res => res.X > Global.MAX_CELL_BOUNDARY || res.Y > Global.MAX_CELL_BOUNDARY || res.X < Global.MIN_CELL_BOUNDARY || res.Y < Global.MIN_CELL_BOUNDARY);
		}

		public List<Point> CheckBishopRange(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLocs = new List<Point>() { loc };
			var otherPieces = GetOtherPieces(pieces);

			CheckDownLeft(loc, pieceColor, aLocs, otherPieces);

			CheckDownRight(loc, pieceColor, aLocs, otherPieces);

			CheckUpLeft(loc, pieceColor, aLocs, otherPieces);

			CheckUpRight(loc, pieceColor, aLocs, otherPieces);

			return aLocs;
		}

		private static void CheckDownLeft(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			var x = loc.X;
			var y = loc.Y;

			while (true)
			{
				if (!(x >= Global.MIN_CELL_BOUNDARY) || !(y <= Global.MAX_CELL_BOUNDARY))
					break;

				var aLoc = new Point(x--, y++);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		private static void CheckDownRight(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			var x = loc.X;
			var y = loc.Y;

			while (true)
			{
				if (!(x <= Global.MAX_CELL_BOUNDARY) || !(y <= Global.MAX_CELL_BOUNDARY))
					break;

				var aLoc = new Point(x++, y++);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		private static void CheckUpLeft(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			var x = loc.X;
			var y = loc.Y;

			while (true)
			{
				if (!(x >= Global.MIN_CELL_BOUNDARY) || !(y >= Global.MIN_CELL_BOUNDARY))
					break;

				var aLoc = new Point(x--, y--);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		private static void CheckUpRight(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			var x = loc.X;
			var y = loc.Y;

			while (true)
			{
				if (!(x <= Global.MAX_CELL_BOUNDARY) || !(y >= Global.MIN_CELL_BOUNDARY))
					break;

				var aLoc = new Point(x++, y--);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		public List<Point> CheckRookRange(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLocs = new List<Point>() { loc };

			var otherPieces = GetOtherPieces(pieces);

			CheckRight(loc, pieceColor, aLocs, otherPieces);

			CheckDown(loc, pieceColor, aLocs, otherPieces);

			CheckLeft(loc, pieceColor, aLocs, otherPieces);

			CheckUp(loc, pieceColor, aLocs, otherPieces);

			return aLocs;
		}

		private static void CheckRight(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			for (var x = loc.X; x <= Global.MAX_CELL_BOUNDARY; x++) // CHECK RIGHT
			{
				var aLoc = new Point(x, loc.Y);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		private static void CheckDown(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			for (var y = loc.Y; y <= Global.MAX_CELL_BOUNDARY; y++) // CHECK DOWN
			{
				var aLoc = new Point(loc.X, y);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		private static void CheckLeft(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			for (var x = loc.X; x >= Global.MIN_CELL_BOUNDARY; x--) // CHECK LEFT
			{
				var aLoc = new Point(x, loc.Y);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		private static void CheckUp(Point loc, PieceColor pieceColor, List<Point> aLocs, IEnumerable<Piece> otherPieces)
		{
			for (var y = loc.Y; y >= Global.MIN_CELL_BOUNDARY; y--) // CHECK UP
			{
				var aLoc = new Point(loc.X, y);

				if (aLoc.Equals(loc))
					continue;

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && res.PieceColor.Equals(pieceColor)))
					break;

				aLocs.Add(aLoc);

				if (otherPieces.Any(res => res.Location.Equals(aLoc) && !res.PieceColor.Equals(pieceColor)))
					break;
			}
		}

		private IEnumerable<Piece> GetOtherPieces(List<Piece> pieces)
		{
			return pieces.Where(res => !res.IsSelected);
		}

		private List<Point> RemoveColorMatchingPieceLocations(List<Piece> pieces, PieceColor pieceColor, List<Point> aLoc)
		{
			var colorMatchingPieceLocations =
			GetOtherPieces(pieces)
			.Where(res => aLoc.Contains(res.Location) && res.PieceColor.Equals(pieceColor))
			.Select(res => res.Location);

			return aLoc.Where(res => !colorMatchingPieceLocations.Contains(res)).ToList();
		}
	}
}
