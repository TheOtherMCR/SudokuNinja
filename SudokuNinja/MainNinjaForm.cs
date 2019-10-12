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
using System.IO;
using SudokuGridControl;
using ProgramAttributeNS;

namespace SudokuNinja
{
	public partial class MainNinjaForm : Form
	{
		const string c_strSavedPuzzles = "SavedPuzzles.xml";

		/// <summary>
		/// Container for program settings we want to persist.
		/// </summary>
		public static ProgramAttribTemplate<PuzzleCollection> SavedPuzzles;

		protected PuzzleCollection m_PC;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainNinjaForm"/> class.
		/// </summary>
		public MainNinjaForm()
		{
			InitializeComponent();

			SavedPuzzles = new ProgramAttribTemplate<PuzzleCollection>();
			SavedPuzzles.AttributeFileName = c_strSavedPuzzles;
			SavedPuzzles.AttributeGroupName = "PuzzleListing";
		}

		/// <summary>
		/// Handles the Load event of the MainNinjaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MainNinjaForm_Load(object sender, System.EventArgs e)
		{
			SavedPuzzles.CreateIfNoFile_ElseRead();
			m_PC = SavedPuzzles.ToPersist;

			pnlSetting.Visible = false;
		}

		/// <summary>
		/// Handles the FormClosing event of the MainNinjaForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
		private void MainNinjaForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SavedPuzzles.WriteAttributes();
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

			pnlSetting.Visible = true;
			lblPuzzleSaved.Text = "";
			btnSave.Enabled = true;
			txtPuzzleNameIn.Text = "";
			txtPuzzleNameIn.Enabled = true;
			txtPuzzleNameIn.Focus();
		}

		/// <summary>
		/// Handles the Click event of the btnSave control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtPuzzleNameIn.Text.Length == 0)
			{
				lblPuzzleSaved.Text = "NEEDS A NAME";
				return;
			}

			SavedPuzzle sp;
			ReadCurrentGrid(out sp);
			m_PC.PuzzleList.Add(sp);

			lblPuzzleSaved.Text = "PUZZLE SAVED";
			btnSave.Enabled = false;
			txtPuzzleNameIn.Enabled = false;
			sudokuStandardGrid.SetGridMode(GridMode.GM_Idle);
		}

		/// <summary>
		/// Reads the current grid.
		/// Sets the puzzle for storage.
		/// </summary>
		/// <param name="spOut">The sp out.</param>
		void ReadCurrentGrid(out SavedPuzzle spOut)
		{
			int i;
			StandardCellData ssd;
			spOut = new SavedPuzzle();
			for (i = 0; i < SudokuCell.NineByNine; i++)
			{
				ssd = spOut.CellData[i];
				ssd.Index = i;
				ssd.Asgmt = sudokuStandardGrid.GetValue(i);
				if (ssd.Asgmt == 0)
					ssd.Stat = CellStatus.CS_Unsolved;
				else
					ssd.Stat = CellStatus.CS_Fixed;
			}
			spOut.PuzzleName = txtPuzzleNameIn.Text;
			spOut.Created = DateTime.Now;
			spOut.Modified = spOut.Created;
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			LoadPuzzleFile(out m_PC);
		}

		/// <summary>
		/// Loads the XML file containing all of the puzzles that we have saved.
		/// </summary>
		/// <returns>true if a file was found.</returns>
		public bool LoadPuzzleFile(out PuzzleCollection pc)
		{
			pc = null;
			if (File.Exists(c_strSavedPuzzles) == false)
			{
				return false;
			}
			else
			{
				SavedPuzzles.CreateIfNoFile_ElseRead();
				return true;
			}
		}

		#endregion

	}
}
