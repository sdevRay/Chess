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
		public Texture2D KnightWhiteTexture => _content.Load<Texture2D>("Pieces/White_Knight");
		public Texture2D KnightBlackTexture => _content.Load<Texture2D>("Pieces/Black_Knight");
		public Texture2D RookWhiteTexture => _content.Load<Texture2D>("Pieces/White_Rook");
		public Texture2D RookBlackTexture => _content.Load<Texture2D>("Pieces/Black_Rook");
		public Texture2D BishopWhiteTexture => _content.Load<Texture2D>("Pieces/White_Bishop");
		public Texture2D BishopBlackTexture => _content.Load<Texture2D>("Pieces/Black_Bishop");
		public Texture2D PawnWhiteTexture => _content.Load<Texture2D>("Pieces/White_Pawn");
		public Texture2D PawnBlackTexture => _content.Load<Texture2D>("Pieces/Black_Pawn");
		public Texture2D QueenWhiteTexture => _content.Load<Texture2D>("Pieces/White_Queen");
		public Texture2D QueenBlackTexture => _content.Load<Texture2D>("Pieces/Black_Queen");
		public Texture2D KingWhiteTexture => _content.Load<Texture2D>("Pieces/White_King");
		public Texture2D KingBlackTexture => _content.Load<Texture2D>("Pieces/Black_King");
	}
}
