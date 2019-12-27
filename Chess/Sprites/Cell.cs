using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chess.Sprites
{
	public class Cell : Sprite
	{
		public string AlgebraicNotation;
		public Piece Piece;
		public Vector2 CellOrigin(Texture2D pieceTexture)
		{
				return new Vector2(((Origin.X - (Rectangle.Width / 2)) + this.Rectangle.Width / 2) - pieceTexture.Width / 2, ((Origin.Y - (Rectangle.Height / 2)) + this.Rectangle.Height / 2) - pieceTexture.Height / 2);
		}
		public Cell(Texture2D texture) : base(texture)
		{
		}


	}
}
