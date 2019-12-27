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
				this.Position.X = mouseState.X - (this.Rectangle.Width / 2);
				this.Position.Y = mouseState.Y - (this.Rectangle.Height / 2);

				this.Position = Vector2.Clamp(this.Position, new Vector2(0, 0), new Vector2(Chess.ScreenWidth - this.Rectangle.Width, Chess.ScreenHeight - this.Rectangle.Height));
			}

			if (Rectangle.Contains(mouseState.Position))
			{
				if (isPressed)
				{
					this._isSelected = true;
				}

				if (isReleased)
				{
					this._isSelected = false;

					var newCell = sprites.OfType<Cell>().Where(res => res.Rectangle.Intersects(this.Rectangle)).FirstOrDefault();

					if (newCell != null)
					{
						var occupyingPiece = sprites.OfType<Piece>().Any(res => res.AlgebraicNotation.Equals(newCell.AlgebraicNotation));

						if (occupyingPiece)
						{
							var startingCell = sprites.OfType<Cell>().Where(res => (res as Cell).AlgebraicNotation == this.AlgebraicNotation).FirstOrDefault();

							if (startingCell != null)
							{
								this.Position = startingCell.CellOrigin(this.Texture);
								return;
							}
						}
						else
						{
							this.AlgebraicNotation = newCell.AlgebraicNotation;
							this.Position = newCell.CellOrigin(this.Texture);
						}
					}
				}
			}
		
			base.Update(gameTime, sprites);
		}	
	}
}

