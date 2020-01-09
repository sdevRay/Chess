using Chess.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Pieces
{
	public class Pawn : Piece
	{
		public Point MovementRange = new Point(1, 1);
		public Pawn(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				var movDirY = Color == Color.White ? -MovementRange.Y : MovementRange.Y;
				var movementY = ((int)(Location.Y + movDirY)).Clamp();

				AvailableLocations.Add(new Point(Location.X, movementY));
			
			}

			base.Update(gameTime, pieces, chessBoard);
		}

	}
}
