using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites
{
	public class Piece : Sprite
	{
		private bool _isSelected;
		public string AlgebraicNotation;

		public Piece(Texture2D texture) : base(texture)
		{
			
		}

		public override void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{
			var mouseState = Mouse.GetState();
			var isPressed = mouseState.LeftButton == ButtonState.Pressed;
			var isReleased = mouseState.LeftButton == ButtonState.Released;

			foreach (var sprite in sprites)
			{
				if (sprite is Piece)
				{
					if (sprite == this)
						continue;

					if ((sprite as Piece)._isSelected)
						return;
				}
			}

			if (_isSelected)
			{
				Position.X = mouseState.X - (Rectangle.Width / 2);
				Position.Y = mouseState.Y - (Rectangle.Height / 2);
			}			

			if (Rectangle.Contains(mouseState.Position))
			{
				if (isPressed)
				{
					_isSelected = true;
				}

				if (isReleased)
				{
					_isSelected = false;

					foreach (var sprite in sprites)
					{
						if (sprite is Cell)
						{
							var cell = (sprite as Cell);
							if (cell.Rectangle.Intersects(this.Rectangle))
							{
								Position = cell.CellOrigin(this.Texture);
							}
						}
					}

				}
			}

			base.Update(gameTime, sprites);
		}
	}
}
