using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Chess.Sprites.Cells
{
	public interface ILocationCheckerService
	{
		List<Point> CheckPawnRange(Point loc, int movementRange, List<Piece> otherPieces);
		List<Point> CheckKingRange(Point loc, int movementRange);
		List<Point> CheckKnightRange(Point loc, int movementRange);
		List<Point> CheckDown(Point loc, List<Piece> otherPieces);
		List<Point> CheckUp(Point loc, List<Piece> otherPieces);
		List<Point> CheckRight(Point loc, List<Piece> otherPieces);
		List<Point> CheckLeft(Point loc, List<Piece> otherPieces);
		List<Point> CheckDownRight(Point loc, List<Piece> otherPieces);
		List<Point> CheckDownLeft(Point loc, List<Piece> otherPieces);
		List<Point> CheckUpRight(Point loc, List<Piece> otherPieces);
		List<Point> CheckUpLeft(Point loc, List<Piece> otherPieces);
	}
}
