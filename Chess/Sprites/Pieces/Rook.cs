using Chess.Sprites.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Rook : Piece
	{
		private readonly ICellCheckerService _cellCheckerService;

		public Rook(Texture2D texture, ICellCheckerService cellCheckerService) : base(texture)
		{
			_cellCheckerService = cellCheckerService;
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				var otherPieces = pieces.Where(res => !res.IsSelected).ToList();
				_cellCheckerService.CellChecker(this, otherPieces);
			}

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}
