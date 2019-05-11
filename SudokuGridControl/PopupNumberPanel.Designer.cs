namespace SudokuGridControl
{
	partial class PopupNumberPanel
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
			this.btnClear = new System.Windows.Forms.Button();
			this.btnX = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnClear
			// 
			this.btnClear.BackColor = System.Drawing.Color.PaleGreen;
			this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.PaleGreen;
			this.btnClear.FlatAppearance.BorderSize = 0;
			this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
			this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClear.Location = new System.Drawing.Point(2, 80);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(51, 23);
			this.btnClear.TabIndex = 9;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = false;
			// 
			// btnX
			// 
			this.btnX.BackColor = System.Drawing.Color.Crimson;
			this.btnX.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
			this.btnX.FlatAppearance.BorderSize = 0;
			this.btnX.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.btnX.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
			this.btnX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnX.ForeColor = System.Drawing.Color.White;
			this.btnX.Location = new System.Drawing.Point(56, 80);
			this.btnX.Name = "btnX";
			this.btnX.Size = new System.Drawing.Size(24, 23);
			this.btnX.TabIndex = 10;
			this.btnX.Text = "X";
			this.btnX.UseVisualStyleBackColor = false;
			this.btnX.Click += new System.EventHandler(this.btnX_Click);
			// 
			// PopupNumberPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(82, 105);
			this.ControlBox = false;
			this.Controls.Add(this.btnX);
			this.Controls.Add(this.btnClear);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "PopupNumberPanel";
			this.ShowIcon = false;
			this.Text = "Number Entry Panel";
			this.Load += new System.EventHandler(this.PopupNumberPanel_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnX;
	}
}