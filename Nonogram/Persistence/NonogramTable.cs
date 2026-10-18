using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Persistence
{
    public class NonogramTable
    {
        #region private fields
        private int _size;
        //játékos kép
        private CellState[,] _playerGrid;
        //jó kép
        private bool[,] _solution;

        private int _nonogramTime;
        private int _lives;

        #endregion

        #region public properties

        public int Size { get { return _size; } }

        public int NonogramTime
        {
            get { return _nonogramTime; }
            set { _nonogramTime = value; }
        }

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        #endregion

        #region constructor
        public NonogramTable(int size)
        {
            if(size < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Size cannot be negative.");
            }
            _size = size;
            _playerGrid = new CellState[size, size];
            _solution = new bool[size, size];
            _nonogramTime = 0;
            _lives = 3;
        }

        #endregion

        #region public methods

        //mező állapot ellenőrzés
        public CellState GetPlayerCell(int x, int y)
        {
            return _playerGrid[x, y];
        }

        //ténylegesen fekete e a mező xddd
        public bool IsBlackInSolution(int x, int y)
        {
            return _solution[x, y];
        }

        //mező állapot változtatás
        public void SetPlayerCell(int x, int y, CellState state)
        {
            _playerGrid[x, y] = state;
        }

        public void SetSolutionCell(int x, int y, bool isBlack)
        {
            _solution[x, y] = isBlack;
        }

        #endregion

        #region private methods

        #endregion
    }
}
