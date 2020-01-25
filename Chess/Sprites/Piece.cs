using Chess.Sprites.Cells;
using Chess.Sprites.Pieces;
using Chess.Types;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites
{
	public class Piece : Sprite
	{
		private MouseState _previousState;

		public List<Point> AvailableLocations = new List<Point>();

		public PieceColor PieceColor;
		public PieceType PieceType;

		public bool IsSelected;
		public bool IsRemoved;
		public Piece(Texture2D texture) : base(texture)
		{
		}

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
						chessBoard.Where(res => AvailableLocations.Contains(res.Location)).ToList().ForEach(res => res.Color = res.DefaultColor); // DEFAULT CELL COLORS

						var cell = chessBoard.FirstOrDefault(res => res.Rectangle.Contains(newState.Position)); // THE CELL THE MOUSE CURSER IS OVER

						if (AvailableLocations.Contains(cell.Location))
						{
							var occupyingPiece = pieces.Where(res => !res.IsSelected).FirstOrDefault(res => res.Location == cell.Location);
							if (occupyingPiece != null)
							{
								if (!occupyingPiece.PieceColor.Equals(PieceColor))
								{
									occupyingPiece.IsRemoved = true;
									NewLocation(cell, pieces, player);
								}
								else
								{
									PreviousLocation(chessBoard);
								}
							}
							else
							{
								NewLocation(cell, pieces, player);
							}
						}
						else
						{
							PreviousLocation(chessBoard);
						}
			
						AvailableLocations.Clear();

						//System.Diagnostics.Debug.WriteLine(availableLocation.ToString());
					}
				}
			}

			if (IsSelected)
			{
				chessBoard.Where(res => AvailableLocations.Contains(res.Location)).ToList().ForEach(res => res.Color = res.HighlightColor); // HIGHLIGHT CELL COLORS

				Position.X = newState.X - (Rectangle.Width / 2);
				Position.Y = newState.Y - (Rectangle.Height / 2);

				Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Global.SCREEN_WIDTH - Rectangle.Width, Global.SCREEN_HEIGHT - Rectangle.Height));
			}

			_previousState = newState;

			base.Update(gameTime, pieces, chessBoard, player);
		}

		public virtual void SetAvailableLocations(List<Piece> pieces)
		{
		//	return AvailableLocations;
		}

		private void PreviousLocation(List<Cell> chessBoard)
		{
			Position = chessBoard.FirstOrDefault(res => res.Location.Equals(Location)).SetPieceOrigin(Texture);
		}

		private void NewLocation(Cell cell, List<Piece> pieces, Player player)
		{
			if (PieceType.Pawn.Equals(PieceType))
				(this as Pawn).InitialMove = false;

			Position = cell.SetPieceOrigin(Texture);
			Location = cell.Location;
			
			if (PieceType.Equals(PieceType.Queen))
			{
				
				(this as Queen).SetAvailableLocations(pieces.Where(res => res != this).ToList());
			
					// FIND OUT IF THE ENEMY KING IS IN AVAIABLE LOCATIONS NOW
					var enemyKing = pieces.FirstOrDefault(res => AvailableLocations.Contains(res.Location) && !res.PieceColor.Equals(player.CurrentPlayerColor) && res.PieceType.Equals(PieceType.King));
					if(enemyKing != null)
					{
						// ENEMY KING IS IN CHECK
					}
				
			}

			// MAYBE TURN ON FLAG WHEN ENEMY IS IN CHECK
			// ONLY TURN OFF IF NEXT MOVE GETS OUT OF CHECK


			// once the piece has moved to a new location
			// check if the enemy king is in its new avaiable location
			// that means your in check
			// if so, the next move has to get the king out of check
			// if it cant, checkmate

			player.SwitchPlayerColor();
		}
	}
}

	