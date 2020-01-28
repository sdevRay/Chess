using Chess.Sprites;
using Chess.Types.Enumerations;

namespace Chess.Types.Models
{
	public class Player
	{
		public PieceColor CurrentPlayerColor;
		public bool BlackChecked;
		public bool WhiteChecked;
		public bool IsChecked;

		public Player(PieceColor currentPlayerColor)
		{
			CurrentPlayerColor = currentPlayerColor;
		}

		public void SwitchPlayerColor()
		{
			if (CurrentPlayerColor.Equals(PieceColor.White))
				CurrentPlayerColor = PieceColor.Black;
			else
				CurrentPlayerColor = PieceColor.White;
		}		
	}
}
