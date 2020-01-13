using Chess.Sprites.Cells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Rook : Piece
	{
		private readonly ILocationCheckerService _locationCheckerService;
		public Rook(Texture2D texture, ILocationCheckerService locationCheckerService) : base(texture)
		{
			_locationCheckerService = locationCheckerService;
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			if (IsSelected)
			{
				var otherPieces = pieces.Where(res => !res.IsSelected).ToList();

				AvailableLocations = AvailableLocations
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
