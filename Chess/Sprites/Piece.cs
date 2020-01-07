using Chess.Types.Constants;
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
		public Point StartingLocation;

		public bool IsSelected;
		public bool IsRemoved;
		public Piece(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, List<Piece> pieces, List<Cell> chessBoard)
		{
			var allOtherPieces = pieces.OfType<Piece>().Where(res => res != this);
			if (allOtherPieces.Any(res => res.IsSelected))
				return; // ONLY EVALUATE THE SELECTED ONE

			var newState = Mouse.GetState();

			//if (_locations != null)
			//{
			//	_locationCells = cells.Where(res => _locations.Contains(res.Location)).ToList();

			//	foreach (var lCell in _locationCells)
			//	{

			//		if (IsSelected)
			//			lCell.Color = lCell.HighlightColor;
			//		else
			//			lCell.Color = lCell.DefaultColor;
			//	}
			//}


			if (Rectangle.Contains(newState.Position))
			{
				if (newState.LeftButton == ButtonState.Pressed && _previousState.LeftButton == ButtonState.Released)
				{
					IsSelected = !IsSelected;

					if (!IsSelected)
					{


						var cell = chessBoard.FirstOrDefault(res => res.Rectangle.Contains(newState.Position));
						if (AvailableLocations.Contains(cell.Location))
						{
							Position = cell.CellOrigin(Texture);
							Location = cell.Location;
						}
						else
						{
							Position = chessBoard.FirstOrDefault(res => res.Location.Equals(Location)).CellOrigin(Texture);
						}


						//System.Diagnostics.Debug.WriteLine(availableLocation.ToString());

					}
				}
			}

			if (IsSelected)
			{
				chessBoard.ForEach(res => {
					if (AvailableLocations.Contains(res.Location))
					{
						res.Color = res.HighlightColor;
					}
				});

				Position.X = newState.X - (Rectangle.Width / 2);
				Position.Y = newState.Y - (Rectangle.Height / 2);

				Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Global.SCREEN_WIDTH - Rectangle.Width, Global.SCREEN_HEIGHT - Rectangle.Height));
			}

			_previousState = newState;

			base.Update(gameTime, pieces, chessBoard);
		}
	}
}

