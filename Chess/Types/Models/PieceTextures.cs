using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chess.Types.Models
{
	public class PieceTextures
	{
		private ContentManager _content;
		public PieceTextures(ContentManager content)
		{
			_content = content;
		}
		public Texture2D knightWhiteTexture => _content.Load<Texture2D>("Pieces/Knight_White");
		public Texture2D knightBlackTexture => _content.Load<Texture2D>("Pieces/Knight_Black");
		public Texture2D rookWhiteTexture => _content.Load<Texture2D>("Pieces/Rook_White");
		public Texture2D rookBlackTexture => _content.Load<Texture2D>("Pieces/Rook_Black");
		public Texture2D bishopWhiteTexture => _content.Load<Texture2D>("Pieces/Bishop_White");
		public Texture2D bishopBlackTexture => _content.Load<Texture2D>("Pieces/Bishop_Black");
		public Texture2D pawnWhiteTexture => _content.Load<Texture2D>("Pieces/Pawn_White");
		public Texture2D pawnBlackTexture => _content.Load<Texture2D>("Pieces/Pawn_Black");
		public Texture2D queenWhiteTexture => _content.Load<Texture2D>("Pieces/Queen_White");
		public Texture2D queenBlackTexture => _content.Load<Texture2D>("Pieces/Queen_Black");
		public Texture2D kingWhiteTexture => _content.Load<Texture2D>("Pieces/King_White");
		public Texture2D kingBlackTexture => _content.Load<Texture2D>("Pieces/King_Black");
	}
}
