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
			this.SuspendLayout();
			// 
			// SudokuCellControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.MaximumSize = new System.Drawing.Size(48, 48);
			this.MinimumSize = new System.Drawing.Size(24, 24);
			this.Name = "SudokuCellControl";
			this.Size = new System.Drawing.Size(48, 48);
			this.Load += new System.EventHandler(this.SudokuCellControl_Load);
			this.SizeChanged += new System.EventHandler(this.SudokuCellControl_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
