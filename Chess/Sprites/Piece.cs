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

		private IEnumerable<Point> _locations;
		private IEnumerable<Cell> _locationCells;

		public bool IsSelected;
		public bool IsRemoved;
		public Piece(Texture2D texture) : base(texture)
		{
		}

		public virtual void SetLocations(IEnumerable<Point> locations)
		{
			_locations = locations;
		}

		public override void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{
			if (sprites.OfType<Piece>().Where(res => res != this).Any(res => res.IsSelected))
				return; // ONLY EVALUATE THE SELECTED ONE

			var newState = Mouse.GetState();

			var cells = sprites.OfType<Cell>();
			if (_locations != null)
			{
				_locationCells = cells.Where(res => _locations.Contains(res.Location));

				foreach (var lCell in _locationCells)
				{

					if (IsSelected)
						lCell.Color = lCell.HighlightColor;
					else
						lCell.Color = lCell.DefaultColor;
				}
			}

			if (Rectangle.Contains(newState.Position))
			{
				if (newState.LeftButton == ButtonState.Pressed && _previousState.LeftButton == ButtonState.Released)
				{
					IsSelected = !IsSelected;

					if (!IsSelected)
					{
						var locationCell = _locationCells.Where(res => res.Rectangle.Contains(newState.Position)).FirstOrDefault();
						if (locationCell != null)
						{
							Position = locationCell.CellOrigin(Texture);
							Location = locationCell.Location;
						}
						else
						{
							var startingCell = cells.Where(res => res.Location.Equals(Location)).FirstOrDefault();
							Position = startingCell.CellOrigin(Texture);
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

			base.Update(gameTime, sprites);
		}
	}
}

