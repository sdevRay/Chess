using Chess.Sprites.Cells;
using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Pawn : Piece
	{
		private readonly ILocationCheckerService _locationCheckerService;
		private readonly int _movementRange;
		public Pawn(Texture2D texture, ILocationCheckerService locationCheckerService, Point location) : base(texture)
		{
			_locationCheckerService = locationCheckerService;

			Location = location;

			if (Location.Y < Global.MAX_CELL_BOUNDARY * 0.5)
				_movementRange = 1;
			else
				_movementRange = -1;
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				AvailableLocations.AddRange(_locationCheckerService.CheckPawnRange(Location, _movementRange, pieces, PieceColor));
			}

			base.Update(gameTime, pieces, chessBoard);
		}

	}
}
