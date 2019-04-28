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
			this.sudokuStandardGrid = new SudokuGridControl.SudokuStandardGrid();
			this.SuspendLayout();
			// 
			// sudokuStandardGrid
			// 
			this.sudokuStandardGrid.BackColor = System.Drawing.Color.Black;
			this.sudokuStandardGrid.Location = new System.Drawing.Point(13, 13);
			this.sudokuStandardGrid.Name = "sudokuStandardGrid";
			this.sudokuStandardGrid.Size = new System.Drawing.Size(394, 394);
			this.sudokuStandardGrid.TabIndex = 0;
			// 
			// MainNinjaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 427);
			this.Controls.Add(this.sudokuStandardGrid);
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.Name = "MainNinjaForm";
			this.Text = "Sudoku Ninja";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainNinjaForm_FormClosing);
			this.Load += new System.EventHandler(this.MainNinjaForm_Load);
			this.Resize += new System.EventHandler(this.MainNinjaForm_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private SudokuGridControl.SudokuStandardGrid sudokuStandardGrid;
	}
}

