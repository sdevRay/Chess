using Chess.Sprites;
using Chess.Types.Constants;
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

		//private MouseState oldState;

		private List<Piece> _pieces;
		private List<Cell> _chessBoard;

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
			spriteBatch = new SpriteBatch(GraphicsDevice);
			_pieces = new List<Piece>();

			base.Initialize();
		}
		protected override void LoadContent()
		{

			var cellTexture = Content.Load<Texture2D>("Cell");
			_chessBoard = new CellGrid(cellTexture).ChessBoard;

			var pawnTexture = Content.Load<Texture2D>("Pawn");

			foreach(var cell in _chessBoard)
			{
				if (cell.Location.Equals(new Point(0, 0)))
				{
					var piece = new Rook(pawnTexture)
					{
						Position = cell.CellOrigin(pawnTexture),
						Color = Color.Black,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(5, 5)))
				{
					var piece = new Rook(pawnTexture)
					{
						Position = cell.CellOrigin(pawnTexture),
						Color = Color.Black,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
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

			_pieces.ForEach(res => res.Update(gameTime, _pieces, _chessBoard));

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
			for (var i = 0; i < _pieces.Count; i++)
			{
				var sprite = _pieces[i];
				if (sprite is Piece)
				{
					if ((sprite as Piece).IsRemoved)
					{
						_pieces.RemoveAt(i);
						i--;
					}
				}
			}
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			_chessBoard.ForEach(res => res.Draw(spriteBatch));
			_pieces.ForEach(res => res.Draw(spriteBatch));
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
