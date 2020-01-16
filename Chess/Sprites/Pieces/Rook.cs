﻿using Chess.Sprites.Cells;
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
				AvailableLocations = AvailableLocations
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
