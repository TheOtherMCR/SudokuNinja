/*	
	SudokuNinjaForm
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
	This is the "Main Form" that will host the Sudoku
	Grid Control and other menus and controls.
*/
using System;
using System.Windows.Forms;
using SudokuGridControl;

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
		/// Handles the Resize event of the MainNinjaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void MainNinjaForm_Resize(object sender, EventArgs e)
		{
		}

		#region Puzzle Setup
		/// <summary>
		/// Handles the Click event of the setPuzzleToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void setPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			sudokuStandardGrid.SetGridMode(GridMode.GM_SetPuzzle);
		}
		#endregion
	}
}
