namespace Nonogram.WinForms
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            newGameMenu = new ToolStripMenuItem();
            easyMenu = new ToolStripMenuItem();
            mediumMenu = new ToolStripMenuItem();
            hardMenu = new ToolStripMenuItem();
            pauseGameMenu = new ToolStripMenuItem();
            loadGameMenu = new ToolStripMenuItem();
            saveGameMenu = new ToolStripMenuItem();
            exitMenu = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            timeLabel = new ToolStripStatusLabel();
            livesLabel = new ToolStripStatusLabel();
            timer = new System.Windows.Forms.Timer(components);
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            map1ToolStripMenuItem = new ToolStripMenuItem();
            map2ToolStripMenuItem = new ToolStripMenuItem();
            map3ToolStripMenuItem = new ToolStripMenuItem();
            map1ToolStripMenuItem1 = new ToolStripMenuItem();
            map2ToolStripMenuItem1 = new ToolStripMenuItem();
            map3ToolStripMenuItem1 = new ToolStripMenuItem();
            map1ToolStripMenuItem2 = new ToolStripMenuItem();
            map2ToolStripMenuItem2 = new ToolStripMenuItem();
            map3ToolStripMenuItem2 = new ToolStripMenuItem();
            randomToolStripMenuItem = new ToolStripMenuItem();
            easyToolStripMenuItem = new ToolStripMenuItem();
            mediumToolStripMenuItem = new ToolStripMenuItem();
            hardToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { newGameMenu, pauseGameMenu, loadGameMenu, saveGameMenu, exitMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(700, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // newGameMenu
            // 
            newGameMenu.DropDownItems.AddRange(new ToolStripItem[] { easyMenu, mediumMenu, hardMenu, randomToolStripMenuItem });
            newGameMenu.Name = "newGameMenu";
            newGameMenu.Size = new Size(77, 20);
            newGameMenu.Text = "New Game";
            newGameMenu.Click += newGameMenu_Click;
            // 
            // easyMenu
            // 
            easyMenu.DropDownItems.AddRange(new ToolStripItem[] { map1ToolStripMenuItem, map2ToolStripMenuItem, map3ToolStripMenuItem });
            easyMenu.Name = "easyMenu";
            easyMenu.Size = new Size(180, 22);
            easyMenu.Text = "Easy";
            
            // 
            // mediumMenu
            // 
            mediumMenu.DropDownItems.AddRange(new ToolStripItem[] { map1ToolStripMenuItem1, map2ToolStripMenuItem1, map3ToolStripMenuItem1 });
            mediumMenu.Name = "mediumMenu";
            mediumMenu.Size = new Size(180, 22);
            mediumMenu.Text = "Medium";
           
            // 
            // hardMenu
            // 
            hardMenu.DropDownItems.AddRange(new ToolStripItem[] { map1ToolStripMenuItem2, map2ToolStripMenuItem2, map3ToolStripMenuItem2 });
            hardMenu.Name = "hardMenu";
            hardMenu.Size = new Size(180, 22);
            hardMenu.Text = "Hard";
            
            // 
            // pauseGameMenu
            // 
            pauseGameMenu.Name = "pauseGameMenu";
            pauseGameMenu.Size = new Size(84, 20);
            pauseGameMenu.Text = "Pause Game";
            pauseGameMenu.Click += pauseGameMenu_Click;
            // 
            // loadGameMenu
            // 
            loadGameMenu.Name = "loadGameMenu";
            loadGameMenu.Size = new Size(79, 20);
            loadGameMenu.Text = "Load Game";
            loadGameMenu.Click += loadGameMenu_Click;
            // 
            // saveGameMenu
            // 
            saveGameMenu.Name = "saveGameMenu";
            saveGameMenu.Size = new Size(77, 20);
            saveGameMenu.Text = "Save Game";
            saveGameMenu.Click += saveGameMenu_Click;
            // 
            // exitMenu
            // 
            exitMenu.Name = "exitMenu";
            exitMenu.Size = new Size(37, 20);
            exitMenu.Text = "Exit";
            exitMenu.Click += exitMenu_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { timeLabel, livesLabel });
            statusStrip.Location = new Point(0, 316);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 12, 0);
            statusStrip.Size = new Size(700, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip";
            statusStrip.ItemClicked += statusStrip_ItemClicked;
            // 
            // timeLabel
            // 
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(38, 17);
            timeLabel.Text = "Timer";
            timeLabel.Click += timerLable_Click;
            // 
            // livesLabel
            // 
            livesLabel.Name = "livesLabel";
            livesLabel.Size = new Size(33, 17);
            livesLabel.Text = "Lives";
            // 
            // timer
            // 
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            openFileDialog.Filter = "Szövegfájlok (*.txt)|*.txt";
            // 
            // saveFileDialog
            // 
            saveFileDialog.Filter = "Szövegfájlok (*.txt)|*.txt";
            // 
            // map1ToolStripMenuItem
            // 
            map1ToolStripMenuItem.Name = "map1ToolStripMenuItem";
            map1ToolStripMenuItem.Size = new Size(180, 22);
            map1ToolStripMenuItem.Text = "Map 1";
            map1ToolStripMenuItem.Click += map1ToolStripMenuItem_Click;
            // 
            // map2ToolStripMenuItem
            // 
            map2ToolStripMenuItem.Name = "map2ToolStripMenuItem";
            map2ToolStripMenuItem.Size = new Size(180, 22);
            map2ToolStripMenuItem.Text = "Map 2";
            map2ToolStripMenuItem.Click += map2ToolStripMenuItem_Click;
            // 
            // map3ToolStripMenuItem
            // 
            map3ToolStripMenuItem.Name = "map3ToolStripMenuItem";
            map3ToolStripMenuItem.Size = new Size(180, 22);
            map3ToolStripMenuItem.Text = "Map 3";
            map3ToolStripMenuItem.Click += map3ToolStripMenuItem_Click;
            // 
            // map1ToolStripMenuItem1
            // 
            map1ToolStripMenuItem1.Name = "map1ToolStripMenuItem1";
            map1ToolStripMenuItem1.Size = new Size(180, 22);
            map1ToolStripMenuItem1.Text = "Map 1";
            map1ToolStripMenuItem1.Click += map1ToolStripMenuItem1_Click;
            // 
            // map2ToolStripMenuItem1
            // 
            map2ToolStripMenuItem1.Name = "map2ToolStripMenuItem1";
            map2ToolStripMenuItem1.Size = new Size(180, 22);
            map2ToolStripMenuItem1.Text = "Map 2";
            map2ToolStripMenuItem1.Click += map2ToolStripMenuItem1_Click;
            // 
            // map3ToolStripMenuItem1
            // 
            map3ToolStripMenuItem1.Name = "map3ToolStripMenuItem1";
            map3ToolStripMenuItem1.Size = new Size(180, 22);
            map3ToolStripMenuItem1.Text = "Map 3";
            map3ToolStripMenuItem1.Click += map3ToolStripMenuItem1_Click;
            // 
            // map1ToolStripMenuItem2
            // 
            map1ToolStripMenuItem2.Name = "map1ToolStripMenuItem2";
            map1ToolStripMenuItem2.Size = new Size(180, 22);
            map1ToolStripMenuItem2.Text = "Map 1";
            map1ToolStripMenuItem2.Click += map1ToolStripMenuItem2_Click;
            // 
            // map2ToolStripMenuItem2
            // 
            map2ToolStripMenuItem2.Name = "map2ToolStripMenuItem2";
            map2ToolStripMenuItem2.Size = new Size(180, 22);
            map2ToolStripMenuItem2.Text = "Map 2";
            map2ToolStripMenuItem2.Click += map2ToolStripMenuItem2_Click;
            // 
            // map3ToolStripMenuItem2
            // 
            map3ToolStripMenuItem2.Name = "map3ToolStripMenuItem2";
            map3ToolStripMenuItem2.Size = new Size(180, 22);
            map3ToolStripMenuItem2.Text = "Map 3";
            map3ToolStripMenuItem2.Click += map3ToolStripMenuItem2_Click;
            // 
            // randomToolStripMenuItem
            // 
            randomToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { easyToolStripMenuItem, mediumToolStripMenuItem, hardToolStripMenuItem });
            randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            randomToolStripMenuItem.Size = new Size(180, 22);
            randomToolStripMenuItem.Text = "Random";
            // 
            // easyToolStripMenuItem
            // 
            easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            easyToolStripMenuItem.Size = new Size(180, 22);
            easyToolStripMenuItem.Text = "Easy";
            easyToolStripMenuItem.Click += easyToolStripMenuItem_Click;
            // 
            // mediumToolStripMenuItem
            // 
            mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            mediumToolStripMenuItem.Size = new Size(180, 22);
            mediumToolStripMenuItem.Text = "Medium";
            mediumToolStripMenuItem.Click += mediumToolStripMenuItem_Click;
            // 
            // hardToolStripMenuItem
            // 
            hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            hardToolStripMenuItem.Size = new Size(180, 22);
            hardToolStripMenuItem.Text = "Hard";
            hardToolStripMenuItem.Click += hardToolStripMenuItem_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "GameForm";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private MenuStrip menuStrip1;
        private ToolStripMenuItem newGameMenu;
        private ToolStripMenuItem easyMenu;
        private ToolStripMenuItem mediumMenu;
        private ToolStripMenuItem hardMenu;
        private ToolStripMenuItem loadGameMenu;
        private ToolStripMenuItem saveGameMenu;
        private ToolStripMenuItem exitMenu;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel timeLabel;
        private ToolStripStatusLabel livesLabel;
        private System.Windows.Forms.Timer timer;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private ToolStripMenuItem pauseGameMenu;
        private ToolStripMenuItem map1ToolStripMenuItem;
        private ToolStripMenuItem map2ToolStripMenuItem;
        private ToolStripMenuItem map3ToolStripMenuItem;
        private ToolStripMenuItem map1ToolStripMenuItem1;
        private ToolStripMenuItem map2ToolStripMenuItem1;
        private ToolStripMenuItem map3ToolStripMenuItem1;
        private ToolStripMenuItem map1ToolStripMenuItem2;
        private ToolStripMenuItem map2ToolStripMenuItem2;
        private ToolStripMenuItem map3ToolStripMenuItem2;
        private ToolStripMenuItem randomToolStripMenuItem;
        private ToolStripMenuItem easyToolStripMenuItem;
        private ToolStripMenuItem mediumToolStripMenuItem;
        private ToolStripMenuItem hardToolStripMenuItem;
    }
}
