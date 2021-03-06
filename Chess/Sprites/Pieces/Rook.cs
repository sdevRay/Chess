﻿using Chess.Sprites.Cells;
using Chess.LocationChecker;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Chess.Types.Enumerations;
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
		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard, Player player)
		{
			if (IsSelected || AvailableLocations == null)
			{
				AvailableLocations = GetAvailableLocations(Location, pieces, PieceColor);
			}

			base.Update(gameTime, pieces, chessBoard, player);
		}

		public override List<Point> GetAvailableLocations(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			return new List<List<Point>>()
				{
					_locationCheckerService.CheckRookRange(loc, pieces, pieceColor),
				}.SelectMany(res => res).ToList();
		}
	}
}
