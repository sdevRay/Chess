using Chess.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites
{
	public class Rook : Piece
	{
		public Point MovementRange = new Point(7, 7);
		public Rook(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{

			if (IsSelected)
			{
				
					for(var i = 0; i <= MovementRange.X; i++)
					{
						AvailableLocations.Add(new Point(i, Location.Y));
					}

					for (var i = 0; i <= MovementRange.Y; i++)
					{
						AvailableLocations.Add(new Point(Location.X, i));
					}
				

			}

			base.Update(gameTime, pieces, chessBoard);
		}

	}
}
