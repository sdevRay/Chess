using Chess.Sprites.Cells;
using Chess.Sprites.Pieces;
using Chess.Types;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites
{
	public class Piece : Sprite
	{
		private MouseState _previousState;

		public List<Point> AvailableLocations;

		public PieceColor PieceColor;
		public PieceType PieceType;

		public bool IsSelected;
		public bool IsRemoved;
		public Piece(Texture2D texture) : base(texture) { }

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard, Player player)
		{
			var newState = Mouse.GetState();

			if (Rectangle.Contains(newState.Position) && PieceColor.Equals(player.CurrentPlayerColor))
			{
				if (newState.LeftButton == ButtonState.Pressed && _previousState.LeftButton == ButtonState.Released)
				{
					IsSelected = !IsSelected;

					if (!IsSelected)
					{
						var cell = chessBoard.FirstOrDefault(res => res.Rectangle.Contains(newState.Position)); // THE CELL THE MOUSE CURSER IS OVER

						if (AvailableLocations.Contains(cell.Location))
						{
							ManagePieceMovement(cell, pieces, player, chessBoard);
							CheckForCheckmate(cell, pieces, player, chessBoard);
						}
						else // LOCATION IS NOT IN THE AVAILABLE LIST
						{
							PreviousLocation(chessBoard, player, Location);
						}
					}
				}
			}

			if (IsSelected)
			{
				Position.X = newState.X - (Rectangle.Width / 2);
				Position.Y = newState.Y - (Rectangle.Height / 2);

				Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Global.SCREEN_WIDTH - Rectangle.Width, Global.SCREEN_HEIGHT - Rectangle.Height));
			}

			_previousState = newState;

			base.Update(gameTime, pieces, chessBoard, player);
		}

		private void ManagePieceMovement(Cell cell, List<Piece> pieces, Player player, List<Cell> chessBoard)
		{
			var prevLocation = Location;
			var currentPlayerColor = player.CurrentPlayerColor;

			var piece = pieces.Find(res => res == this);
			piece.Location = cell.Location;
			UpdateAvailableLocations(pieces);

			var playerKing = pieces.OfType<King>().FirstOrDefault(res => currentPlayerColor.Equals(res.PieceColor));
			var opponentPiecesKingIntersection = pieces.Where(res => !currentPlayerColor.Equals(res.PieceColor)).Where(res => res.AvailableLocations.Contains(playerKing.Location));

			if (opponentPiecesKingIntersection.Count() > 0) // KING IN CHECK
			{
				var occupyingPiece = opponentPiecesKingIntersection.FirstOrDefault(res => res.Location == cell.Location);
				if (occupyingPiece != null)
				{
					var kingCheckPieces = new List<Piece>().Concat(pieces).ToList();
					kingCheckPieces.Remove(occupyingPiece);
					UpdateAvailableLocations(kingCheckPieces);

					var opponentPieceMovementRangeKingIntersection = kingCheckPieces.Where(res => !currentPlayerColor.Equals(res.PieceColor)).Any(res => res.AvailableLocations.Contains(playerKing.Location));

					if (opponentPieceMovementRangeKingIntersection)
					{
						PreviousLocation(chessBoard, player, prevLocation);
					}
					else
					{
						NewLocation(cell, player, prevLocation);
						occupyingPiece.IsRemoved = true;
					}
				}
				else
				{
					PreviousLocation(chessBoard, player, prevLocation);
				}
			}
			else
			{
				var occupyingPiece = pieces.Where(res => res != this).FirstOrDefault(res => res.Location == cell.Location);
				if (occupyingPiece != null)
				{
					if (occupyingPiece.PieceColor.Equals(PieceColor)) // FRIENDLY PIECE
					{
						PreviousLocation(chessBoard, player, prevLocation);
					}
					else
					{
						NewLocation(cell, player, prevLocation);
						occupyingPiece.IsRemoved = true;
					}
				}
				else
				{
					NewLocation(cell, player, prevLocation);
				}
			}

		}
		private void CheckForCheckmate(Cell cell, List<Piece> pieces, Player player, List<Cell> chessBoard) { 
			var kings = pieces.OfType<King>();

			foreach (var king in kings)
			{
				var enemyPieces = pieces.Where(res => !res.PieceColor.Equals(king.PieceColor) && res.IsRemoved == false);
				var enemyPieceAvailableLocations = enemyPieces.SelectMany(res => res.AvailableLocations);

				if (enemyPieceAvailableLocations.Contains(king.Location))
				{
					king.CheckStatus = true;

					if (king.AvailableLocations.Except(enemyPieceAvailableLocations).Count() == 0)
					{
						var enemyPieceThatHasKingInCheck = enemyPieces.FirstOrDefault(res => res.AvailableLocations.Contains(king.Location));
						if(enemyPieceThatHasKingInCheck != null)
						{
							var friendlyPieces = pieces.Where(res => res.PieceColor.Equals(king.PieceColor) && res.IsRemoved == false).SelectMany(res => res.AvailableLocations);
							var pinningPieceToKingMapping = PinningPieceToKingMapping(king.Location, enemyPieceThatHasKingInCheck.Location);

							if (!(friendlyPieces.Intersect(pinningPieceToKingMapping).Count() > 0))
							{
								player.GameStart = false;
							}
						}
					}
				}
				else
				{
					king.CheckStatus = false;
				}
			}
		}

		private List<Point> PinningPieceToKingMapping(Point kingLocaton, Point pinningEnemyLocation)
		{
			var kingMappingAvailableLocations = new List<Point>();
			var iterator = GetIterator(kingLocaton, pinningEnemyLocation);

			for (var i = 0; i < iterator; i++)
			{
				var xLoc = 0;
				var yLoc = 0;

				if (kingLocaton.X == pinningEnemyLocation.X)
				{
					xLoc = kingLocaton.X;
				}
				else
				{
					if (kingLocaton.X < pinningEnemyLocation.X)
					{
						xLoc = ++kingLocaton.X;
					}
					else
					{
						xLoc = --kingLocaton.X;
					}
				}

				if (kingLocaton.Y == pinningEnemyLocation.Y)
				{
					yLoc = kingLocaton.Y;
				}
				else
				{
					if (kingLocaton.Y > pinningEnemyLocation.Y)
					{
						yLoc = --kingLocaton.Y;
					}
					else
					{
						yLoc = ++kingLocaton.Y;
					}
				}

				kingMappingAvailableLocations.Add(new Point(xLoc, yLoc));
			}

			return kingMappingAvailableLocations;
		}

		private int GetIterator(Point kingLocaton, Point pinningEnemyLocation)
		{
			int iterator;
			if (kingLocaton.X == pinningEnemyLocation.X)
				iterator = Math.Abs(kingLocaton.Y - pinningEnemyLocation.Y);
			else
				iterator = Math.Abs(kingLocaton.X - pinningEnemyLocation.X);

			return iterator;
		}

		public virtual List<Point> GetAvailableLocations(Point loc, List<Piece> pieces, PieceColor pieceColor)
		{
			return new List<Point>();
		}

		private void PreviousLocation(List<Cell> chessBoard, Player player, Point prevLocation)
		{
			var cell = chessBoard.FirstOrDefault(res => res.Location.Equals(prevLocation));
			Position = cell.SetPieceOrigin(Texture);
			Location = cell.Location;
		}

		private void NewLocation(Cell cell, Player player, Point prevLocation)
		{
			Position = cell.SetPieceOrigin(Texture);
			Location = cell.Location;

			if (!cell.Location.Equals(prevLocation)) // Only switch player if its a new location, not the same one
			{
				if (PieceType.Equals(PieceType.Pawn))
					(this as Pawn).InitialMove = false;
				
				player.SwitchPlayerColor();
			}
		}

		private void UpdateAvailableLocations(List<Piece> pieces)
		{
			pieces.ForEach(res =>
			{
				res.AvailableLocations = res.GetAvailableLocations(res.Location, pieces, res.PieceColor);
			});
		}
	}
}

