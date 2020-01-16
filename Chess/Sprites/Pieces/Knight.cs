using Chess.Sprites.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Knight : Piece
	{
		private readonly ILocationCheckerService _locationCheckerService;
		private readonly int _movementRange = 2;

		public Knight(Texture2D texture, ILocationCheckerService locationCheckerService) : base(texture)
		{
			_locationCheckerService = locationCheckerService;
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				{
					AvailableLocations.AddRange(_locationCheckerService.CheckKnightRange(Location, _movementRange, pieces, PieceColor));
				}
			}

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}
