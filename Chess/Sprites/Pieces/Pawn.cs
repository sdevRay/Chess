using Chess.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites
{
	public class Pawn : Piece
	{
		public Point MovementRange = new Point(1, 1);
		public Pawn(Texture2D texture) : base(texture)
		{
		}

		public override void SetLocations(IEnumerable<Point> locations)
		{
			base.SetLocations(locations);
		}

		public override void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{
			if (IsSelected)
			{
				var movDirY = Color == Color.White ? -MovementRange.Y : MovementRange.Y;
				var movementY = ((int)(Location.Y + movDirY)).Clamp();

				var locations = new List<Point>
				{
					new Point(Location.X, movementY),
					new Point(Location.X + MovementRange.X, movementY),
					new Point(Location.X + -MovementRange.X, movementY)
				};

				SetLocations(locations);

			}
			base.Update(gameTime, sprites);
		}

	}
}
