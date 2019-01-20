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
		SudokuCell[] arrCells;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainNinjaForm"/> class.
		/// </summary>
		public MainNinjaForm()
		{
			InitializeComponent();
			arrCells = new SudokuCell[SudokuCell.Nine];
			arrCells[0] = sudokuCell1;
			arrCells[1] = sudokuCell2;
			arrCells[2] = sudokuCell3;
			arrCells[3] = sudokuCell4;
			arrCells[4] = sudokuCell5;
			arrCells[5] = sudokuCell6;
			arrCells[6] = sudokuCell7;
			arrCells[7] = sudokuCell8;
			arrCells[8] = sudokuCell9;

			sudokuCell1.MainSelection = 1;
			sudokuCell2.MainSelection = 2;
			sudokuCell3.MainSelection = 3;
			sudokuCell4.MainSelection = 4;
			sudokuCell5.MainSelection = 5;
			sudokuCell6.MainSelection = 6;
			sudokuCell7.MainSelection = 7;
			sudokuCell8.MainSelection = 8;
			sudokuCell9.MainSelection = 9;

			sudokuCell1.ShowPossible = true;
			sudokuCell2.ShowPossible = true;
			sudokuCell3.ShowPossible = true;
			sudokuCell4.ShowPossible = true;
			sudokuCell5.ShowPossible = true;
			sudokuCell6.ShowPossible = true;
			sudokuCell7.ShowPossible = true;
			sudokuCell8.ShowPossible = true;
			sudokuCell9.ShowPossible = true;

		}

		/// <summary>
		/// Handles the Load event of the MainNinjaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MainNinjaForm_Load(object sender, System.EventArgs e)
		{
			/*  
			 *  For Test Only
			 */
			int i, j, k, n;
			Random rnd = new Random();
			for (i = 0; i < SudokuCell.Nine; i++)
			{
				n = rnd.Next(1, 8);
				for (j = 0; j < n; j++)
				{
					do
					{
						k = rnd.Next(1, 9);
					}
					while (arrCells[i].SubNumIsEnabled(k) == true ||
							k == arrCells[i].MainSelection);

					arrCells[i].EnableSubNum(k, true);
				}
			}
			// End of Test
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
