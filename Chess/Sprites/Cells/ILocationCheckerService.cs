using Chess.Types.Enumerations;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Chess.Sprites.Cells
{
	public interface ILocationCheckerService
	{
		List<Point> CheckPawnRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor, bool firstMove);
		List<Point> CheckKingRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckKnightRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckDown(Point loc, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckUp(Point loc, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckRight(Point loc, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckLeft(Point loc, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckDownRight(Point loc, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckDownLeft(Point loc, List<Piece>pieces, PieceColor pieceColor);
		List<Point> CheckUpRight(Point loc, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckUpLeft(Point loc, List<Piece> pieces, PieceColor pieceColor);
	}
}
