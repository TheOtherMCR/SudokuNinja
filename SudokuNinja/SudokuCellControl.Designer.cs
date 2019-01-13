namespace SudokuNinja
{
	partial class SudokuCellControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlCell = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnlCell
			// 
			this.pnlCell.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlCell.BackColor = System.Drawing.SystemColors.Control;
			this.pnlCell.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCell.Location = new System.Drawing.Point(0, 0);
			this.pnlCell.Name = "pnlCell";
			this.pnlCell.Size = new System.Drawing.Size(50, 50);
			this.pnlCell.TabIndex = 0;
			this.pnlCell.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCell_Paint);
			// 
			// SudokuCellControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlCell);
			this.MaximumSize = new System.Drawing.Size(50, 50);
			this.MinimumSize = new System.Drawing.Size(24, 24);
			this.Name = "SudokuCellControl";
			this.Size = new System.Drawing.Size(50, 50);
			this.SizeChanged += new System.EventHandler(this.SudokuCellControl_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlCell;
	}
}
