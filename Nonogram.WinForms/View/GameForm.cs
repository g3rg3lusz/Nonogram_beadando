using System;
using System.Drawing;
using System.Windows.Forms;
using Nonogram.Model;
using Nonogram.Persistence;

namespace Nonogram.WinForms
{
    public partial class GameForm : Form
    {
        #region private fields
        private NonogramGameModel _model;
        private Button[,] _gridButtons = null!;
        private Label[] _rowLabels = null!;
        private Label[] _colLabels = null!;
        private bool _isPaused = false;

        #endregion

        #region const values

        private const int CellSize = 40;
        private const int OffsetX = 100;
        private const int OffsetY = 80;

        #endregion

        #region constructor

        public GameForm()
        {
            InitializeComponent();

            INonogramDataAccess _dataAccess = new NonogramFileDataAccess();
            _model = new NonogramGameModel(_dataAccess);

            _model.FieldChanged += Model_FieldChanged;
            _model.RowColCompleted += Model_RowColCompleted;
            _model.GameAdvanced += Model_GameAdvanced;
            _model.GameOver += Model_GameOver;

            StartNewGame(5, 0.5);
        }

        #endregion

        #region private methods

        private int GetRandomSize()
        {
            int[] sizes = { 5, 10, 15 };
            Random rnd = new Random();
            return sizes[rnd.Next(sizes.Length)];
        }

        private void StartNewGame(int size, double fillRatio)
        {
            _model.NewRandomGame(size, fillRatio);
            GenerateGrid();
            timer.Start();
        }

        //ZH-ra fontos lehet!!!
        private void GenerateGrid()
        {
            //cleaning service
            for (int i = this.Controls.Count - 1; i >= 0; --i)
                if (this.Controls[i] is Button || (this.Controls[i] is Label && this.Controls[i].Name == "")) this.Controls.RemoveAt(i);

            int size = _model.TableSize;
            _gridButtons = new Button[size, size];
            _rowLabels = new Label[size];
            _colLabels = new Label[size];

            int minimumWidth = OffsetX + size * CellSize + 50;
            int minimumHeigth = OffsetY + size * CellSize + statusStrip.Height + 50;

            this.ClientSize = new Size(Math.Max(450, minimumWidth), minimumHeigth);

            for (int i = 0; i < size; ++i)
            {
                _rowLabels[i] = new Label
                {
                    Text = string.Join(" ", _model.GetRowNumbers(i)),
                    Location = new Point(10, OffsetY + i * CellSize),
                    Size = new Size(OffsetX - 20, CellSize),
                    TextAlign = ContentAlignment.MiddleRight,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                this.Controls.Add(_rowLabels[i]);

                _colLabels[i] = new Label
                {
                    Text = string.Join("\n", _model.GetColNumbers(i)),
                    Location = new Point(OffsetX + i * CellSize, 30),
                    Size = new Size(CellSize, OffsetY - 30),
                    TextAlign = ContentAlignment.BottomCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                this.Controls.Add(_colLabels[i]);

                for (int j = 0; j < size; ++j)
                {
                    Button button = new Button
                    {
                        Location = new Point(OffsetX + j * CellSize, OffsetY + i * CellSize),
                        Size = new Size(CellSize, CellSize),
                        BackColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        Tag = new Point(i, j)
                    };

                    button.MouseDown += GridButton_MouseDown;
                    button.MouseEnter += GridButton_MouseEnter;
                    _gridButtons[i, j] = button;
                    this.Controls.Add(button);

                    UpdateCellAppearance(i, j, _model.GetPlayerCell(i, j));
                }
            }
        }


        private void UpdateCellAppearance(int x, int y, CellState state)
        {
            Button button = _gridButtons[x, y];

            button.Text = "";
            button.BackColor = Color.White;
            button.ForeColor = Color.Black;
            button.FlatAppearance.BorderColor = Color.Black;

            if (state == CellState.Filled) button.BackColor = Color.Black;
            else if (state == CellState.Crossed)
            {
                button.Text = "X";
                button.ForeColor = Color.Red;
            }
        }

        private void newGameMenu_Click(object sender, EventArgs e) { }
        private void timerLable_Click(object sender, EventArgs e) { }
        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        private void loadGameMenu_Click(object sender, EventArgs e)
        {
            timer.Stop();
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Szövegfájlok (*.txt)|*.txt", InitialDirectory = System.IO.Path.Combine(Application.StartupPath, "Maps") })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _model.LoadGame(ofd.FileName);
                        GenerateGrid();
                        if (!_model.IsGameOver) timer.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something went wrong while loading the game: " + ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        timer.Start();
                    }
                }
                else timer.Start();
            }
        }

