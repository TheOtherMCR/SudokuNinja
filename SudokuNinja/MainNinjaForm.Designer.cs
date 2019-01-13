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
			this.sudokuCellControl = new SudokuNinja.SudokuCellControl();
			this.SuspendLayout();
			// 
			// sudokuCellControl
			// 
			this.sudokuCellControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sudokuCellControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sudokuCellControl.Location = new System.Drawing.Point(12, 12);
			this.sudokuCellControl.MaximumSize = new System.Drawing.Size(50, 50);
			this.sudokuCellControl.MinimumSize = new System.Drawing.Size(24, 24);
			this.sudokuCellControl.Name = "sudokuCellControl";
			this.sudokuCellControl.Size = new System.Drawing.Size(50, 50);
			this.sudokuCellControl.TabIndex = 1;
			// 
			// MainNinjaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 152);
			this.Controls.Add(this.sudokuCellControl);
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.Name = "MainNinjaForm";
			this.Text = "Sudoku Ninja";
			this.ResumeLayout(false);

		}

		#endregion
		private SudokuCellControl sudokuCellControl;
	}
}

