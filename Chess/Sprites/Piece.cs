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
		public bool IsRemoved;

		public Piece(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{
			if (sprites.OfType<Piece>().Where(res => res != this).Any(res => res._isSelected))
				return; // ONLY EVALUATE THE SELECTED ONE

			var mouseState = Mouse.GetState();

			var mouseHover = this.Rectangle.Contains(mouseState.Position);
			var isPressed = mouseState.LeftButton == ButtonState.Pressed;
			var isReleased = mouseState.LeftButton == ButtonState.Released;

			if (this._isSelected)
			{
				var originPosX = mouseState.X - (this.Rectangle.Width / 2);
				var originPosY = mouseState.Y - (this.Rectangle.Height / 2);
				this.Position.X = originPosX;
				this.Position.Y = originPosY;

				this.Position = Vector2.Clamp(this.Position, new Vector2(0, 0), new Vector2(Chess.ScreenWidth - this.Rectangle.Width, Chess.ScreenHeight - this.Rectangle.Height));
			}

			if (isPressed && mouseHover)
			{
				this._isSelected = true;
			}

			if (isReleased && mouseHover)
			{
				this._isSelected = false;

				var targetCell = sprites.OfType<Cell>().Where(res => res.Rectangle.Contains(mouseState.Position)).FirstOrDefault();

				if (targetCell != null)
				{
					var occupyingPiece = sprites.OfType<Piece>().Where(res => res != this).Where(res => res.Location.Equals(targetCell.Location)).FirstOrDefault();

					if (occupyingPiece != null)
					{
						if (occupyingPiece.Color.Equals(this.Color))
						{
							var originCell = sprites.OfType<Cell>().Where(res => res.Location.Equals(this.Location)).FirstOrDefault();

							if (originCell != null)
							{
								this.Position = originCell.CellOrigin(this.Texture);
								return;
							}
						}

						occupyingPiece.IsRemoved = true;
						return;
					}

					this.Location = targetCell.Location;
					this.Position = targetCell.CellOrigin(this.Texture);
				}
			}

			base.Update(gameTime, sprites);
		}
	}
}

