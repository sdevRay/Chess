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

		private readonly int _consoleSize = 60;
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
			_player = new Player(PieceColor.White); // Starting player color

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_font = Content.Load<SpriteFont>("Font");
			_player.GameStart = true;

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

			if (Keyboard.GetState().IsKeyDown(Keys.R))
			{
				_pieces.Clear();
				_player.CurrentPlayerColor = PieceColor.White;
				AddPiecesToChessBoard();

				if (!_player.GameStart)
					_player.GameStart = true;
			}

			if (_player.GameStart)
			{
				var currentPieceSelected = _pieces.FirstOrDefault(res => res.IsSelected);
				if (currentPieceSelected != null)
					currentPieceSelected.Update(gameTime, _pieces, _chessBoard, _player);
				else
					_pieces.ForEach(res => res.Update(gameTime, _pieces, _chessBoard, _player));
			}

			var removedKing = _pieces.Where(res => res.PieceType.Equals(PieceType.King) && res.IsRemoved == true);
			if(removedKing.Count() > 0)
				_player.GameStart = false;
			
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

			if (!_player.GameStart)
			{
				spriteBatch.DrawString(_font, "Game Over \nPress 'R' to restart\nPress 'ESC' to exit", new Vector2(400, 805), Color.White);
			}

			GetKingStatus();					
			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void AddPiecesToChessBoard()
		{
			_pieces = chessBoard.GetPieces(_chessBoard, new LocationCheckerService(), new PieceTextures(Content));

			foreach(var piece in _pieces)
				piece.AvailableLocations = piece.GetAvailableLocations(piece.Location, _pieces.Where(res => res != piece).ToList(), piece.PieceColor);
		}

		private void GetKingStatus()
		{
			var sb = new StringBuilder();

			var kings = _pieces.OfType<King>();

			foreach(var king in kings)
			{
				sb.Append($"{king.PieceColor} {king.PieceType} Check => {king.CheckStatus}\n");
			}

			sb.Append($"Current Player: {_player.CurrentPlayerColor}");
			spriteBatch.DrawString(_font, sb.ToString(), new Vector2(5, 805), Color.White);
		}
	}
}
