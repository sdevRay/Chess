﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Chess.Sprites
{
	public class Sprite
	{
		public Texture2D Texture;

		public Vector2 Position;
		//public Vector2 Velocity;
		 
		//public float Speed;
		//public Input Input;
		public Color Color = Color.Blue;	

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

		public virtual void Update(GameTime gameTime, IEnumerable<Sprite> sprites)
		{

		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Color);
		}
	}
}