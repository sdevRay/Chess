using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Pieces
{
	public class Bishop : Piece
	{
		public Bishop(Texture2D texture) : base(texture)
		{

		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}
