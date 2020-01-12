using System.Collections.Generic;

namespace Chess.Sprites.Cells
{
	public interface ICellCheckerService
	{
		void CellChecker(Piece piece, List<Piece> otherPieces);
	}
}
