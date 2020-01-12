using Chess.Sprites.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Queen : Piece
	{
		public Queen(Texture2D texture) : base(texture)
		{
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				var otherPieces = pieces.Where(res => !res.IsSelected).ToList();
				//_cellCheckerService = new CellCheckerService(this, otherPieces);
				//new CellCheckerService(this, otherPieces);
			}

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}
