using Chess.Sprites;
using Chess.Types.Enumerations;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Chess.LocationChecker
{
	public interface ILocationCheckerService
	{
		List<Point> CheckPawnRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor, bool firstMove);
		List<Point> CheckKingRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckKnightRange(Point loc, int movementRange, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckRookRange(Point loc, List<Piece> pieces, PieceColor pieceColor);
		List<Point> CheckBishopRange(Point loc, List<Piece> pieces, PieceColor pieceColor);
	}
}
