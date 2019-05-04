/*	
	SudokuStandardGrid
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		This control provides the entire standard Sudoku
		playing grid.
*/
using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// The SudokuGridControl namespace.
/// 
/// The Standard Grid Control has 9 Rows, 9 Columns and 9 "Subgrids".
/// "Subgrid" is the name I give to the 9 entities that contain 9 
/// "Cells" arranged 3x3.
/// 
/// 81 "Cells" will be created. Each of them must be assigned to
/// 1 Row, 1 Column and 1 Subgrid.
/// 
///	How to assign each index 0 - 80 to which rows, columns and subgrids
///	is arbitrary but it must be consistent. I am going to choose to arrange 
///	them as follows:
///		Row 0: Cells 0 to  8
///		Row 1: Cells 9 to 17
///		..
///		Row 8: Cells 72 to 80
///		
/// This leaves us having to do some math to determine which cell belongs
/// to which column or subgrid, given a cell index 0 to 80.
/// </summary>
namespace SudokuGridControl
{
	/// <summary>
	/// Enum GridMode
	/// </summary>
	public enum GridMode
	{
		GM_Idle,
		GM_SetPuzzle
	};

	/// <summary>
	/// Class SudokuStandardGrid.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.UserControl" />
	public partial class SudokuStandardGrid : UserControl
	{
		const int c_nOuterBorderWidth = 3;
		const int c_nInnerSubgridSpacing = 3;
		const int c_nInnerCellSpacing = 1;

		const int c_nNumSubgrids = SudokuCell.Nine;
		const int c_nNumCellsPerSubgid = SudokuCell.Nine;
		const int c_nNumCellsTotal = c_nNumSubgrids * c_nNumCellsPerSubgid;

		// Calculation of allowed overall control widths:
		// We want all of the cells to be the same width, Wc and
		// of the the subgrids to be the same width Ws.
		// Ws = 3 * Wc + 2 * c_nInnerCellSpacing
		// So, Wc = (Ws - 2 * c_nInnerCellSpacing) / 3
		//		Ws - 2 * c_nInnerCellSpacing = n * 3; where n = 1, 2, 3, 4 ...
		//		Ws = n * 3 + 2 * c_nInnerCellSpacing
		//
		// The overall width of the control is then:
		// W = 3 * Ws + 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing);
		// Ws = (W - 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing)) / 3
		//
		// For a new W, calculate Ws. Find the closest Ws that will
		// satisfy: Ws = n * 3 + 2 * c_nInnerCellSpacing

		Panel[] subgridPanel = new Panel[SudokuCell.Nine];
		SudokuCell[] sudCell = new SudokuCell[SudokuCell.NineByNine];
		StandardCellData[] m_cellData = new StandardCellData[SudokuCell.NineByNine];
		int m_nSubgridWidth;
		int m_nSubgridHeight;
		int m_nCellWidth;
		int m_nCellHeight;

		protected GridMode m_gridMode;
		protected int m_nSelectedCell;

		/// <summary>
		/// Initializes a new instance of the <see cref="SudokuStandardGrid"/> class.
		/// </summary>
		public SudokuStandardGrid()
		{
			InitializeComponent();

			int i, j, k;

			for (i = 0; i < c_nNumSubgrids; i++)
			{
				subgridPanel[i] = new Panel();
				subgridPanel[i].BackColor = Color.Black;
				subgridPanel[i].BorderStyle = BorderStyle.None;
				subgridPanel[i].Name= "subgrid" + i.ToString();
				subgridPanel[i].TabStop = false;
				Controls.Add(subgridPanel[i]);
				
				for (j = 0; j < c_nNumCellsPerSubgid; j++)
				{
					k = SudokuCell.Nine * i + j;
					sudCell[k] = new SudokuCell();
					sudCell[k].BackgroundColor = Color.White;
					sudCell[k].BorderStyle = BorderStyle.None;
					sudCell[k].HoverOutlineColor = Color.Red;
					sudCell[k].HoverSubNumberColor = Color.DarkMagenta;
					sudCell[k].MainNumberColor = Color.Black;
					sudCell[k].MaximumSize = new Size(64, 64);
					sudCell[k].MinimumSize = new Size(16, 16);
					sudCell[k].ShowPossible = false;
					sudCell[k].SubNumberColor = Color.DarkGray;
					sudCell[k].Name = "cell" + k.ToString();
				}
			}
			// Now assign all of the cells to their subgrids:
			for (i = 0; i < c_nNumCellsTotal; i++)
			{
				// Initialize the cell data and attached it to the cell:
				m_cellData[i] = new StandardCellData(i);
				sudCell[i].AttachCellData(m_cellData[i]);
				j = m_cellData[i].Subgrid;
				subgridPanel[j].Controls.Add(sudCell[i]);
			}

			m_gridMode = GridMode.GM_Idle;
			m_nSelectedCell = -1;
		}

