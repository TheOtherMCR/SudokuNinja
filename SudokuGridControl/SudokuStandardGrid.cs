/*	
	SudokuStandardGrid
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		This control provides the entire standard Sudoku
		playing grid.
*/
using System.Windows.Forms;

namespace SudokuGridControl
{
	public partial class SudokuStandardGrid : UserControl
	{
		Panel[] subgridPanel = new Panel[SudokuCell.Nine];
		SudokuCell[] sudCell = new SudokuCell[SudokuCell.Nine * SudokuCell.Nine];

		public SudokuStandardGrid()
		{
			InitializeComponent();

			int i, j, k;
			for (i = 0; i < SudokuCell.Nine; i++)
			{
				subgridPanel[i] = new Panel();
				for (j = 0; j < SudokuCell.Nine; j++)
				{
					k = SudokuCell.Nine * i + j;
					sudCell[k] = new SudokuCell();
					sudCell[k].BackgroundColor = System.Drawing.Color.White;
					sudCell[k].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
					sudCell[k].HoverHighlightColor = System.Drawing.Color.Red;
					sudCell[k].HoverSubnumColor = System.Drawing.Color.DarkMagenta;
					sudCell[k].MainNumberColour = System.Drawing.Color.Black;
					sudCell[k].MaximumSize = new System.Drawing.Size(64, 64);
					sudCell[k].MinimumSize = new System.Drawing.Size(16, 16);
					sudCell[k].ShowPossible = false;
					sudCell[k].SubNumberColor = System.Drawing.Color.DarkGray;
					sudCell[k].Name = "cell" + k.ToString();

					subgridPanel[i].Controls.Add(sudCell[k]);
				}
				Controls.Add(subgridPanel[i]);

				subgridPanel[i].BackColor = System.Drawing.Color.White;
				subgridPanel[i].Name = "subgridpanel" + i.ToString();
				subgridPanel[i].TabStop = false;
				subgridPanel[i].Location = new System.Drawing.Point(247, 3);
				subgridPanel[i].Size = new System.Drawing.Size(120, 120);
			}
		}

		/// <summary>
		/// Handles the Resize event of the SudokuStandardGrid control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SudokuStandardGrid_Resize(object sender, System.EventArgs e)
		{
			Width = Height;
			Invalidate();
		}
	}
}
