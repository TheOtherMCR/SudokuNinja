/*	
	SudokuCellControl
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
	A "UserControl" that represents a single Sudoku "Cell".
	81 of these will make up a complete Sudoku playing grid.
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuNinja
{
	/// <summary>
	/// Class SudokuCellControl.
	/// User control represents the visual interface and
	/// function of a single Sudoku Cell.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.UserControl" />
	public partial class SudokuCellControl : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SudokuCellControl"/> class.
		/// </summary>
		public SudokuCellControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Handles the Load event of the SudokuCellControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SudokuCellControl_Load(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Handles the SizeChanged event of the SudokuCellControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SudokuCellControl_SizeChanged(object sender, EventArgs e)
		{
			if (Width == Height)
				return;
			else if (Width < Height)
				Width = Height;
			else
				Height = Width;

			float fFontSize = Width;
			fFontSize /= 2;
			btnCellValue.Font = new Font(FontFamily.GenericSansSerif, fFontSize);
		}
	}
}
