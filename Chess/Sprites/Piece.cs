using Chess.Types;
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
		public string AlgebraicNotation;
		private Vector2 _prevPosition;

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

					if ((sprite as Piece)._isSelected) // EXIT SO WE ONLY EVALUATE SELECTED PIECE IF IT'S NOT 'THIS'
						return;
				}
			}

			if (this._isSelected)
			{
				var originPosX = mouseState.X - (this.Rectangle.Width / 2);
				var originPosY = mouseState.Y - (this.Rectangle.Height / 2);
				this.Position.X = originPosX;
				this.Position.Y = originPosY;

				var intersectingCellAlgebraicNotation = sprites.OfType<Cell>().Where(res => this.Rectangle.Intersects(res.Rectangle)).Select(res => res.AlgebraicNotation).FirstOrDefault();

				var occupyingPiece = sprites.OfType<Piece>().Where(res => res != this).Where(res => res.Color == this.Color).Where(res => res.AlgebraicNotation.Equals(intersectingCellAlgebraicNotation)).FirstOrDefault();

				if(occupyingPiece != null)
				{
					System.Diagnostics.Debug.WriteLine(occupyingPiece.AlgebraicNotation);

				}

				this.Position = Vector2.Clamp(this.Position, new Vector2(0, 0), new Vector2(Chess.ScreenWidth - this.Rectangle.Width, Chess.ScreenHeight - this.Rectangle.Height));

				_prevPosition = new Vector2(this.Position.X, this.Position.Y);
			}

			if (this.Rectangle.Contains(mouseState.Position))
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
						var occupyingPiece = sprites.OfType<Piece>().Where(res => res.AlgebraicNotation.Equals(newCell.AlgebraicNotation)).FirstOrDefault();

						if (occupyingPiece != null)
						{
							if (this.Color.Equals(occupyingPiece.Color))
							{
								var startingCell = sprites.OfType<Cell>().Where(res => res.AlgebraicNotation == this.AlgebraicNotation).FirstOrDefault();

								if (startingCell != null)
								{
									this.Position = startingCell.CellOrigin(this.Texture);
									return;
								}
							}
							else
							{
								occupyingPiece.IsRemoved = true;
								return;
							}
						}

						this.AlgebraicNotation = newCell.AlgebraicNotation;
						this.Position = newCell.CellOrigin(this.Texture);
					}
				}
			}

			base.Update(gameTime, sprites);
		}
	}
}

