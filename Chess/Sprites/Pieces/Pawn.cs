using Chess.Sprites.Cells;
using Chess.LocationChecker;
using Chess.Types.Constants;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Chess.Types.Enumerations;
using System.Linq;

namespace Chess.Sprites.Pieces
{
	public class Pawn : Piece
	{
		public bool InitialMove = true;
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

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard, Player player)
		{
			if (IsSelected)
			{
				AvailableLocations = GetAvailableLocations(Location, pieces, PieceColor);
			}

			base.Update(gameTime, pieces, chessBoard, player);
		}

		public override List<Point> GetAvailableLocations(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			return new List<List<Point>>()
			{
				_locationCheckerService.CheckPawnRange(Location, _movementRange, pieces, PieceColor, InitialMove)
			}.SelectMany(res => res).ToList();
		}
	}
}
