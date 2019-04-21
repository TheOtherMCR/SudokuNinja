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
			this.sudokuStandardGrid1 = new SudokuGridControl.SudokuStandardGrid();
			this.SuspendLayout();
			// 
			// sudokuStandardGrid1
			// 
			this.sudokuStandardGrid1.BackColor = System.Drawing.Color.Black;
			this.sudokuStandardGrid1.Location = new System.Drawing.Point(13, 13);
			this.sudokuStandardGrid1.Name = "sudokuStandardGrid1";
			this.sudokuStandardGrid1.Size = new System.Drawing.Size(393, 393);
			this.sudokuStandardGrid1.TabIndex = 0;
			// 
			// MainNinjaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 427);
			this.Controls.Add(this.sudokuStandardGrid1);
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.Name = "MainNinjaForm";
			this.Text = "Sudoku Ninja";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainNinjaForm_FormClosing);
			this.Load += new System.EventHandler(this.MainNinjaForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private SudokuGridControl.SudokuStandardGrid sudokuStandardGrid1;
	}
}

