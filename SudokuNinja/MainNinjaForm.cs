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
		public MainNinjaForm()
		{
			SudokuCellControl.ClearCellUpdateCount();
			InitializeComponent();
		}

		private void MainNinjaForm_SizeChanged(object sender, System.EventArgs e)
		{
			SudokuCellControl.ClearCellUpdateCount();
		}
	}
}
