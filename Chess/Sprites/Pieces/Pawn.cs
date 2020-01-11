using Chess.Sprites.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Pieces
{
	public class Pawn : Piece
	{
		private readonly int _movementRange = 1;

		public Pawn(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				AvailableLocations.Add(new Point(Location.X, Location.Y + _movementRange));
			}

			base.Update(gameTime, pieces, chessBoard);
		}

	}
}
