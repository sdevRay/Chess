using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites
{
	public class Pawn : Piece
	{
		IEnumerable<Cell> movementCells;
		public Point MovementDirection = new Point(0, 1);
		public Pawn(Texture2D texture) : base(texture)
		{
		}

		public override void SetMovementCells(IEnumerable<Cell> movementCells)
		{
			base.SetMovementCells(movementCells);
		}

		public override void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{

			if (this.IsSelected)
			{
				var movDirY = this.Color == Color.White ? -MovementDirection.Y : MovementDirection.Y;



				movementCells = sprites.OfType<Cell>().Where(res => res.Location.Grid.X == this.Location.Grid.X && res.Location.Grid.Y == this.Location.Grid.Y + movDirY);


				SetMovementCells(movementCells);
			}

			base.Update(gameTime, sprites);
		}

	}
}
