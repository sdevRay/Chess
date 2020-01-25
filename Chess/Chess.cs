using Chess.Sprites;
using Chess.Sprites.Cells;
using Chess.Types.Constants;
using Chess.Types.Enumerations;
using Chess.Types.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class Chess : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Player _player;
		private List<Piece> _pieces;
		private List<Cell> _chessBoard;

		public Chess()
		{
			IsMouseVisible = true;

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = Global.SCREEN_WIDTH;
			graphics.PreferredBackBufferHeight = Global.SCREEN_HEIGHT;
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
			var cellTexture = Content.Load<Texture2D>("Cell");
			var chessBoard = new ChessBoard(cellTexture);
			
			_chessBoard = chessBoard.GetChessBoard();
			_pieces = chessBoard.GetPieces(_chessBoard, new LocationCheckerService(), new PieceTextures(Content));
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			var currentPieceSelected = _pieces.FirstOrDefault(res => res.IsSelected);

			if (currentPieceSelected != null)
				currentPieceSelected.Update(gameTime, _pieces, _chessBoard, _player);
			else
				_pieces.ForEach(res => res.Update(gameTime, _pieces, _chessBoard, _player));

			_pieces.RemoveAll(res => res.IsRemoved);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			_chessBoard.ForEach(res => res.Draw(spriteBatch));
			_pieces.ForEach(res => res.Draw(spriteBatch));
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
