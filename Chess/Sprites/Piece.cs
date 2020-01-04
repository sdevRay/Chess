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
		public bool IsSelected;
		public bool IsRemoved;
		private IEnumerable<Cell> _movementCells;
		public Piece(Texture2D texture) : base(texture)
		{
		}

		public virtual void SetMovementCells(IEnumerable<Cell> movementCells)
		{
			_movementCells = movementCells;
		}

		public override void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{
			if (sprites.OfType<Piece>().Where(res => res != this).Any(res => res.IsSelected))
				return; // ONLY EVALUATE THE SELECTED ONE

			var mouseState = Mouse.GetState();

			if (Rectangle.Contains(mouseState.Position))
			{
				if (mouseState.LeftButton == ButtonState.Pressed)
				{
					IsSelected = true;
				}

				if (mouseState.LeftButton == ButtonState.Released)
				{
					var cells = sprites.OfType<Cell>().Where(res => res.Rectangle.Contains(mouseState.Position));
					System.Diagnostics.Debug.WriteLine(cells.Count());

					foreach (var cell in cells)
					{
						Position = cell.CellOrigin(Texture);
					}

					IsSelected = false;
				}
			}

			if (IsSelected)
			{
				Position.X = mouseState.X - (Rectangle.Width / 2);
				Position.Y = mouseState.Y - (Rectangle.Height / 2);

				Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Global.SCREEN_WIDTH - Rectangle.Width, Global.SCREEN_HEIGHT - Rectangle.Height));
			}

			base.Update(gameTime, sprites);
		}
	}
}

