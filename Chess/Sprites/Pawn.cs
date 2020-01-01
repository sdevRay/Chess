using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Sprites
{
	public class Pawn : Piece
	{
		public Pawn(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{
			if(this.)
			System.Diagnostics.Debug.WriteLine("HIT ME");
			var test = sprites.OfType<Cell>().Where(res => res.Location.Row == (this.Location.Row + 1)).FirstOrDefault();
			test.Color = Color.Green;

			base.Update(gameTime, sprites);
		}

	}
}
