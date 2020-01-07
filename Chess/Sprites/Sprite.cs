using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites
{
	public class Sprite
	{
		public Texture2D Texture;
		public Vector2 Position;
		public Point Location;
		public Color Color;	

		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
			}
		}

		public Point Origin
		{
			get
			{
				return Rectangle.Center;
			}
		}

		public Sprite(Texture2D texture)
		{
			Texture = texture;
		}

		public virtual void Update(GameTime gameTime, List<Piece> sprites, List<Cell> chessBoard)
		{
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Color);
		}

		public override string ToString()
		{
			return $"{GetType().Name} X: {Location.X} Y: {Location.Y}";
		}
	}
}
