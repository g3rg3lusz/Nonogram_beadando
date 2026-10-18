using Nonogram.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Model
{
    public class NonogramGameModel
    {
        #region private fields
        
        private INonogramDataAccess _dataAccess;
        private NonogramTable _table;

        #endregion

        #region public properties

        public int TableSize => _table != null ? _table.Size : 0;
        public int Lives => _table != null ? _table.Lives : 0;
        public int GameTime => _table != null ? _table.NonogramTime : 0;
        public bool IsGameOver => (Lives == 0 || CheckWin());

        #endregion

        #region public events

        public event EventHandler<NonogramFieldEventArgs>? FieldChanged;
        public event EventHandler<NonogramRowColCompletedEventArgs>? RowColCompleted;
        public event EventHandler<NonogramEventArgs>? GameAdvanced;
        public event EventHandler<NonogramEventArgs>? GameOver;

        #endregion

        #region private events

        private void  OnFieldChanged(int x, int y) => FieldChanged?.Invoke(this, new NonogramFieldEventArgs(x, y, _table.GetPlayerCell(x, y)));
        private void OnGameAdvanced() => GameAdvanced?.Invoke(this, new NonogramEventArgs(GameTime, Lives, IsGameOver, CheckWin()));
        private void OnGameOver(bool isWon) => GameOver?.Invoke(this, new NonogramEventArgs(GameTime, Lives, true, isWon));

        #endregion

        #region constructor

        public NonogramGameModel(INonogramDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _table = new NonogramTable(5);
        }

        #endregion

        #region table management

        public CellState GetPlayerCell(int x, int y) => _table.GetPlayerCell(x, y);
        public bool IsBlackInSolution(int x, int y) => _table.IsBlackInSolution(x, y);

        //sor számai
        public List<int> GetRowNumbers(int rowIndex)
        {
            List<int> numbers = new List<int>();
            int count = 0;
            for(int i = 0; i < _table.Size; ++i)
            {
                if (_table.IsBlackInSolution(rowIndex, i)) ++count;
                else if (count > 0)
                {
                    numbers.Add(count);
                    count = 0;
                }
            }
            if(count > 0) numbers.Add(count);
            if(numbers.Count == 0) numbers.Add(0);
            return numbers;
        }

        //oszlop számai
        public List<int> GetColNumbers(int colIndex)
        {
            List<int> numbers = new List<int>();
            int count = 0;
            for (int i = 0; i < _table.Size; ++i)
            {
                if (_table.IsBlackInSolution(i, colIndex)) ++count;
                else if (count > 0)
                {
                    numbers.Add(count);
                    count = 0;
                }
            }
            if (count > 0) numbers.Add(count);
            if (numbers.Count == 0) numbers.Add(0);
            return numbers;
        }

        #endregion

        #region public methods

        public void NewRandomGame(int size, double fillRatio = 0.5)
        {
            _table = new NonogramTable(size);
            GenerateRandomMap(fillRatio);
            OnGameAdvanced();
        }

        //fix nem használok ITimert, ez elég
        public void AdvanceTime()
        {
            if (IsGameOver) return;
            _table.NonogramTime++;
            OnGameAdvanced();
        }

        public void Step(int x, int y, bool isFill)
        {
            if (IsGameOver) return;

            CellState currentState = _table.GetPlayerCell(x, y);
            if (currentState == CellState.Filled) return;

            if (isFill)
            {
                if (_table.IsBlackInSolution(x, y))
                {
                    _table.SetPlayerCell(x, y, CellState.Filled);
                    OnFieldChanged(x, y);
                    CheckRowColCompleted(x, y);

                    if (CheckWin()) OnGameOver(true);
                }
                else
                {
                    _table.SetPlayerCell(x, y, CellState.Crossed);
                    _table.Lives--;
                    OnFieldChanged(x, y);
                    OnGameAdvanced();

                    if (_table.Lives == 0) OnGameOver(false);
                }

            }
            else
            {
                if (currentState == CellState.Empty)
                {
                    _table.SetPlayerCell(x, y, CellState.Crossed);
                    OnFieldChanged(x, y);
                }
                else if (currentState == CellState.Crossed)
                {
                    _table.SetPlayerCell(x, y, CellState.Empty);
                    OnFieldChanged(x, y);
                }
            }
        }

        public void LoadGame(string path)
        {
            if(_dataAccess == null) throw new InvalidOperationException("Data cannot be reached");
            _table = _dataAccess.Load(path);
            OnGameAdvanced();
        }

        public void SaveGame(string path)
        {
            if (_dataAccess == null) throw new InvalidOperationException("Data cannot be reached");
            _dataAccess.Save(path, _table);
        }

        #endregion

        #region private methods

        private void GenerateRandomMap(double fillRatio)
        {
            Random rnd = new Random();
            for(int i = 0; i < _table.Size; ++i)
            {
                for(int j = 0; j < _table.Size; ++j)
                {
                    _table.SetSolutionCell(i, j, rnd.NextDouble() < fillRatio);
                }
            }
        }

        private bool CheckWin()
        {
            for(int i = 0; i < _table.Size; ++i)
            {
                for (int j = 0; j < _table.Size; ++j)
                {
                    if (_table.IsBlackInSolution(i, j) && _table.GetPlayerCell(i, j) != CellState.Filled)
                        return false;
                }
            }
            return true;
        }

        private void CheckRowColCompleted(int x, int y)
        {
            bool rowComplete = true;
            for(int i = 0; i < _table.Size; ++i)
            {
                if (_table.IsBlackInSolution(x, i) && _table.GetPlayerCell(x, i) != CellState.Filled)
                {
                    rowComplete = false;
                    break;
                }
            }
            if(rowComplete)
            {
                for (int i = 0; i < _table.Size; ++i)
                {
                    if (_table.GetPlayerCell(x, i) == CellState.Empty)
                    {
                        _table.SetPlayerCell(x, i, CellState.Crossed);
                        OnFieldChanged(x, i);
                    }
                }
                RowColCompleted?.Invoke(this, new NonogramRowColCompletedEventArgs(x, true));
            }

            bool colComplete = true;
            for(int i = 0; i < _table.Size; ++i)
            {
                if (_table.IsBlackInSolution(i, y) && _table.GetPlayerCell(i, y) != CellState.Filled)
                {
                    colComplete = false;
                    break;
                }
            }
            if(colComplete)
            {
                for (int i = 0; i < _table.Size; ++i)
                {
                    if (_table.GetPlayerCell(i, y) == CellState.Empty)
                    {
                        _table.SetPlayerCell(i, y, CellState.Crossed);
                        OnFieldChanged(i, y);
                    }
                }
                RowColCompleted?.Invoke(this, new NonogramRowColCompletedEventArgs(y, false));
            }
        }

        #endregion
    }
}
