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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.puzzlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlSetting = new System.Windows.Forms.Panel();
			this.btnSave = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.sudokuStandardGrid = new SudokuGridControl.SudokuStandardGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPuzzleNameIn = new System.Windows.Forms.TextBox();
			this.menuStrip.SuspendLayout();
			this.pnlSetting.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.puzzlesToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(652, 24);
			this.menuStrip.TabIndex = 1;
			this.menuStrip.Text = "menuStrip";
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
			// pnlSetting
			// 
			this.pnlSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSetting.Controls.Add(this.txtPuzzleNameIn);
			this.pnlSetting.Controls.Add(this.label2);
			this.pnlSetting.Controls.Add(this.btnSave);
			this.pnlSetting.Controls.Add(this.label1);
			this.pnlSetting.Location = new System.Drawing.Point(420, 34);
			this.pnlSetting.Name = "pnlSetting";
			this.pnlSetting.Size = new System.Drawing.Size(224, 394);
			this.pnlSetting.TabIndex = 3;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(161, 78);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(53, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Give the Puzzle a Name:";
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
			this.sudokuStandardGrid.PuzzleSetupFontColor = System.Drawing.Color.DarkBlue;
			this.sudokuStandardGrid.PuzzleSetupFrameHighlight = System.Drawing.Color.Lime;
			this.sudokuStandardGrid.Size = new System.Drawing.Size(396, 396);
			this.sudokuStandardGrid.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(7, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Set a New Puzzle";
			// 
			// txtPuzzleNameIn
			// 
			this.txtPuzzleNameIn.Location = new System.Drawing.Point(11, 52);
			this.txtPuzzleNameIn.Name = "txtPuzzleNameIn";
			this.txtPuzzleNameIn.Size = new System.Drawing.Size(203, 20);
			this.txtPuzzleNameIn.TabIndex = 4;
			// 
			// MainNinjaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 440);
			this.Controls.Add(this.pnlSetting);
			this.Controls.Add(this.sudokuStandardGrid);
			this.Controls.Add(this.menuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.MainMenuStrip = this.menuStrip;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainNinjaForm";
			this.Text = "Sudoku Ninja";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainNinjaForm_FormClosing);
			this.Load += new System.EventHandler(this.MainNinjaForm_Load);
			this.Resize += new System.EventHandler(this.MainNinjaForm_Resize);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.pnlSetting.ResumeLayout(false);
			this.pnlSetting.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem puzzlesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setPuzzleToolStripMenuItem;
		private SudokuGridControl.SudokuStandardGrid sudokuStandardGrid;
		private System.Windows.Forms.Panel pnlSetting;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPuzzleNameIn;
	}
}

