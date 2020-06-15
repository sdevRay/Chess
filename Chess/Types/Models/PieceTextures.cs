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
		public Texture2D KnightWhiteTexture => _content.Load<Texture2D>("Pieces/Knight_White");
		public Texture2D KnightBlackTexture => _content.Load<Texture2D>("Pieces/Knight_Black");
		public Texture2D RookWhiteTexture => _content.Load<Texture2D>("Pieces/Rook_White");
		public Texture2D RookBlackTexture => _content.Load<Texture2D>("Pieces/Rook_Black");
		public Texture2D BishopWhiteTexture => _content.Load<Texture2D>("Pieces/Bishop_White");
		public Texture2D BishopBlackTexture => _content.Load<Texture2D>("Pieces/Bishop_Black");
		public Texture2D PawnWhiteTexture => _content.Load<Texture2D>("Pieces/Pawn_White");
		public Texture2D PawnBlackTexture => _content.Load<Texture2D>("Pieces/Pawn_Black");
		public Texture2D QueenWhiteTexture => _content.Load<Texture2D>("Pieces/Queen_White");
		public Texture2D QueenBlackTexture => _content.Load<Texture2D>("Pieces/Queen_Black");
		public Texture2D KingWhiteTexture => _content.Load<Texture2D>("Pieces/King_White");
		public Texture2D KingBlackTexture => _content.Load<Texture2D>("Pieces/King_Black");
	}
}
