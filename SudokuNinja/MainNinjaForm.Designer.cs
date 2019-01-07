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
			this.sudokuCellControl1 = new SudokuNinja.SudokuCellControl();
			this.SuspendLayout();
			// 
			// sudokuCellControl1
			// 
			this.sudokuCellControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sudokuCellControl1.BackColor = System.Drawing.Color.White;
			this.sudokuCellControl1.Location = new System.Drawing.Point(12, 12);
			this.sudokuCellControl1.MaximumSize = new System.Drawing.Size(48, 48);
			this.sudokuCellControl1.MinimumSize = new System.Drawing.Size(24, 24);
			this.sudokuCellControl1.Name = "sudokuCellControl1";
			this.sudokuCellControl1.Size = new System.Drawing.Size(48, 48);
			this.sudokuCellControl1.TabIndex = 0;
			// 
			// MainNinjaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(289, 147);
			this.Controls.Add(this.sudokuCellControl1);
			this.Name = "MainNinjaForm";
			this.Text = "Sudoku Ninja";
			this.ResumeLayout(false);

		}

		#endregion

		private SudokuCellControl sudokuCellControl1;
	}
}

