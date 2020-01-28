using Chess.Sprites.Cells;
using Chess.Types.Enumerations;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Chess.LocationChecker;

namespace Chess.Sprites.Pieces
{
	public class Queen : Piece
	{
		private readonly ILocationCheckerService _locationCheckerService;
		public Queen(Texture2D texture, ILocationCheckerService locationCheckerService) : base(texture)
		{
			_locationCheckerService = locationCheckerService;
		}
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard, Player player)
		{
			if (IsSelected)
			{
				AvailableLocations.Clear();
				AvailableLocations = (GetAvailableLocations(Location, pieces, PieceColor));
			}

			base.Update(gameTime, pieces, chessBoard, player);
		}

		public override List<Point> GetAvailableLocations(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{

			if(AvailableLocations.Count() > 0)
			{
				return AvailableLocations;
			}
			else
			{
				return new List<List<Point>>()
				{
					_locationCheckerService.CheckUpRight(loc, pieces, pieceColor),
					_locationCheckerService.CheckDownRight(loc, pieces, pieceColor),
					_locationCheckerService.CheckDownLeft(loc, pieces, pieceColor),
					_locationCheckerService.CheckUpLeft(loc, pieces, pieceColor),
					_locationCheckerService.CheckUp(loc, pieces, pieceColor),
					_locationCheckerService.CheckRight(loc, pieces, pieceColor),
					_locationCheckerService.CheckDown(loc, pieces, pieceColor),
					_locationCheckerService.CheckLeft(loc, pieces, pieceColor)
				}.SelectMany(res => res).ToList();
			}
		}
	}
}