        private void saveGameMenu_Click(object sender, EventArgs e)
        {
            timer.Stop();
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Szövegfájlok (*.txt)|*.txt", InitialDirectory = System.IO.Path.Combine(Application.StartupPath, "Maps") })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _model.SaveGame(sfd.FileName);
                        MessageBox.Show("Succesful save!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something went wrong while saving: " + ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            timer.Start();
        }

        private void exitMenu_Click(object sender, EventArgs e) => this.Close();
        private void timer_Tick(object sender, EventArgs e) => _model.AdvanceTime();

        private void pauseGameMenu_Click(object sender, EventArgs e)
        {
            if (_gridButtons == null || _model.IsGameOver) return;

            _isPaused = !_isPaused;

            if (_isPaused)
            {
                timer.Stop();

                ((ToolStripMenuItem)sender).Text = "Continue";
                SetBoardVisibility(false);
            }
            else
            {
                timer.Start();
                ((ToolStripMenuItem)sender).Text = "Pause";
                SetBoardVisibility(true);
            }
        }

        private void SetBoardVisibility(bool isVisible)
        {
            int size = _model.TableSize;
            for (int i = 0; i < size; ++i)
            {
                _rowLabels[i].Visible = isVisible;
                _colLabels[i].Visible = isVisible;

                for (int j = 0; j < size; ++j)
                {
                    _gridButtons[i, j].Visible = isVisible;
                }
            }
        }

        private void StartBuiltInGame(string fileName)
        {
            string path = System.IO.Path.Combine(Application.StartupPath, "Maps", fileName);
            try
            {
                _model.LoadGame(path);
                GenerateGrid();
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while loading the map: " + ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region game events

        private void GridButton_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is Button button && button.Tag is Point point)
            {
                _model.Step(point.X, point.Y, e.Button == MouseButtons.Left);
                this.ActiveControl = null;

                button.Capture = false;
            }
        }

        private void GridButton_MouseEnter(object? sender, EventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left || Control.MouseButtons == MouseButtons.Right)
            {
                if (sender is Button button && button.Tag is Point point)
                {
                    _model.Step(point.X, point.Y, Control.MouseButtons == MouseButtons.Left);
                }
            }
        }

        private void Model_FieldChanged(object? sender, NonogramFieldEventArgs e) => UpdateCellAppearance(e.X, e.Y, e.State);

        private void Model_RowColCompleted(object? sender, NonogramRowColCompletedEventArgs e)
        {
            if (e.IsRow) _rowLabels[e.Index].ForeColor = Color.LightGray;
            else _colLabels[e.Index].ForeColor = Color.LightGray;
        }

        private void Model_GameAdvanced(object? sender, NonogramEventArgs e)
        {
            timeLabel.Text = $"Time: {e.GameTime} sec";
            livesLabel.Text = $" | Lives: {e.Lives}";
        }

        private void Model_GameOver(object? sender, NonogramEventArgs e)
        {
            timer.Stop();
            MessageBox.Show(e.IsWon ? "YOU WIN!" : "YOU LOOSE!");
        }

        #endregion

        #region map generation
        private void map1ToolStripMenuItem_Click(object sender, EventArgs e) => StartBuiltInGame("map_5_1.txt");

        private void map2ToolStripMenuItem_Click(object sender, EventArgs e) => StartBuiltInGame("map_5_2.txt");

        private void map3ToolStripMenuItem_Click(object sender, EventArgs e) => StartBuiltInGame("map_5_3.txt");

        private void map1ToolStripMenuItem1_Click(object sender, EventArgs e) => StartBuiltInGame("map_10_1.txt");

        private void map2ToolStripMenuItem1_Click(object sender, EventArgs e) => StartBuiltInGame("map_10_2.txt");

        private void map3ToolStripMenuItem1_Click(object sender, EventArgs e) => StartBuiltInGame("map_10_3.txt");

        private void map1ToolStripMenuItem2_Click(object sender, EventArgs e) => StartBuiltInGame("map_15_1.txt");

        private void map2ToolStripMenuItem2_Click(object sender, EventArgs e) => StartBuiltInGame("map_15_2.txt");

        private void map3ToolStripMenuItem2_Click(object sender, EventArgs e) => StartBuiltInGame("map_15_3.txt");

        private void easyToolStripMenuItem_Click(object sender, EventArgs e) => StartNewGame(GetRandomSize(), 0.3);

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e) => StartNewGame(GetRandomSize(), 0.5);

        private void hardToolStripMenuItem_Click(object sender, EventArgs e) => StartNewGame(GetRandomSize(), 0.7);

        #endregion
    }
}
