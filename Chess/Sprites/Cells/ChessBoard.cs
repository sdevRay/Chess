using Chess.LocationChecker;
using Chess.Sprites.Pieces;
using Chess.Types;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Cells
{
	public class ChessBoard
	{
		private readonly int _cellCount = 8;
		private Texture2D _cellTexture;
		public List<Debug> debug;
		public ChessBoard(Texture2D cellTexture)
		{
			_cellTexture = cellTexture;
		}
		private List<Point> GetPawnWhiteLocations()
		{
			var pawnWhiteLocations = new List<Point>();
			for (var x = 0; x <= Global.MAX_CELL_BOUNDARY; x++)
				pawnWhiteLocations.Add(new Point(x, 6));

			return pawnWhiteLocations;
		}
		private List<Point> GetPawnBlackLocations()
		{
			var pawnBlackLocations = new List<Point>();
			for (var x = 0; x <= Global.MAX_CELL_BOUNDARY; x++)
				pawnBlackLocations.Add(new Point(x, 1));
			
			return pawnBlackLocations;
		}
		public List<Cell> GetChessBoard()
		{
			debug = new List<Debug>();
			var chessBoard = new List<Cell>();
			var _cellWidth = Global.SCREEN_WIDTH / _cellCount;
			var _cellHeight = Global.SCREEN_HEIGHT / _cellCount;

			for (var x = Global.MIN_CELL_BOUNDARY; x < _cellCount; x++)
			{
				for (var y = Global.MIN_CELL_BOUNDARY; y < _cellCount; y++)
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

					debug.Add(new Debug
					{
						Position = new Vector2(_cellWidth * x, _cellHeight * y),
						Label = $"X:{x} Y:{y}"
					});

					chessBoard.Add(new Cell(_cellTexture)
					{
						Position = new Vector2(_cellWidth * x, _cellHeight * y),
						DefaultColor = cellColor,
						Location = new Point(x, y)
					});
				}
			}

			return chessBoard;
		}

		public List<Piece> GetPieces(List<Cell> chessBoard, LocationCheckerService locationCheckerService, PieceTextures pieceTextures)
		{
			var pieces = new List<Piece>();

			foreach (var cell in chessBoard)
			{
				// PAWNS
				var pawnW = pieceTextures.PawnWhiteTexture;
				if (GetPawnWhiteLocations().Contains(cell.Location))
				{
					pieces.Add(
						new Pawn(pawnW, locationCheckerService, cell.Location)
						{
							Position = cell.SetPieceOrigin(pawnW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Pawn,
						}
					);
				}

				var pawnB = pieceTextures.PawnBlackTexture;
				if (GetPawnBlackLocations().Contains(cell.Location))
				{
					pieces.Add(
						new Pawn(pawnB, locationCheckerService, cell.Location)
						{
							Position = cell.SetPieceOrigin(pawnB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Pawn,
						}
					);
				}

				// KINGS
				var kingB = pieceTextures.KingBlackTexture;
				if (cell.Location.Equals(new Point(4, 0)))
				{
					pieces.Add(
						new King(kingB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(kingB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.King,
							Location = new Point(cell.Location.X, cell.Location.Y)			
						}
					);
				}

				var kingW = pieceTextures.KingWhiteTexture;
				if (cell.Location.Equals(new Point(4, 7)))
				{
					pieces.Add(
						new King(kingW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(kingW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.King,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// QUEENS
				var queenB = pieceTextures.QueenBlackTexture;
				if (cell.Location.Equals(new Point(3, 0)))
				{
					pieces.Add(
						new Queen(queenB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(queenB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Queen,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				var queenW = pieceTextures.QueenWhiteTexture;
				if (cell.Location.Equals(new Point(3, 7)))
				{
					pieces.Add(
						new Queen(queenW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(queenW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Queen,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// BLACK ROOKS
				var rookB = pieceTextures.RookBlackTexture;
				if (cell.Location.Equals(new Point(0, 0)))
				{
					pieces.Add(
						new Rook(rookB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(7, 0)))
				{
					pieces.Add(
						new Rook(rookB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// WHITE ROOKS
				var rookW = pieceTextures.RookWhiteTexture;
				if (cell.Location.Equals(new Point(0, 7)))
				{
					pieces.Add(
						new Rook(rookW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(7, 7)))
				{
					pieces.Add(
						new Rook(rookW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(rookW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Rook,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// BLACK BISHOPS
				var bishopB = pieceTextures.BishopBlackTexture;
				if (cell.Location.Equals(new Point(2, 0)))
				{
					pieces.Add(
						new Bishop(bishopB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(5, 0)))
				{
					pieces.Add(
						new Bishop(bishopB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// WHITE BISHOPS
				var bishopW = pieceTextures.BishopWhiteTexture;
				if (cell.Location.Equals(new Point(2, 7)))
				{
					pieces.Add(
						new Bishop(bishopW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(5, 7)))
				{
					pieces.Add(
						new Bishop(bishopW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(bishopW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Bishop,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// BLACK KNIGHTS
				var knightB = pieceTextures.KnightBlackTexture;
				if (cell.Location.Equals(new Point(1, 0)))
				{
					pieces.Add(
						new Knight(knightB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(6, 0)))
				{
					pieces.Add(
						new Knight(knightB, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightB),
							PieceColor = PieceColor.Black,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				// WHITE KNIGHTS
				var knightW = pieceTextures.KnightWhiteTexture;
				if (cell.Location.Equals(new Point(1, 7)))
				{
					pieces.Add(
						new Knight(knightW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}

				if (cell.Location.Equals(new Point(6, 7)))
				{
					pieces.Add(
						new Knight(knightW, locationCheckerService)
						{
							Position = cell.SetPieceOrigin(knightW),
							PieceColor = PieceColor.White,
							PieceType = PieceType.Knight,
							Location = new Point(cell.Location.X, cell.Location.Y)
						}
					);
				}
			}

			return pieces;
		}
	}
}
