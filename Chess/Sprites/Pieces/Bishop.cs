using Chess.Sprites.Cells;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Pieces
{
	public class Bishop : Piece
	{
		private readonly ILocationCheckerService _locationCheckerService;
		public Bishop(Texture2D texture, ILocationCheckerService locationCheckerService) : base(texture)
		{
			_locationCheckerService = locationCheckerService;
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard, Player player)
		{
			if (IsSelected)
			{
				SetAvailableLocations(pieces);
			}

			base.Update(gameTime, pieces, chessBoard, player);
		}

		public override void SetAvailableLocations(List<Piece> pieces)
		{
			AvailableLocations.Clear();
			AvailableLocations.AddRange(_locationCheckerService.CheckUpRight(Location, pieces, PieceColor));
			AvailableLocations.AddRange(_locationCheckerService.CheckDownRight(Location, pieces, PieceColor));
			AvailableLocations.AddRange(_locationCheckerService.CheckDownLeft(Location, pieces, PieceColor));
			AvailableLocations.AddRange(_locationCheckerService.CheckUpLeft(Location, pieces, PieceColor));
		}
	}
}
