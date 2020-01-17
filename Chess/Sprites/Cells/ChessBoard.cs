using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Cells
{
	public class ChessBoard
	{
		private readonly int _cellCount = 8;
		private Texture2D _cellTexture;
		public ChessBoard(Texture2D cellTexture)
		{
			_cellTexture = cellTexture;
		}
		public List<Point> GetPawnWhiteLocations()
		{
			var pawnWhiteLocations = new List<Point>();
			for (var x = 0; x <= Global.MAX_CELL_BOUNDARY; x++)
				pawnWhiteLocations.Add(new Point(x, 6));

			return pawnWhiteLocations;
		}
		public List<Point> GetPawnBlackLocations()
		{
			var pawnBlackLocations = new List<Point>();
			for (var x = 0; x <= Global.MAX_CELL_BOUNDARY; x++)
				pawnBlackLocations.Add(new Point(x, 1));
			
			return pawnBlackLocations;
		}
		public List<Cell> GetChessBoard()
		{
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
	}
}
