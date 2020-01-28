using Chess.Sprites.Cells;
using Chess.LocationChecker;
using Chess.Types.Enumerations;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class King : Piece
	{
		private readonly ILocationCheckerService _locationCheckerService;
		private readonly int _movementRange = 1;

		public King(Texture2D texture, ILocationCheckerService locationCheckerService) : base(texture)
		{
			_locationCheckerService = locationCheckerService;
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard, Player player)
		{
			if (IsSelected)
			{
				AvailableLocations.Clear();
				AvailableLocations.AddRange(GetAvailableLocations(Location, pieces, PieceColor));
			}

			base.Update(gameTime, pieces, chessBoard, player);
		}

		public override List<Point> GetAvailableLocations(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{

			if (AvailableLocations.Count() > 0)
			{
				return AvailableLocations;
			}
			else
			{
				return _locationCheckerService.CheckKingRange(loc, _movementRange, pieces, pieceColor);
			}
		}
	}
}
