using Chess.Sprites.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Queen : Piece
	{
		private readonly ILocationCheckerService _locationCheckerService;

		public Queen(Texture2D texture, ILocationCheckerService locationCheckerService) : base(texture)
		{
			_locationCheckerService = locationCheckerService;
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				var otherPieces = pieces.Where(res => !res.IsSelected).ToList();

				AvailableLocations = AvailableLocations
					.Concat(_locationCheckerService.CheckUpRight(Location, otherPieces))
					.Concat(_locationCheckerService.CheckDownRight(Location, otherPieces))
					.Concat(_locationCheckerService.CheckDownLeft(Location, otherPieces))
					.Concat(_locationCheckerService.CheckUpLeft(Location, otherPieces))
					.Concat(_locationCheckerService.CheckUp(Location, otherPieces))
					.Concat(_locationCheckerService.CheckRight(Location, otherPieces))
					.Concat(_locationCheckerService.CheckDown(Location, otherPieces))
					.Concat(_locationCheckerService.CheckLeft(Location, otherPieces))
					.ToList();
			}

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}
