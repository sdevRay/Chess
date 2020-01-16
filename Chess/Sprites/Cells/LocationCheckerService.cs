using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Cells
{
	public class LocationCheckerService : ILocationCheckerService
	{
		public LocationCheckerService()
		{
		}
		public List<Point> CheckPawnRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>() { loc };
			var otherPieces = GetOtherPieces(pieces);

			var forward = new Point(loc.X, loc.Y += movementRange);
			var forLeft = new Point(loc.X + 1, loc.Y);
			var forRight = new Point(loc.X - 1, loc.Y);

			if(!otherPieces.Any(res => res.Location.Equals(forward)))
				aLoc.Add(forward);

			otherPieces.ForEach(res => { 
				if(res.Location.Equals(forLeft) || res.Location.Equals(forRight))
				{
					if (!res.PieceColor.Equals(pieceColor))
						aLoc.Add(res.Location);
				}
			});

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

			return RemoveColorMatchingPieceLocations(pieces, pieceColor, aLoc);
		}

		public List<Point> CheckDown(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			for (var y = loc.Y; y <= Global.MAX_CELL_BOUNDARY; y++)
			{
				if (ProcessLocation(loc.X, y, aLoc, pieces, pieceColor))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckUp(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			for (var y = loc.Y; y >= Global.MIN_CELL_BOUNDARY; y--)
			{
				if (ProcessLocation(loc.X, y, aLoc, pieces, pieceColor))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckRight(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			for (var x = loc.X; x <= Global.MAX_CELL_BOUNDARY; x++)
			{
				if (ProcessLocation(x, loc.Y, aLoc, pieces, pieceColor))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckLeft(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			for (var x = loc.X; x >= Global.MIN_CELL_BOUNDARY; x--)
			{
				if (ProcessLocation(x, loc.Y, aLoc, pieces, pieceColor))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckDownLeft(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, pieces, pieceColor))
					break;

				loc.X -= 1;
				loc.Y++;

			} while (loc.X >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		public List<Point> CheckUpRight(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, pieces, pieceColor))
					break;

				loc.X += 1;
				loc.Y--;

			} while (loc.Y >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		public List<Point> CheckUpLeft(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, pieces, pieceColor))
					break;

				loc.X -= 1;
				loc.Y--;

			} while (loc.Y >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		public List<Point> CheckDownRight(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, pieces, pieceColor))
					break;

				loc.X += 1;
				loc.Y++;

			} while (loc.X <= Global.MAX_CELL_BOUNDARY);

			return aLoc;
		}

		private static List<Piece> GetOtherPieces(List<Piece> pieces)
		{
			return pieces.Where(res => !res.IsSelected).ToList();
		}

		private static List<Point> RemoveColorMatchingPieceLocations(List<Piece> pieces, PieceColor pieceColor, List<Point> aLoc)
		{
			var colorMatchingPieceLocations = GetOtherPieces(pieces)
				.Where(res => aLoc.Contains(res.Location))
				.Where(res => res.PieceColor.Equals(pieceColor))
				.Select(res => res.Location);

			return aLoc.Where(res => !colorMatchingPieceLocations.Contains(res)).ToList();
		}

		private static bool ProcessLocation(int x, int y, List<Point> aLoc, List<Piece> pieces, PieceColor pieceColor)
		{
			var add = false;
			var skip = false;
			var loc = new Point(x, y);

			var otherPiece = GetOtherPieces(pieces).FirstOrDefault(res => res.Location.Equals(loc));
			if (otherPiece != null)
			{
				add = true;
				if(otherPiece.PieceColor.Equals(pieceColor))
					skip = true;
			}

			if(!skip)
				aLoc.Add(new Point(x, y));

			return add;
		}
	}
}
