namespace SudokuNinja
{
	partial class MainNinjaForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.puzzlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sudokuStandardGrid = new SudokuGridControl.SudokuStandardGrid();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.puzzlesToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(542, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip";
			// 
			// puzzlesToolStripMenuItem
			// 
			this.puzzlesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setPuzzleToolStripMenuItem});
			this.puzzlesToolStripMenuItem.Name = "puzzlesToolStripMenuItem";
			this.puzzlesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			this.puzzlesToolStripMenuItem.Text = "Puzzles";
			// 
			// setPuzzleToolStripMenuItem
			// 
			this.setPuzzleToolStripMenuItem.Name = "setPuzzleToolStripMenuItem";
			this.setPuzzleToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.setPuzzleToolStripMenuItem.Text = "Set Puzzle";
			this.setPuzzleToolStripMenuItem.Click += new System.EventHandler(this.setPuzzleToolStripMenuItem_Click);
			// 
			// sudokuStandardGrid
			// 
			this.sudokuStandardGrid.BackColor = System.Drawing.Color.Black;
			this.sudokuStandardGrid.IdleBackground = System.Drawing.Color.White;
			this.sudokuStandardGrid.IdleFontColor = System.Drawing.Color.Black;
			this.sudokuStandardGrid.IdleFrameHighlight = System.Drawing.Color.White;
			this.sudokuStandardGrid.Location = new System.Drawing.Point(13, 32);
			this.sudokuStandardGrid.Name = "sudokuStandardGrid";
			this.sudokuStandardGrid.PuzzleSetupBackground = System.Drawing.Color.White;
			this.sudokuStandardGrid.PuzzleSetupFontColor = System.Drawing.Color.Red;
			this.sudokuStandardGrid.PuzzleSetupFrameHighlight = System.Drawing.Color.Lime;
			this.sudokuStandardGrid.Size = new System.Drawing.Size(396, 396);
			this.sudokuStandardGrid.TabIndex = 2;
			// 
			// MainNinjaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(542, 501);
			this.Controls.Add(this.sudokuStandardGrid);
			this.Controls.Add(this.menuStrip1);
			this.HelpButton = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "MainNinjaForm";
			this.Text = "Sudoku Ninja";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainNinjaForm_FormClosing);
			this.Load += new System.EventHandler(this.MainNinjaForm_Load);
			this.Resize += new System.EventHandler(this.MainNinjaForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem puzzlesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setPuzzleToolStripMenuItem;
		private SudokuGridControl.SudokuStandardGrid sudokuStandardGrid;
	}
}

