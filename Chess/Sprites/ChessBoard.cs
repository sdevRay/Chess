using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites
{
	public class CellGrid
	{
		private Cell[,] _cellGrid;
		private readonly int _cellCount = 8;
		private int _cellWidth;
		private int _cellHeight;

		public List<Cell> ChessBoard = new List<Cell>();

		public CellGrid(Texture2D cellTexture)
		{
			_cellWidth = Global.SCREEN_WIDTH / _cellCount;
			_cellHeight = Global.SCREEN_HEIGHT / _cellCount;
			_cellGrid = new Cell[_cellCount, _cellCount];

			BuildCellGrid(cellTexture);
		}

		public void BuildCellGrid(Texture2D cellTexture)
		{
			for (var x = Global.MIN_CELL_BOUNDARY; x < _cellGrid.GetLength(0); x++)
			{
				for (var y = Global.MIN_CELL_BOUNDARY; y < _cellGrid.GetLength(1); y++)
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

					ChessBoard.Add(new Cell(cellTexture)
					{
						Position = new Vector2(cellPositionX, cellPositionY),
						DefaultColor = cellColor,
						Location = new Point(x, y)
					});
				}
			}
		}
	}
}
