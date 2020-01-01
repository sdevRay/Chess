using Chess.Models;
using Chess.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Chess
{
	public class Chess : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public static int ScreenWidth = 800;
		public static int ScreenHeight = 800;

		private List<Sprite> _sprites;

		private readonly int _cellCount = 8;
		private int _cellWidth;
		private int _cellHeight;


		public Chess()
		{
			IsMouseVisible = true;

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = ScreenWidth;
			graphics.PreferredBackBufferHeight = ScreenHeight;
			graphics.ApplyChanges();

		}
		protected override void Initialize()
		{
			_cellWidth = ScreenWidth / _cellCount;
			_cellHeight = ScreenHeight / _cellCount;

			spriteBatch = new SpriteBatch(GraphicsDevice);
			_sprites = new List<Sprite>();

			base.Initialize();
		}
		protected override void LoadContent()
		{

			var cellTexture = Content.Load<Texture2D>("Cell");

			BuildChessBoard(cellTexture);

			var pawnTexture = Content.Load<Texture2D>("Pawn");

			foreach (var sprite in _sprites.ToArray())
			{
				if (sprite is Cell)
				{
					var cell = (sprite as Cell);

					if (cell.Location.Row == 1)
					{
						if(cell.Location.Column == 0)
						{
							var piece = new Pawn(pawnTexture)
							{
								Position = cell.CellOrigin(pawnTexture),
								Color = Color.Black,
								Location = new Location()
								{
									Row = cell.Location.Row,
									Column = cell.Location.Column,
									AlgebraicNotation = cell.Location.AlgebraicNotation
								}
							};

							_sprites.Add(piece);
						}
		
					}

					if(cell.Location.Row == 6)
					{
						if (cell.Location.Column == 1)
						{
							var piece = new Pawn(pawnTexture)
							{
								Position = cell.CellOrigin(pawnTexture),
								Color = Color.White,
								Location = new Location()
								{
									Row = cell.Location.Row,
									Column = cell.Location.Column,
									AlgebraicNotation = cell.Location.AlgebraicNotation
								}
							};

							_sprites.Add(piece);
						}
					}
				}
			}
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			//System.Diagnostics.Debug.WriteLine(_mouseState.X.ToString() + " " + _mouseState.Y.ToString());

			foreach (var sprite in _sprites)
			{
				sprite.Update(gameTime, _sprites);
			}

			PostProcess();

			base.Update(gameTime);
		}

		private void PostProcess()
		{
			for (var i = 0; i < _sprites.Count; i++)
			{
				var sprite = _sprites[i];
				if (sprite is Piece)
				{
					if ((sprite as Piece).IsRemoved)
					{
						_sprites.RemoveAt(i);
						i--;
					}
				}
			}
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			foreach (var sprite in _sprites)
			{
				sprite.Draw(spriteBatch);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void BuildChessBoard(Texture2D cellTexture)
		{
			var board = new Cell[_cellCount, _cellCount];

			for (var i = 0; i < board.GetLength(0); i++)
			{
				var anRow = _cellCount - i;

				for (var x = 0; x < board.GetLength(1); x++)
				{
					Color cellColor;
					var evenRow = (i % 2 == 0);
					var evenColumn = (x % 2 == 0);

					if (evenRow)
					{
						if (evenColumn)
						{
							cellColor = Color.White;
						}
						else
						{
							cellColor = Color.LightBlue;
						}
					}
					else // Odd Row
					{
						if (!evenColumn)
						{
							cellColor = Color.White;
						}
						else
						{
							cellColor = Color.LightBlue;
						}
					}

					var cellPositionX = _cellWidth * x;
					var cellPositionY = _cellHeight * i;

					var anCol = 65 + x;
					var algebraicNotation = (char)anCol + "" + anRow;

					_sprites.Add(new Cell(cellTexture)
					{
						Position = new Vector2(cellPositionX, cellPositionY),
						Color = cellColor,
						Location = new Location()
						{
							Row = i,
							Column = x,
							AlgebraicNotation = algebraicNotation
						}
					}
					);
				}
			}
		}
	}
}
