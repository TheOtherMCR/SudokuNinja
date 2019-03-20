namespace SudokuGridControl
{
    partial class SudokuCell
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
			// SudokuCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MaximumSize = new System.Drawing.Size(64, 64);
			this.MinimumSize = new System.Drawing.Size(16, 16);
			this.Name = "SudokuCell";
			this.Size = new System.Drawing.Size(62, 62);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.SudokuCell_Paint);
			this.MouseLeave += new System.EventHandler(this.SudokuCell_MouseLeave);
			this.MouseHover += new System.EventHandler(this.SudokuCell_MouseHover);
			this.Resize += new System.EventHandler(this.SudokuCell_Resize);
			this.ResumeLayout(false);

        }

        #endregion
    }
}
