using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Model
{
    public class NonogramEventArgs : EventArgs
    {
        #region private fields
        private int _gameTime;
        private int _lives;
        private bool _isGameOver;
        private bool _isWon;

        #endregion

        #region public properties

        public int GameTime { get { return _gameTime; } }
        public int Lives { get { return _lives; } }
        public bool IsGameOver { get { return _isGameOver; } }
        public bool IsWon { get { return _isWon; } }

        #endregion

        #region constructor
        public NonogramEventArgs(int gameTime, int lives, bool isGameOver, bool isWon)
        {
            _gameTime = gameTime;
            _lives = lives;
            _isGameOver = isGameOver;
            _isWon = isWon;
        }

        #endregion
    }
}
