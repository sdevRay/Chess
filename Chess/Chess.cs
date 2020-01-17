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
			var chessBoard = new ChessBoard(cellTexture);
			_chessBoard = chessBoard.GetChessBoard();

			var locationCheckerService = new LocationCheckerService();

			InitializeTextures(out Texture2D knightWhiteTexture, out Texture2D knightBlackTexture, out Texture2D rookWhiteTexture, out Texture2D rookBlackTexture, out Texture2D bishopWhiteTexture, out Texture2D bishopBlackTexture, out Texture2D pawnWhiteTexture, out Texture2D pawnBlackTexture, out Texture2D queenWhiteTexture, out Texture2D queenBlackTexture, out Texture2D kingWhiteTexture, out Texture2D kingBlackTexture);

			SetPieceLocations(chessBoard, locationCheckerService, knightWhiteTexture, knightBlackTexture, rookWhiteTexture, rookBlackTexture, bishopWhiteTexture, bishopBlackTexture, pawnWhiteTexture, pawnBlackTexture, queenWhiteTexture, queenBlackTexture, kingWhiteTexture, kingBlackTexture);
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

			if (anyPiecesSelected != null)
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

		private void InitializeTextures(out Texture2D knightWhiteTexture, out Texture2D knightBlackTexture, out Texture2D rookWhiteTexture, out Texture2D rookBlackTexture, out Texture2D bishopWhiteTexture, out Texture2D bishopBlackTexture, out Texture2D pawnWhiteTexture, out Texture2D pawnBlackTexture, out Texture2D queenWhiteTexture, out Texture2D queenBlackTexture, out Texture2D kingWhiteTexture, out Texture2D kingBlackTexture)
		{
			knightWhiteTexture = Content.Load<Texture2D>("Pieces/Knight_White");
			knightBlackTexture = Content.Load<Texture2D>("Pieces/Knight_Black");
			rookWhiteTexture = Content.Load<Texture2D>("Pieces/Rook_White");
			rookBlackTexture = Content.Load<Texture2D>("Pieces/Rook_Black");
			bishopWhiteTexture = Content.Load<Texture2D>("Pieces/Bishop_White");
			bishopBlackTexture = Content.Load<Texture2D>("Pieces/Bishop_Black");
			pawnWhiteTexture = Content.Load<Texture2D>("Pieces/Pawn_White");
			pawnBlackTexture = Content.Load<Texture2D>("Pieces/Pawn_Black");
			queenWhiteTexture = Content.Load<Texture2D>("Pieces/Queen_White");
			queenBlackTexture = Content.Load<Texture2D>("Pieces/Queen_Black");
			kingWhiteTexture = Content.Load<Texture2D>("Pieces/King_White");
			kingBlackTexture = Content.Load<Texture2D>("Pieces/King_Black");
		}

		private void SetPieceLocations(ChessBoard chessBoard, LocationCheckerService locationCheckerService, Texture2D knightWhiteTexture, Texture2D knightBlackTexture, Texture2D rookWhiteTexture, Texture2D rookBlackTexture, Texture2D bishopWhiteTexture, Texture2D bishopBlackTexture, Texture2D pawnWhiteTexture, Texture2D pawnBlackTexture, Texture2D queenWhiteTexture, Texture2D queenBlackTexture, Texture2D kingWhiteTexture, Texture2D kingBlackTexture)
		{
			foreach (var cell in _chessBoard)
			{
				// PAWNS
				if (chessBoard.GetPawnWhiteLocations().Contains(cell.Location))
				{
					_pieces.Add(
						new Pawn(pawnWhiteTexture, locationCheckerService, cell.Location)
						{
							Position = cell.SetPieceOrigin(pawnWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Pawn,
						}
					);
				}

				if (chessBoard.GetPawnBlackLocations().Contains(cell.Location))
				{
					_pieces.Add(
						new Pawn(pawnBlackTexture, locationCheckerService, cell.Location)
						{
							Position = cell.SetPieceOrigin(pawnBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Pawn,
						}
					);
				}

				// KINGS
				if (cell.Location.Equals(new Point(4, 0)))
				{
					_pieces.Add(
						new King(kingBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(kingBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.King,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(4, 7)))
				{
					_pieces.Add(
						new King(kingWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(kingWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.King,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// QUEENS
				if (cell.Location.Equals(new Point(3, 0)))
				{
					_pieces.Add(
						new Queen(queenBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(queenBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Queen,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(3, 7)))
				{
					_pieces.Add(
						new Queen(queenWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(queenWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Queen,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// BLACK ROOKS
				if (cell.Location.Equals(new Point(0, 0)))
				{
					_pieces.Add(
						new Rook(rookBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(7, 0)))
				{
					_pieces.Add(
						new Rook(rookBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// WHITE ROOKS
				if (cell.Location.Equals(new Point(0, 7)))
				{
					_pieces.Add(
						new Rook(rookWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(7, 7)))
				{
					_pieces.Add(
						new Rook(rookWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// BLACK BISHOPS
				if (cell.Location.Equals(new Point(2, 0)))
				{
					_pieces.Add(
						new Bishop(bishopBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(5, 0)))
				{
					_pieces.Add(
						new Bishop(bishopBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// WHITE BISHOPS
				if (cell.Location.Equals(new Point(2, 7)))
				{
					_pieces.Add(
						new Bishop(bishopWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(5, 7)))
				{
					_pieces.Add(
						new Bishop(bishopWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// BLACK KNIGHTS
				if (cell.Location.Equals(new Point(1, 0)))
				{
					_pieces.Add(
						new Knight(knightBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(6, 0)))
				{
					_pieces.Add(
						new Knight(knightBlackTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightBlackTexture),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// WHITE KNIGHTS
				if (cell.Location.Equals(new Point(1, 7)))
				{
					_pieces.Add(
						new Knight(knightWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(6, 7)))
				{
					_pieces.Add(
						new Knight(knightWhiteTexture, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightWhiteTexture),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}
			}
		}
	}
}
