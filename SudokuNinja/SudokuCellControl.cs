/*	
	SudokuCellControl
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
	A "UserControl" that represents a single Sudoku "Cell".
	81 of these will make up a complete Sudoku playing grid.
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

namespace SudokuNinja
{
	public partial class SudokuCellControl : UserControl
	{
		SolidBrush m_bgBrush;
		Brush m_brMain;
		Font m_fgFont;

		/// <summary>
		/// Initializes a new instance of the <see cref="SudokuCellControl"/> class.
		/// </summary>
		public SudokuCellControl()
		{
			InitializeComponent();

			m_bgBrush = new SolidBrush(Color.Honeydew);
		}

		/// <summary>
		/// Gets or sets the 1 - 9 assignment for the cell.
		/// </summary>
		/// <value>The assignment.</value>
		[Browsable(true), Category("Appearance"), Description("Value from 1 to 9 to assign to the cell. 0 = not assigned."), DefaultValue((int)0)]
		public int Assignment
		{
			set;
			get;
		}

		/// <summary>
		/// Handles the Paint event of the pnlCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
		private void pnlCell_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rct;
			RectangleF rctMain;
			rct = pnlCell.ClientRectangle;
			rctMain = pnlCell.ClientRectangle;
			e.Graphics.FillRectangle(m_bgBrush, rct);
			m_brMain = Brushes.Black;
			m_fgFont = new Font("SansSerif", (float)40.0);
			e.Graphics.DrawString(Assignment.ToString(), m_fgFont, m_brMain, rctMain);
		}

		/// <summary>
		/// Handles the SizeChanged event of the SudokuCellControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SudokuCellControl_SizeChanged(object sender, EventArgs e)
		{
			// We may want to do something here to force the cell to be square.
			Width = Height;		// Well that was easy.
		}
	}
}
