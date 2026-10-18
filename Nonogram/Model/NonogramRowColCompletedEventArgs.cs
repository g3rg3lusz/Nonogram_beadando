using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Model
{
    public class NonogramRowColCompletedEventArgs : EventArgs
    {
        #region private fields

        private int _index;
        private bool _isRow;

        #endregion

        #region public properties

        public int Index { get { return _index; } }
        public bool IsRow { get { return _isRow; } }

        #endregion

        #region constructors

        public NonogramRowColCompletedEventArgs(int index, bool isRow)
        {
            _index = index;
            _isRow = isRow;
        }

        #endregion
    }
}
