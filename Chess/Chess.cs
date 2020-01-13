using Chess.Sprites;
using Chess.Sprites.Cells;
using Chess.Sprites.Pieces;
using Chess.Types;
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

			var locationCheckerService = new LocationCheckerService();

			var knightWhiteTexture = Content.Load<Texture2D>("Pieces/Knight_White");
			var knightBlackTexture = Content.Load<Texture2D>("Pieces/Knight_Black");

			var rookWhiteTexture = Content.Load<Texture2D>("Pieces/Rook_White");
			var rookBlackTexture = Content.Load<Texture2D>("Pieces/Rook_Black");

			var bishopWhiteTexture = Content.Load<Texture2D>("Pieces/Bishop_White");
			var bishopBlackTexture = Content.Load<Texture2D>("Pieces/Bishop_Black");

			var pawnWhiteTexture = Content.Load<Texture2D>("Pieces/Pawn_White");
			var pawnBlackTexture = Content.Load<Texture2D>("Pieces/Pawn_Black");

			var queenWhiteTexture = Content.Load<Texture2D>("Pieces/Queen_White");
			var queenBlackTexture = Content.Load<Texture2D>("Pieces/Queen_Black");

			var kingWhiteTexture = Content.Load<Texture2D>("Pieces/King_White");
			var kingBlackTexture = Content.Load<Texture2D>("Pieces/King_Black");

			foreach (var cell in _chessBoard)
			{
				if (cell.Location.Equals(new Point(7, 3)))
				{
					var piece = new King(kingBlackTexture, locationCheckerService)
					{
						Position = cell.CellOrigin(kingBlackTexture),
						PieceColor = PieceColor.Black,
						PieceType = PieceType.King,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(2, 6)))
				{
					var piece = new Queen(queenWhiteTexture, locationCheckerService)
					{
						Position = cell.CellOrigin(queenWhiteTexture),
						PieceColor = PieceColor.White,
						PieceType = PieceType.Queen,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(3, 1)))
				{
					var location = new Point(cell.Location.X, cell.Location.Y);
					var piece = new Pawn(pawnWhiteTexture, locationCheckerService, location)
					{
						Position = cell.CellOrigin(pawnBlackTexture),
						PieceColor = PieceColor.White,
						PieceType = PieceType.Pawn
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(6,6)))
				{
					var location = new Point(cell.Location.X, cell.Location.Y);
					var piece = new Pawn(pawnBlackTexture, locationCheckerService, location)
					{
						Position = cell.CellOrigin(pawnBlackTexture),
						PieceColor = PieceColor.Black,
						PieceType = PieceType.Pawn
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(4, 4)))
				{
					var piece = new Knight(knightWhiteTexture, locationCheckerService)
					{
						Position = cell.CellOrigin(knightWhiteTexture),
						PieceColor = PieceColor.White,
						PieceType = PieceType.Knight,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(6, 3)))
				{
					var piece = new Rook(rookWhiteTexture, locationCheckerService)
					{
						Position = cell.CellOrigin(rookWhiteTexture),
						PieceColor = PieceColor.White,
						PieceType = PieceType.Rook,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(0, 0)))
				{
					var piece = new Bishop(bishopBlackTexture, locationCheckerService)
					{
						Position = cell.CellOrigin(bishopBlackTexture),
						PieceColor = PieceColor.Black,
						PieceType = PieceType.Bishop,
						Location = new Point(cell.Location.X, cell.Location.Y)
					};

					_pieces.Add(piece);
				}

				if (cell.Location.Equals(new Point(1, 1)))
				{
					var piece = new Bishop(bishopWhiteTexture, locationCheckerService)
					{
						Position = cell.CellOrigin(bishopWhiteTexture),
						PieceColor = PieceColor.White,
						PieceType = PieceType.Bishop,
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
