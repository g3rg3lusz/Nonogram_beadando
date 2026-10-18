using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Nonogram.Model;
using Nonogram.Persistence;

namespace Nonogram.Test
{
    [TestClass]
    public class NonogramGameModelTest
    {
        #region private fields
        private NonogramGameModel _model = null!;
        private Mock<INonogramDataAccess> _mock = null!;
        private NonogramTable _mockedTable = null!;

        #endregion

        #region initialize

        [TestInitialize]
        public void Initialize()
        {
            _mockedTable = new NonogramTable(5);
            _mockedTable.NonogramTime = 120;
            _mockedTable.Lives = 2;

            _mockedTable.SetSolutionCell(0, 0, true);
            _mockedTable.SetPlayerCell(0, 0, CellState.Filled);
            _mockedTable.SetSolutionCell(4, 4, false);

            _mock = new Mock<INonogramDataAccess>();

            _mock.Setup(m => m.Load(It.IsAny<string>()))
                 .Returns(_mockedTable);

            _model = new NonogramGameModel(_mock.Object);

            _model.GameAdvanced += new EventHandler<NonogramEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<NonogramEventArgs>(Model_GameOver);
        }

        #endregion

        #region persistence

        [TestMethod]
        public void NonogramGameModel_LoadTest()
        {
            _model.LoadGame("teszt_palya.txt");

            Assert.AreEqual(5, _model.TableSize);
            Assert.AreEqual(120, _model.GameTime);
            Assert.AreEqual(2, _model.Lives);

            Assert.AreEqual(CellState.Filled, _model.GetPlayerCell(0, 0));

            _mock.Verify(dataAccess => dataAccess.Load(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void NonogramGameModel_SaveTest()
        {
            _model.NewRandomGame(5, 0.5);

            _model.SaveGame("mentes_teszt.txt");

            _mock.Verify(dataAccess => dataAccess.Save(It.IsAny<string>(), It.IsAny<NonogramTable>()), Times.Once());
        }

        #endregion

        #region map maker

        [TestMethod]
        public void NonogramGameModel_NewRandomGame_5x5_Test()
        {
            _model.NewRandomGame(5, 0.5);

            Assert.AreEqual(5, _model.TableSize);
            Assert.AreEqual(0, _model.GameTime);
            Assert.AreEqual(3, _model.Lives);
            Assert.IsFalse(_model.IsGameOver);

            int emptyFields = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (_model.GetPlayerCell(i, j) == CellState.Empty)
                        emptyFields++;

            Assert.AreEqual(25, emptyFields);
        }

        [TestMethod]
        public void NonogramGameModel_NewRandomGame_10x10_Test()
        {
            _model.NewRandomGame(10, 0.5);

            Assert.AreEqual(10, _model.TableSize);
            Assert.AreEqual(0, _model.GameTime);
            Assert.AreEqual(3, _model.Lives);
            Assert.IsFalse(_model.IsGameOver);

            int emptyFields = 0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (_model.GetPlayerCell(i, j) == CellState.Empty)
                        emptyFields++;

            Assert.AreEqual(100, emptyFields);
        }

        [TestMethod]
        public void NonogramGameModel_NewRandomGame_15x15_Test()
        {
            _model.NewRandomGame(15, 0.3);

            Assert.AreEqual(15, _model.TableSize);
            Assert.AreEqual(0, _model.GameTime);
            Assert.AreEqual(3, _model.Lives);

            int emptyFields = 0;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    if (_model.GetPlayerCell(i, j) == CellState.Empty)
                        emptyFields++;

            Assert.AreEqual(225, emptyFields);
        }

        #endregion

        #region filling

        [TestMethod]
        public void NonogramGameModel_Step_Test()
        {
            _model.NewRandomGame(5, 0.5);

            //jó
            int targetX = -1, targetY = -1;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (_model.IsBlackInSolution(i, j))
                    {
                        targetX = i;
                        targetY = j;
                        break;
                    }

            if (targetX != -1)
            {
                _model.Step(targetX, targetY, true);

                Assert.AreEqual(CellState.Filled, _model.GetPlayerCell(targetX, targetY));

                Assert.AreEqual(3, _model.Lives);
            }

            //rossz
            int wrongX = -1, wrongY = -1;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (!_model.IsBlackInSolution(i, j))
                    {
                        wrongX = i;
                        wrongY = j;
                        break;
                    }

            if (wrongX != -1)
            {
                _model.Step(wrongX, wrongY, true);

                Assert.AreEqual(CellState.Crossed, _model.GetPlayerCell(wrongX, wrongY));
               
                Assert.AreEqual(2, _model.Lives);
            }
        }

        #endregion

        #region time

        [TestMethod]
        public void NonogramGameModel_AdvanceTime_Test()
        {
            _model.NewRandomGame(5, 0.5);
            Assert.AreEqual(0, _model.GameTime);

            _model.AdvanceTime();
            Assert.AreEqual(1, _model.GameTime);

            _model.AdvanceTime();
            Assert.AreEqual(2, _model.GameTime);
            Assert.IsFalse(_model.IsGameOver);
        }

        #endregion

        #region game state

        private void Model_GameAdvanced(object? sender, NonogramEventArgs e)
        {
            Assert.IsGreaterThanOrEqualTo(0, _model.GameTime);
            Assert.AreEqual(e.GameTime, _model.GameTime);
            Assert.AreEqual(e.Lives, _model.Lives);
        }

        private void Model_GameOver(object? sender, NonogramEventArgs e)
        {
            Assert.IsTrue(_model.IsGameOver);
            
            if (_model.Lives == 0)
            {
                Assert.IsFalse(e.IsWon);
            }
        }

        #endregion
    }
}
