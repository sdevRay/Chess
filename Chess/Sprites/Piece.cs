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
		public bool IsSelected;
		public bool IsRemoved;
		private IEnumerable<Cell> _movementCells;
		public Type type;
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

			var mouseHover = this.Rectangle.Contains(mouseState.Position);
			var isPressed = mouseState.LeftButton == ButtonState.Pressed;
			var isReleased = mouseState.LeftButton == ButtonState.Released;

			if (this.IsSelected)
			{
				var originPosX = mouseState.X - (this.Rectangle.Width / 2);
				var originPosY = mouseState.Y - (this.Rectangle.Height / 2);
				this.Position.X = originPosX;
				this.Position.Y = originPosY;

				this.Position = Vector2.Clamp(this.Position, new Vector2(0, 0), new Vector2(Chess.ScreenWidth - this.Rectangle.Width, Chess.ScreenHeight - this.Rectangle.Height));
			}



			if (isPressed && mouseHover)
			{

				this.IsSelected = true;	
				_movementCells?.ToList().ForEach(res => res.Color = res.HighlightColor);
			}

			if (isReleased)
			{
				this.IsSelected = false;

				_movementCells?.ToList().ForEach(res => res.Color = res.DefaultColor);

				//var targetCell = sprites.OfType<Cell>().Where(res => res.Rectangle.Contains(mouseState.Position)).FirstOrDefault();


				if (_movementCells != null)
				{
					foreach (var movementCell in _movementCells)
					{


						if (movementCell.Rectangle.Contains(this.Rectangle))
						{
							this.Position = movementCell.CellOrigin(this.Texture);
							this.Location = movementCell.Location;
						}
						else
						{
						var originCell = sprites.OfType<Cell>().Where(res => res.Location.Equals(this.Location)).FirstOrDefault();
						this.Position = originCell.CellOrigin(this.Texture);
						}
					}
				}

					//this.Position = originCell.CellOrigin(this.Texture);
					//return;
				


				//var occupyingPiece = sprites.OfType<Piece>().Where(res => res != this).Where(res => res.Location.Equals(targetCell.Location)).FirstOrDefault();

				//if (occupyingPiece != null)
				//{
				//	if (occupyingPiece.Color.Equals(this.Color))
				//	{
				//		var originCell = sprites.OfType<Cell>().Where(res => res.Location.Equals(this.Location)).FirstOrDefault();

				//		if (originCell != null)
				//		{
				//			this.Position = originCell.CellOrigin(this.Texture);
				//			return;
				//		}
				//	}

				//	occupyingPiece.IsRemoved = true;
				//	return;
				//}

				//this.Location = targetCell.Location;
				//this.Position = targetCell.CellOrigin(this.Texture);

			}

			base.Update(gameTime, sprites);
		}
	}
}

