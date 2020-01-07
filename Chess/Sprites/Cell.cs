using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chess.Sprites
{
	public class Cell : Sprite
	{
		public Color HighlightColor = Color.LightPink;
		private Color _defaultColor;
		public Color DefaultColor
		{
			get
			{
				return _defaultColor;
			}
			set
			{
				Color = value;
				_defaultColor = value;
			}
		}
		public Vector2 CellOrigin(Texture2D pieceTexture)
		{
			return new Vector2(((Origin.X - (Rectangle.Width / 2)) + this.Rectangle.Width / 2) - pieceTexture.Width / 2, ((Origin.Y - (Rectangle.Height / 2)) + this.Rectangle.Height / 2) - pieceTexture.Height / 2);
		}
		public Cell(Texture2D texture) : base(texture)
		{
		}
	}
}
