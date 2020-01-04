using Chess.Models;
using Chess.Sprites;
using Chess.Types;
using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess
{
	public class Chess : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		//private MouseState oldState;

		private List<Sprite> _sprites;

		private readonly int _cellCount = 8;
		private int _cellWidth;
		private int _cellHeight;


		public Chess()
		{
			IsMouseVisible = true;

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = Global.SCREEN_WIDTH;
			graphics.PreferredBackBufferHeight = Global.SCREEN_HEIGHT;
			graphics.ApplyChanges();

		}
		protected override void Initialize()
		{
			_cellWidth = Global.SCREEN_WIDTH / _cellCount;
			_cellHeight = Global.SCREEN_HEIGHT / _cellCount;

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

					if (cell.Location.Grid.Y == 1)
					{
						if(cell.Location.Grid.X == 0)
						{
							var piece = new Pawn(pawnTexture)
							{
								Position = cell.CellOrigin(pawnTexture),
								Color = Color.Black, 
								Location = new Location()
								{
									Grid = new Point(cell.Location.Grid.X, cell.Location.Grid.Y),
									AlgebraicNotation = cell.Location.AlgebraicNotation
								}
							};

							_sprites.Add(piece);
						}
		
					}

					if(cell.Location.Grid.Y == 6)
					{
						if (cell.Location.Grid.X == 1)
						{
							var piece = new Pawn(pawnTexture)
							{
								Position = cell.CellOrigin(pawnTexture),
								Color = Color.White,
								
								Location = new Location()
								{
									Grid = new Point(cell.Location.Grid.X, cell.Location.Grid.Y),
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

			_sprites.ForEach(res => res.Update(gameTime, _sprites));

			//await Task.Run(() =>
			//{
			//	Parallel.ForEach(_sprites, (sprite) =>
			//	{
			//		sprite.Update(gameTime, _sprites);
			//	});
			//});

			//PostProcess();

			//_sprites.RemoveAll(res =>
			//{
			//	if (res is Piece)
			//		if((res as Piece).IsRemoved)
			//			return true;
				
			//	return false;
			//});

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
			_sprites.ForEach(res => res.Draw(spriteBatch));
			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void BuildChessBoard(Texture2D cellTexture)
		{
			var board = new Cell[_cellCount, _cellCount];

			for (var x = 0; x < board.GetLength(0); x++)
			{
				var anRow = _cellCount - x;

				for (var y = 0; y < board.GetLength(1); y++)
				{
					Color cellColor;
					var evenRow = (x % 2 == 0);
					var evenColumn = (y % 2 == 0);

					if (evenRow)
					{
						if (evenColumn)
							cellColor = Color.White;
						else
							cellColor = Color.LightBlue;
					}
					else // Odd Row
					{
						if (!evenColumn)
							cellColor = Color.White;
						else
							cellColor = Color.LightBlue;
					}

					var cellPositionX = _cellWidth * x;
					var cellPositionY = _cellHeight * y;

					var anCol = 65 + x;
					var algebraicNotation = (char)anCol + "" + anRow;

					_sprites.Add(new Cell(cellTexture)
					{
						Position = new Vector2(cellPositionX, cellPositionY),
						DefaultColor = cellColor,
						Location = new Location()
						{
							Grid = new Point(x, y),
							AlgebraicNotation = algebraicNotation
						}
					}
					);
				}
			}
		}
	}
}
