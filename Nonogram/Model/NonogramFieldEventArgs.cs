using Nonogram.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Model
{
    public class NonogramFieldEventArgs : EventArgs
    {
        #region private fields

        private int _x;
        private int _y;
        private CellState _state;

        #endregion

        #region public properties

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public CellState State { get { return _state; } }

        #endregion

        #region constructors

        public NonogramFieldEventArgs(int x, int y, CellState state)
        {
            _x = x;
            _y = y;
            _state = state;
        }

        #endregion
    }
}
