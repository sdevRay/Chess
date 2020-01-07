using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites
{
	public class Rook : Piece
	{
		public Point MovementRange = new Point(7, 7);
		public Rook(Texture2D texture) : base(texture)
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
				var locations = new List<Point>();
				
				for(var i = 0; i <= MovementRange.X; i++)
				{
					locations.Add(new Point(i, Location.Y));
				}

				for (var i = 0; i <= MovementRange.Y; i++)
				{
					locations.Add(new Point(Location.X, i));
				}

				SetLocations(locations);
			}

			base.Update(gameTime, sprites);
		}

	}
}
