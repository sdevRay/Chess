using Chess.Types.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chess.Sprites.Cells
{
	public class CellGrid
	{
		private readonly int _cellCount = 8;
		private Texture2D _cellTexture;
		public CellGrid(Texture2D cellTexture)
		{
			_cellTexture = cellTexture;
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
