﻿using Chess.Sprites.Cells;
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
				AvailableLocations = AvailableLocations
					.Concat(_locationCheckerService.CheckUpRight(Location, pieces, PieceColor))
					.Concat(_locationCheckerService.CheckDownRight(Location, pieces, PieceColor))
					.Concat(_locationCheckerService.CheckDownLeft(Location, pieces, PieceColor))
					.Concat(_locationCheckerService.CheckUpLeft(Location, pieces, PieceColor))
					.Concat(_locationCheckerService.CheckUp(Location, pieces, PieceColor))
					.Concat(_locationCheckerService.CheckRight(Location, pieces, PieceColor))
					.Concat(_locationCheckerService.CheckDown(Location, pieces, PieceColor))
					.Concat(_locationCheckerService.CheckLeft(Location, pieces, PieceColor))
					.ToList();
			}

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}
