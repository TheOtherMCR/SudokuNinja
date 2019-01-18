/*	
	SudokuNinjaForm
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
	This is the "Main Form" that will host the Sudoku
	Grid Control and other menus and controls.
*/
using System.Windows.Forms;

namespace SudokuNinja
{
	public partial class MainNinjaForm : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MainNinjaForm"/> class.
		/// </summary>
		public MainNinjaForm()
		{
			InitializeComponent();

			sudokuCell1.MainSelection = 1;
			sudokuCell2.MainSelection = 2;
			sudokuCell3.MainSelection = 3;
			sudokuCell4.MainSelection = 4;
			sudokuCell5.MainSelection = 5;
			sudokuCell6.MainSelection = 6;
			sudokuCell7.MainSelection = 7;
			sudokuCell8.MainSelection = 8;
			sudokuCell9.MainSelection = 9;
		}

		/// <summary>
		/// Handles the Load event of the MainNinjaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MainNinjaForm_Load(object sender, System.EventArgs e)
		{

		}

		/// <summary>
		/// Handles the FormClosing event of the MainNinjaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
		private void MainNinjaForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		/// <summary>
		/// Handles the SizeChanged event of the MainNinjaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MainNinjaForm_SizeChanged(object sender, System.EventArgs e)
		{
			SudokuGridControl.SudokuCell.ClearRefreshCount();
		}
	}
}
