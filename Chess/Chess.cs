using Chess.LocationChecker;
using Chess.Sprites;
using Chess.Sprites.Cells;
using Chess.Sprites.Pieces;
using Chess.Types;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
	public class Chess : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Player _player;
		private List<Piece> _pieces;
		private List<Cell> _chessBoard;

		private ChessBoard chessBoard;
		private SpriteFont _font;

		private bool _debug = false;
		private bool _gameStart = false;

		private readonly int _consoleSize = 55;
		public Chess()
		{
			IsMouseVisible = true;

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = Global.SCREEN_WIDTH;
			graphics.PreferredBackBufferHeight = Global.SCREEN_HEIGHT + _consoleSize;
			graphics.ApplyChanges();

		}
		protected override void Initialize()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			_pieces = new List<Piece>();
			_player = new Player(PieceColor.White);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_font = Content.Load<SpriteFont>("Font");			
			_gameStart = true;

			var cellTexture = Content.Load<Texture2D>("Cell");
			chessBoard = new ChessBoard(cellTexture);
			
			_chessBoard = chessBoard.GetChessBoard();
			AddPiecesToChessBoard();
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.D))
				_debug = true;
			else
				_debug = false;

			if (Keyboard.GetState().IsKeyDown(Keys.R) && !_gameStart)
			{
				_pieces.Clear();
				AddPiecesToChessBoard();
				_gameStart = true;
			}

			if (_gameStart)
			{
				var currentPieceSelected = _pieces.FirstOrDefault(res => res.IsSelected);
				if (currentPieceSelected != null)
					currentPieceSelected.Update(gameTime, _pieces, _chessBoard, _player);
				else
					_pieces.ForEach(res => res.Update(gameTime, _pieces, _chessBoard, _player));
			}

			var removedKing = _pieces.Where(res => res.PieceType.Equals(PieceType.King) && res.IsRemoved == true);
			if(removedKing.Count() > 0)
				_gameStart = false;
			
			_pieces.RemoveAll(res => res.IsRemoved);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();
			_chessBoard.ForEach(res => res.Draw(spriteBatch));
			_pieces.ForEach(res => res.Draw(spriteBatch));

			if (_debug)
				chessBoard.debug.ForEach(res => spriteBatch.DrawString(_font, res.Label, res.Position, Color.Black));

			if (!_gameStart)
			{
				spriteBatch.DrawString(_font, "Game Over \nPress 'R' to restart\nPress 'ESC' to exit.", new Vector2(400, 800), Color.White);
			}

			GetPlayerStatus();					
			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void AddPiecesToChessBoard()
		{
			_pieces = chessBoard.GetPieces(_chessBoard, new LocationCheckerService(), new PieceTextures(Content));
		}

		private void GetPlayerStatus()
		{
			var sb = new StringBuilder();

			var kings = _pieces.Where(res => res.PieceType.Equals(PieceType.King));
			var whiteKing = kings.FirstOrDefault(res => res.PieceColor.Equals(PieceColor.White)) as King;
			var blackKing = kings.FirstOrDefault(res => res.PieceColor.Equals(PieceColor.Black)) as King;

			sb.Append($"White King:");
			if (whiteKing != null)
				 sb.Append("Alive");
			else
				 sb.Append("Defeated");

			sb.Append($"\nBlack King:");
			if (blackKing != null)
				sb.Append("Alive");
			else
				sb.Append("Defeated");

			spriteBatch.DrawString(_font, sb.ToString(), new Vector2(5, 800), Color.White);
			spriteBatch.DrawString(_font, $"Current Player: {_player.CurrentPlayerColor}", new Vector2(5, 835), Color.White);
		}
	}
}
