using Chess.Types;
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

			var mouseHover = this.Rectangle.Contains(mouseState.Position);
			var isPressed = mouseState.LeftButton == ButtonState.Pressed;
			var isReleased = mouseState.LeftButton == ButtonState.Released;

			if (this.IsSelected)
			{
				var originPosX = mouseState.X - (this.Rectangle.Width / 2);
				var originPosY = mouseState.Y - (this.Rectangle.Height / 2);
				this.Position.X = originPosX;
				this.Position.Y = originPosY;

				this.Position = Vector2.Clamp(this.Position, new Vector2(0, 0), new Vector2(Global.SCREEN_WIDTH - this.Rectangle.Width, Global.SCREEN_HEIGHT - this.Rectangle.Height));

				_movementCells?.ToList().ForEach(res => res.Color = res.HighlightColor);
			} 
			else
			{
				_movementCells?.ToList().ForEach(res => res.Color = res.DefaultColor);
			}

			if (isPressed && mouseHover)
			{
				this.IsSelected = true;	
			}

			if (isReleased)
			{
				// TODO
				// CHECK MOVEMENT CELL FOR OCCUPYING PIECE
				// SET BOUNDRY AS TO NOT GO OUTSIDE CHESSBOARD 

				this.IsSelected = false;

				if (_movementCells != null)
				{
					foreach (var movementCell in _movementCells)
					{
						//System.Diagnostics.Debug.WriteLine(movementCell.Location.Grid.Y);
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

