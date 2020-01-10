using Chess.Sprites;
using Chess.Sprites.Pieces;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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

			var rookTexture = Content.Load<Texture2D>("Rook");
			var bishopTexture = Content.Load<Texture2D>("Bishop");
			var pawnTexture = Content.Load<Texture2D>("Pawn");

			foreach(var cell in _chessBoard)
			{
				if (cell.Location.Equals(new Point(0, 0)))
				{
					var piece = new Pawn(pawnTexture)
					{
						Position = cell.CellOrigin(pawnTexture),
						PieceColor = PieceColor.White,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(6, 3)))
				{
					var piece = new Rook(rookTexture)
					{
						Position = cell.CellOrigin(rookTexture),
						PieceColor = PieceColor.White,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(3, 5)))
				{
					var piece = new Rook(rookTexture)
					{
						Position = cell.CellOrigin(rookTexture),
						Color = Color.Black,
						PieceColor = PieceColor.Black,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(5, 5)))
				{
					var piece = new Bishop(bishopTexture)
					{
						Position = cell.CellOrigin(bishopTexture),
						PieceColor = PieceColor.White,
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

			var anyPiecesSelected = _pieces.FirstOrDefault(res => res.IsSelected);
			
			if(anyPiecesSelected != null)
				anyPiecesSelected.Update(gameTime, _pieces, _chessBoard);
			else
				_pieces.ForEach(res => res.Update(gameTime, _pieces, _chessBoard));

			_pieces.RemoveAll(res => res.IsRemoved);

			base.Update(gameTime);
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
