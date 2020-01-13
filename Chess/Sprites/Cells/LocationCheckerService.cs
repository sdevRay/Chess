using Chess.Types.Constants;
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
		public List<Point> CheckPawnRange(Point loc, int movementRange, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>() { loc };

			var verMov = new Point(loc.X, (loc.Y += movementRange));
			var horMovLeft = new Point(loc.X - 1, (loc.Y));
			var horMovRight = new Point(loc.X + 1, (loc.Y));

			//aLoc.AddRange(new List<Point>
			//
			//	new Point(loc.X, (loc.Y += movementRange)),
			//	new Point(loc.X - 1, (loc.Y)),
			//	new Point(loc.X + 1, (loc.Y)),
			//});

			foreach(var otherPiece in otherPieces)
			{
				if (!otherPiece.Location.Equals(verMov))
					aLoc.Add(verMov);

				if (otherPiece.Location.Equals(horMovLeft))
				{
					aLoc.Add(horMovLeft);

				}
				

				if (otherPiece.Location.Equals(horMovRight))
				{

				}

				aLoc.Add(horMovRight);



			}

			//var otherLocations = otherPieces
			//	.Where(res => aLoc.Contains(res.Location))
			//	.Select(res => res.Location)
			//	.ToList();

			//otherLocations.ForEach(res => aLoc.Remove(res));

			return aLoc;
		}

		public List<Point> CheckKingRange(Point loc, int movementRange)
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

			return aLoc;
		}

		public List<Point> CheckKnightRange(Point loc, int movementRange)
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

			return aLoc;
		}

		public List<Point> CheckDown(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var y = loc.Y; y <= Global.MAX_CELL_BOUNDARY; y++)
			{
				if (ProcessLocation(loc.X, y, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckUp(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var y = loc.Y; y >= Global.MIN_CELL_BOUNDARY; y--)
			{
				if (ProcessLocation(loc.X, y, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckRight(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var x = loc.X; x <= Global.MAX_CELL_BOUNDARY; x++)
			{
				if (ProcessLocation(x, loc.Y, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckLeft(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			for (var x = loc.X; x >= Global.MIN_CELL_BOUNDARY; x--)
			{
				if (ProcessLocation(x, loc.Y, aLoc, otherPieces))
					break;
			}

			return aLoc;
		}

		public List<Point> CheckDownLeft(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, otherPieces))
					break;

				loc.X -= 1;
				loc.Y++;

			} while (loc.X >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		public List<Point> CheckUpRight(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, otherPieces))
					break;

				loc.X += 1;
				loc.Y--;

			} while (loc.Y >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		public List<Point> CheckUpLeft(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, otherPieces))
					break;

				loc.X -= 1;
				loc.Y--;

			} while (loc.Y >= Global.MIN_CELL_BOUNDARY);

			return aLoc;
		}

		public List<Point> CheckDownRight(Point loc, List<Piece> otherPieces)
		{
			var aLoc = new List<Point>();

			do
			{
				if (ProcessLocation(loc.X, loc.Y, aLoc, otherPieces))
					break;

				loc.X += 1;
				loc.Y++;

			} while (loc.X <= Global.MAX_CELL_BOUNDARY);

			return aLoc;
		}

		private bool ProcessLocation(int x, int y, List<Point> aLoc, List<Piece> otherPieces)
		{
			var add = false;
			var loc = new Point(x, y);

			var otherPiece = otherPieces.FirstOrDefault(res => res.Location.Equals(loc));
			if (otherPiece != null)
				add = true;

			aLoc.Add(new Point(x, y));

			return add;
		}
	}
}
