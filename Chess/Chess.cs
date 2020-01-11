using Chess.Sprites;
using Chess.Sprites.Cells;
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
			_chessBoard = new CellGrid(cellTexture).GetChessBoard();

			var knightTexture = Content.Load<Texture2D>("Knight");
			var rookTexture = Content.Load<Texture2D>("Rook");
			var bishopWhiteTexture = Content.Load<Texture2D>("Bishop_White");
			var bishopBlackTexture = Content.Load<Texture2D>("Bishop_Black");
			var pawnTexture = Content.Load<Texture2D>("Pawn");
			var queenTexture = Content.Load<Texture2D>("Queen");

			foreach (var cell in _chessBoard)
			{
				if (cell.Location.Equals(new Point(2, 6)))
				{
					var piece = new Queen(queenTexture)
					{
						Position = cell.CellOrigin(queenTexture),
						PieceColor = PieceColor.White,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(2, 1)))
				{
					var piece = new Pawn(pawnTexture)
					{
						Position = cell.CellOrigin(pawnTexture),
						PieceColor = PieceColor.White,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(4, 4)))
				{
					var piece = new Knight(knightTexture)
					{
						Position = cell.CellOrigin(knightTexture),
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
					var piece = new Bishop(bishopBlackTexture)
					{
						Position = cell.CellOrigin(bishopBlackTexture),
						PieceColor = PieceColor.Black,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(5, 5)))
				{
					var piece = new Bishop(bishopWhiteTexture)
					{
						Position = cell.CellOrigin(bishopWhiteTexture),
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
			{
				anyPiecesSelected.OtherPieces = _pieces.Where(res => !res.IsSelected).ToList();
				anyPiecesSelected.Update(gameTime, _pieces, _chessBoard);
			}
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
