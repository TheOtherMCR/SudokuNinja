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
		}

		/// <summary>
		/// Handles the Click event of the btnSave control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void btnSave_Click(object sender, EventArgs e)
		{
			SavedPuzzle sp;
			ReadCurrentGrid(out sp);
			m_PC.PuzzleList.Add(sp);
		}

		void ReadCurrentGrid(out SavedPuzzle spOut)
		{
			spOut = new SavedPuzzle();

			sudokuStandardGrid.CopyAllCellData(out spOut.Cell);
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			LoadPuzzleFile(out m_PC);
		}

		/// <summary>
		/// Loads the JSON file containing all of the power characteristic settings for the
		/// all of the tags.
		/// </summary>
		/// <returns></returns>
		public bool LoadPuzzleFile(out PuzzleCollection pc)
		{
			pc = null;
			if (File.Exists(c_strSavedPuzzles) == false)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		#endregion

	}
}
