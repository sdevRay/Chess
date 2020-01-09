using Chess.Types;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
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
		public Point StartingLocation; // NOT USED YET

		public PieceColor PieceColor;
		public PieceType PieceType; // NOT USED YET

		public bool IsSelected;
		public bool IsRemoved;
		public Piece(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			//var allOtherPieces = pieces.Where(res => res != this);
			//if (allOtherPieces.Any(res => res.IsSelected))
			//	return; // ONLY EVALUATE THE SELECTED ONE

			var newState = Mouse.GetState();

			if (Rectangle.Contains(newState.Position))
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
							var occupyingPiece = pieces.Where(res => res != this).FirstOrDefault(res => res.Location == cell.Location);
							if (occupyingPiece != null)
								if (!occupyingPiece.PieceColor.Equals(PieceColor))
									occupyingPiece.IsRemoved = true;

							Position = cell.CellOrigin(Texture);
							Location = cell.Location;
						}
						else
						{
							Position = chessBoard.FirstOrDefault(res => res.Location.Equals(Location)).CellOrigin(Texture);
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

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}

