/*	
	SudokuCell
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
	The control represents a single cell in a Sudoku Grid.
	There will be 81 of these cells in a grid.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuGridControl
{
    public partial class SudokuCell: UserControl
    {

		static Rectangle sm_CellRect;
		static Font sm_SelectionFont;
		static Font sm_BoldFont;
		static int sm_nCount = 0;
		static int sm_nRefreshCount = 0;

		const double c_dFontScaleFactor = 0.45;

		int m_nCellSN;

		public SudokuCell()
        {
			m_nCellSN = sm_nCount;
			sm_nCount++;

			InitializeComponent();

			BackgroundColor = Color.White;
			MainNumberColour = Color.DarkOliveGreen;
        }

		public static void ClearRefreshCount()
		{
			sm_nRefreshCount = 0;
		}

		/// <summary>
		/// Gets or sets the main selection.
		/// </summary>
		/// <value>The main selection.</value>
		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"),
		System.ComponentModel.Description("Set the initial value of the cell."),
		System.ComponentModel.DefaultValue(0)]
		public int MainSelection
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public Color BackgroundColor
		{
			set; get;
		}

		/// <summary>
		/// Gets or sets the main number colour.
		/// </summary>
		/// <value>The main number colour.</value>
		public Color MainNumberColour
		{
			set; get;
		}

		/// <summary>
		/// Handles the Paint event of the SudokuCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
		private void SudokuCell_Paint(object sender, PaintEventArgs e)
		{
			if (sm_nRefreshCount == 0)
			{
				// All of the items in here can be calculated once
				// for all cells present on the form.
				// They will share the results.
				int nFntSize;

				// Get the rectangle representing all cells.
				sm_CellRect = new Rectangle(new Point(0, 0), Size);

				// All cells use the same font.
				nFntSize = (int)(Size.Height * c_dFontScaleFactor);
				sm_SelectionFont = new Font("Arial", nFntSize);
				sm_BoldFont = new Font(sm_SelectionFont, FontStyle.Bold);
			}
			sm_nRefreshCount++;

			// Paint the background first.
			SolidBrush br = new SolidBrush(BackgroundColor);
			e.Graphics.FillRectangle(br, sm_CellRect);

			// Paint the number into the cell unless it is blank (0)
			if (MainSelection > 0)
			{
				Font fnt;
				SizeF sfFont;
				float fX, fY;
				string str;
				str = MainSelection.ToString();
				fnt = MainSelection % 2 == 0 ? sm_SelectionFont : sm_BoldFont;
				sfFont = e.Graphics.MeasureString(str, fnt);
				fX = ((float)Width - sfFont.Width) / 2;
				fY = ((float)Height - sfFont.Height) / 2;

				SolidBrush brFont = new SolidBrush(ForeColor);
				e.Graphics.DrawString(str, fnt, brFont, new PointF(fX, fY));
			}

		}

		/// <summary>
		/// Handles the Resize event of the SudokuCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SudokuCell_Resize(object sender, EventArgs e)
		{
			Width = Height;
			Invalidate();
		}
	}
}
