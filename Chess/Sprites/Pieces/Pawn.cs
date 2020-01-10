using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Pawn : Piece
	{
		public Pawn(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				//var movDirY = Location.Y > (Location.Y / Global.MAX_CELL_BOUNDARY) ? -1 : 1;

				AvailableLocations.Add(new Point(Location.X, Location.Y + movDirY));

				//if (otherPiece != null)
				//{
				//	add = true;
				//	skip = otherPiece.PieceColor.Equals(PieceColor);
				//}

				//if (!skip)
				//	AvailableLocations.Add(new Point(Location.X, y));

			}

			base.Update(gameTime, pieces, chessBoard);
		}

	}
}
