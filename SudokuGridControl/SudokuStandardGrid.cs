﻿/*	
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
		const int c_nNumSubgrids = SudokuCell.Nine;
		const int c_nNumCellsPerSubgid = SudokuCell.Nine;
		const int c_nNumCellsTotal = c_nNumSubgrids * c_nNumCellsPerSubgid;

		const int c_nOuterBorderWidth = 3;
		const int c_nInnerSubgridSpacing = 3;
		const int c_nInnerCellSpacing = 1;

		const double c_dMainFontScaleFactor = 0.55;
		const double c_dSubFontScaleFactor = 0.16;
		const double c_dMainNumberVerticalPositionPercent = 0.30;

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
		PopupNumberPanel m_popup;

		int m_nSubgridHeight;
		int m_nSubgridWidth;
		int m_nCellWidth;
		int m_nCellHeight;

		Rectangle m_CellRect;
		Font m_SelectionFont;
		Font m_BoldFont;
		Font m_SubNumFont;

		protected GridMode m_gridMode;
		protected int m_nSelectedCell;

		/// <summary>
		/// Delegate CellKeyEvent
		/// </summary>
		/// <param name="khEvent">The kh event.</param>
		/// <param name="nCell">The n cell.</param>
		public delegate void CellKeyEvent(KeyHandling khEvent, int nCell);
		CellKeyEvent CallMeOnKey;

		/// <summary>
		/// Initializes a new instance of the <see cref="SudokuStandardGrid"/> class.
		/// </summary>
		public SudokuStandardGrid()
		{
			InitializeComponent();
			CallMeOnKey = new CellKeyEvent(KeyboardEvent);

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
					sudCell[k] = new SudokuCell(k);
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
					sudCell[k].SetGridKeyHandler(CallMeOnKey);
				}
			}

			// Now assign all of the cells to their subgrids:
			for (i = 0; i < c_nNumCellsTotal; i++)
			{
				j = SudokuStandardGrid.Subgrid(i);
				subgridPanel[j].Controls.Add(sudCell[i]);
			}

			m_gridMode = GridMode.GM_Idle;
			m_nSelectedCell = -1;
			m_popup = new PopupNumberPanel();
			m_popup.StartPosition = FormStartPosition.Manual;
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
		/// Handles the Resize event of the SudokuStandardGrid control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SudokuStandardGrid_Resize(object sender, System.EventArgs e)
		{
			Invalidate();
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
		/// Recalculates the sizes.
		/// </summary>
		private void RecalcSizes()
		{
			int nFntSize;
			m_nSubgridWidth = (Width - 2 * (c_nInnerSubgridSpacing + c_nOuterBorderWidth)) / 3;
			m_nSubgridHeight = m_nSubgridWidth;

			m_nCellWidth = (m_nSubgridWidth - 2 * c_nInnerCellSpacing) / 3;
			m_nCellHeight = m_nCellWidth;

			m_CellRect = new Rectangle(0, 0, m_nCellWidth, m_nCellHeight);

			// All cells use the same font.
			nFntSize = (int)(m_nCellHeight * c_dMainFontScaleFactor);
			m_SelectionFont = new Font("Arial", nFntSize);
			m_BoldFont = new Font(m_SelectionFont, FontStyle.Bold);
			nFntSize = (int)(m_nCellHeight * c_dSubFontScaleFactor);
			m_SubNumFont = new Font("Arial", nFntSize);
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
			int nX, nY;
			sc = sudCell[nCellIndex];
			sc.Size = new Size(m_nCellWidth, m_nCellHeight);
			sc.SetCellMetrics(ref m_SelectionFont, ref m_BoldFont, ref m_SubNumFont, 
								ref m_CellRect, c_dMainNumberVerticalPositionPercent);
			nX = SubgridColumn(nCellIndex) * (c_nInnerCellSpacing + m_nCellWidth);
			nY = SubgridRow(nCellIndex) * (c_nInnerCellSpacing + m_nCellHeight);
			sc.Location = new Point(nX, nY);
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
			if (gm != GridMode.GM_Idle)
			{
				InitiateGridMode(gm, false);
				ChangeSelectedCell(0);
			}
			Invalidate(true);
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
						sudCell[i].HoverHighlightOn = false;
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
					m_nSelectedCell = 0;
					break;
			}

		}

		/// <summary>
		/// Changes the selected cell.
		/// </summary>
		/// <param name="nNewSel">The n new sel.</param>
		private void ChangeSelectedCell(int nNewSel)
		{
			int nOldCell = m_nSelectedCell;
			switch (m_gridMode)
			{
				case GridMode.GM_SetPuzzle:
					if (nOldCell >= 0 && nOldCell <= SudokuCell.NineByNine)
					{
						sudCell[nOldCell].OutlineColor = IdleFrameHighlight;
						sudCell[nOldCell].BackgroundColor = IdleBackground;
						sudCell[nOldCell].MainNumberColor = IdleFontColor;
						sudCell[nOldCell].FrameOn = false;
						sudCell[nOldCell].Invalidate();
					}
					if (nNewSel >= 0 && nNewSel <= SudokuCell.NineByNine)
					{
						m_nSelectedCell = nNewSel;
						sudCell[m_nSelectedCell].OutlineColor = PuzzleSetupFrameHighlight;
						sudCell[m_nSelectedCell].BackgroundColor = PuzzleSetupBackground;
						sudCell[m_nSelectedCell].MainNumberColor = PuzzleSetupFontColor;
						sudCell[m_nSelectedCell].FrameOn = true;
						sudCell[m_nSelectedCell].Invalidate();
					}
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <param name="nCellIndex">Index of the n cell.</param>
		/// <returns>System.Int32.</returns>
		public int GetValue(int nCellIndex)
		{
			SudokuCell sc = sudCell[nCellIndex];
			return sc.MainSelection;
		}
		#endregion

		#region Keyboard Control

		/// <summary>
		/// Keyboard events that are relevant are given a simple code
		/// and are routed here for handling along with the cell
		/// number that generated them.
		/// </summary>
		/// <param name="kh">The KeyHandling code.</param>
		/// <param name="nCellNum">The cell number.</param>
		protected void KeyboardEvent(KeyHandling kh, int nCellNum)
		{
			int nUpdateCode = 0, nNewCell;
			int nOldCell = m_nSelectedCell;

			// What action is taken depends on the Grid Mode:
			if (m_gridMode == GridMode.GM_SetPuzzle)
			{
				switch (kh)
				{
					case KeyHandling.KH_Escape:
						SetGridMode(GridMode.GM_Idle);
						nUpdateCode = 1;	// Update the cell
						break;
					case KeyHandling.KH_Up:
						nNewCell = m_nSelectedCell - ((m_nSelectedCell > 8) ? 9 : 0);
						ChangeSelectedCell(nNewCell);
						break;
					case KeyHandling.KH_Down:
						nNewCell = m_nSelectedCell + ((m_nSelectedCell < 72) ? 9 : 0);
						ChangeSelectedCell(nNewCell);
						break;
					case KeyHandling.KH_Left:
						nNewCell = m_nSelectedCell - ((m_nSelectedCell % 9) > 0 ? 1 : 0);
						ChangeSelectedCell(nNewCell);
						break;
					case KeyHandling.KH_Right:
						nNewCell = m_nSelectedCell + ((m_nSelectedCell % 9) < 8 ? 1 : 0);
						ChangeSelectedCell(nNewCell);
						break;
					case KeyHandling.KH_LClick:
						ChangeSelectedCell(nCellNum);
						break;
					case KeyHandling.KH_RClick:
						LaunchSelectorPopup(nCellNum);
						break;
				}

				if (nUpdateCode == 1)
				{
					sudCell[nOldCell].Invalidate();
					sudCell[m_nSelectedCell].Invalidate();
				}
				else if (nUpdateCode == 2)
					Invalidate(true);
			}
		}

		/// <summary>
		/// Launches the selector popup.
		/// </summary>
		/// <param name="nCellNum">The n cell number.</param>
		protected void LaunchSelectorPopup(int nCellNum)
		{
			int x, y;
			Panel pnl = sudCell[nCellNum].Parent as Panel;
			x = sudCell[nCellNum].Location.X;
			x += pnl.Location.X;
			y = sudCell[nCellNum].Location.Y;
			y += pnl.Location.Y;
			Point pt = PointToScreen(new Point(x, y));
			m_popup.Left = pt.X;
			m_popup.Top = pt.Y;
			m_popup.ShowDialog();
			if (m_popup.ReturnCode >= 0 && m_popup.ReturnCode <= 9)
			{
				sudCell[nCellNum].MainSelection = m_popup.ReturnCode;
				sudCell[nCellNum].Invalidate();
			}
		}
		#endregion

		#region Static Functions

		/// <summary>
		/// Gets the row number of the cell.
		/// </summary>
		/// <value>The row.</value>
		static public int Row(int Index)
		{
			return Index / SudokuCell.Nine;
		}

		/// <summary>
		/// Gets the column number of the cell.
		/// </summary>
		/// <value>The column.</value>
		static public int Column(int Index)
		{
			return Index % SudokuCell.Nine;
		}

		/// <summary>
		/// Gets the cell subgrid.
		/// </summary>
		/// <param name="nFullIndex">Full index of the cell.</param>
		/// <returns>System.Int32.</returns>
		static public int Subgrid(int FullIndex)
		{
			int sg, col, row;
			col = Column(FullIndex);
			row = Row(FullIndex);
			sg = col / 3;
			sg += (row / 3) * 3;
			return sg;
		}

		/// <summary>
		/// Gets the cell row number within it's subgrid.
		/// </summary>
		/// <value>The subgrid row.</value>
		static public int SubgridRow(int Index)
		{
			int row = Row(Index);
			return (row % 3);
		}

		/// <summary>
		/// Gets the cell Column number within it's subgrid.
		/// </summary>
		/// <value>The subgrid column.</value>
		static public int SubgridColumn(int Index)
		{
			int col = Column(Index);
			return (col % 3);
		}

		#endregion
	}
}
