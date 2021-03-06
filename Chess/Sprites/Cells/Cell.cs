﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chess.Sprites.Cells
{
	public class Cell : Sprite
	{
		public Color HighlightColor = Color.DarkSalmon;
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
		public Vector2 SetPieceOrigin(Texture2D pieceTexture)
		{
			return new Vector2(((Origin.X - (Rectangle.Width / 2)) + Rectangle.Width / 2) - pieceTexture.Width / 2, ((Origin.Y - (Rectangle.Height / 2)) + Rectangle.Height / 2) - pieceTexture.Height / 2);
		}
		public Cell(Texture2D texture) : base(texture)
		{
		}
	}
}