		#region Color Sets
		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("Color for Frame Highlight in Setup Mode."),
		System.ComponentModel.DefaultValue(true)]
		public Color PuzzleSetupFrameHighlight { get; set; }

		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("Color for Backgroung in Setup Mode."),
		System.ComponentModel.DefaultValue(true)]
		public Color PuzzleSetupBackground { get; set; }

		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("Color for Font in Setup Mode."),
		System.ComponentModel.DefaultValue(true)]
		public Color PuzzleSetupFontColor { get; set; }

		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("Color for Frame Highlight in Idle Mode."),
		System.ComponentModel.DefaultValue(true)]
		public Color IdleFrameHighlight { get; set; }

		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("Color for Backgroung in Idle Mode."),
		System.ComponentModel.DefaultValue(true)]
		public Color IdleBackground { get; set; }

		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("Color for Font in Idle Mode."),
		System.ComponentModel.DefaultValue(true)]
		public Color IdleFontColor { get; set; }

		#endregion

		/// <summary>
		/// Handles the Load event of the SudokuStandardGrid control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SudokuStandardGrid_Load(object sender, System.EventArgs e)
		{
			// We want to determine the overall width of the control here:
			int nWidth;
			nWidth = Width;
			nWidth -= 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing);
			nWidth -= 6 * c_nInnerCellSpacing;
			nWidth /= SudokuCell.Nine;
			nWidth *= SudokuCell.Nine;
			nWidth += 6 * c_nInnerCellSpacing;
			nWidth += 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing);
			Width = nWidth;
			Height = Width;
		}


		/// <summary>
		/// Handles the Paint event of the SudokuStandardGrid control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
		private void SudokuStandardGrid_Paint(object sender, PaintEventArgs e)
		{
			LayoutAll();
		}

		/// <summary>
		/// Places the subgrid with index from 0 to 8.
		/// The overall height and width of the panel is 
		/// accounted for.
		/// </summary>
		/// <param name="pnl">The PNL.</param>
		/// <param name="nIndex">Index of the n.</param>
		private void PlaceSubgrid(ref Panel pnl, int nIndex)
		{
			int nX, nY, nRow, nCol;

			nCol = nIndex % 3;
			nRow = nIndex / 3;
			nX = c_nOuterBorderWidth + nCol * m_nSubgridWidth + nCol * c_nInnerSubgridSpacing;
			nY = c_nOuterBorderWidth + nRow * m_nSubgridHeight + nRow * c_nInnerSubgridSpacing;

			pnl.Size = new Size(m_nSubgridWidth, m_nSubgridHeight);
			pnl.Location = new Point(nX, nY);
			pnl.BringToFront();
		}

		/// <summary>
		/// Places the cell in its parent subgrid.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="i">Cell index from 0 to 80</param>
		private void PlaceCell(int nCellIndex)
		{
			SudokuCell sc;
			StandardCellData scd;
			int nX, nY;
			sc = sudCell[nCellIndex];
			scd = sc.CellData as StandardCellData;
			sc.Size = new Size(m_nCellWidth, m_nCellHeight);
			nX = scd.SubgridColumn * (c_nInnerCellSpacing + m_nCellWidth);
			nY = scd.SubgridRow * (c_nInnerCellSpacing + m_nCellHeight);
			sc.Location = new Point(nX, nY);
		}

		/// <summary>
		/// Recalculates the sizes.
		/// </summary>
		private void RecalcSizes()
		{
			m_nSubgridWidth = Width - 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing);
			m_nSubgridWidth /= 3;
			m_nSubgridHeight = Height - 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing);
			m_nSubgridHeight /= 3;

			m_nCellWidth = (m_nSubgridWidth - 2 * c_nInnerCellSpacing) / 3;
			m_nCellHeight = (m_nSubgridHeight - 2 * c_nInnerCellSpacing) / 3;
		}

		/// <summary>
		/// Layouts all.
		/// </summary>
		private void LayoutAll()
		{
			int i;
			SuspendLayout();
			RecalcSizes();
			for (i = 0; i < c_nNumSubgrids; i++)
			{
				PlaceSubgrid(ref subgridPanel[i], i);
			}
			for (i = 0; i < c_nNumCellsTotal; i++)
			{
				PlaceCell(i);
			}
			ResumeLayout();
		}

		/// <summary>
		/// Handles the Resize event of the SudokuStandardGrid control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SudokuStandardGrid_Resize(object sender, System.EventArgs e)
		{
			Invalidate();
		}

		#region Grid Modes

		/// <summary>
		/// Sets the grid mode.
		/// </summary>
		/// <param name="gm">The gm.</param>
		public void SetGridMode(GridMode gm)
		{
			m_gridMode = gm;
			// Reset all the cells to Idle
			InitiateGridMode(GridMode.GM_Idle, true);
			InitiateGridMode(gm, false);
			ChangeSelectedCell(0);
		}

		/// <summary>
		/// Initiates the grid mode.
		/// </summary>
		/// <param name="gm">The gm.</param>
		private void InitiateGridMode(GridMode gm, bool blClearAll)
		{
			int i;
			switch (gm)
			{
				case GridMode.GM_SetPuzzle:
					for (i = 0; i < SudokuCell.NineByNine; i++)
					{
						sudCell[i].ShowPossible = false;
						if (blClearAll == true)
							sudCell[i].MainSelection = 0;
					}
					break;

				default:
					for (i = 0; i < SudokuCell.NineByNine; i++)
					{
						sudCell[i].OutlineColor = IdleFrameHighlight;
						sudCell[i].BackgroundColor = IdleBackground;
						sudCell[i].MainNumberColor = IdleFontColor;
						sudCell[i].FrameOn = false;
						if (blClearAll == true)
							sudCell[i].MainSelection = 0;
					}
					m_nSelectedCell = -1;
					break;
			}

		}

		/// <summary>
		/// Changes the selected cell.
		/// </summary>
		/// <param name="nNewSel">The n new sel.</param>
		private void ChangeSelectedCell(int nNewSel)
		{
			switch (m_gridMode)
			{
				case GridMode.GM_SetPuzzle:
					if (m_nSelectedCell >= 0 && m_nSelectedCell <= SudokuCell.NineByNine)
					{
						sudCell[m_nSelectedCell].OutlineColor = IdleFrameHighlight;
						sudCell[m_nSelectedCell].BackgroundColor = IdleBackground;
						sudCell[m_nSelectedCell].MainNumberColor = IdleFontColor;
						sudCell[m_nSelectedCell].FrameOn = false;
					}
					if (nNewSel >= 0 && nNewSel <= SudokuCell.NineByNine)
					{
						m_nSelectedCell = nNewSel;
						sudCell[m_nSelectedCell].OutlineColor = PuzzleSetupFrameHighlight;
						sudCell[m_nSelectedCell].BackgroundColor = PuzzleSetupBackground;
						sudCell[m_nSelectedCell].MainNumberColor = PuzzleSetupFontColor;
						sudCell[m_nSelectedCell].FrameOn = true;
					}
					Invalidate(true);
					break;

				default:
					break;
			}

		}

		#endregion

	}
}
