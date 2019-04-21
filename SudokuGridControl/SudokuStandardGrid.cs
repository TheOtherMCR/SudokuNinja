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
		SudokuCell[] sudCell = new SudokuCell[SudokuCell.Nine * SudokuCell.Nine];

		int m_nSubgridWidth;
		int m_nSubgridHeight;
		int m_nCellWidth;
		int m_nCellHeight;

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
					sudCell[k].HoverHighlightColor = Color.Red;
					sudCell[k].HoverSubnumColor = Color.DarkMagenta;
					sudCell[k].MainNumberColour = Color.Black;
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
				j = GetCellSubgrid(i);
				subgridPanel[j].Controls.Add(sudCell[i]);
			}
		}

		/// <summary>
		/// Handles the Load event of the SudokuStandardGrid control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void SudokuStandardGrid_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Gets the cell row.
		/// </summary>
		/// <param name="nFullIndex">Full index of the cell.</param>
		/// <returns>System.Int32.</returns>
		public int GetCellRow(int nFullIndex)
		{
			return (nFullIndex / SudokuCell.Nine);
		}

		/// <summary>
		/// Gets the cell column.
		/// </summary>
		/// <param name="nFullIndex">Full index of the cell.</param>
		/// <returns>System.Int32.</returns>
		public int GetCellColumn(int nFullIndex)
		{
			return (nFullIndex % SudokuCell.Nine);
		}

		/// <summary>
		/// Gets the cell subgrid.
		/// </summary>
		/// <param name="nFullIndex">Full index of the cell.</param>
		/// <returns>System.Int32.</returns>
		public int GetCellSubgrid(int nFullIndex)
		{
			int nSG, row, col;
			row = GetCellRow(nFullIndex);
			col = GetCellColumn(nFullIndex);
			if (row < 3)
				nSG = col / 3;
			else if (row < 6)
				nSG = col / 3 + 3;
			else
				nSG = col / 3 + 6;
			return nSG;
		}

		/// <summary>
		/// Gets the cell subgrid row, which can be 0 to 2.
		/// </summary>
		/// <param name="nFullIndex">Full index of the n.</param>
		/// <returns>System.Int32.</returns>
		public int GetCellSubgridRow(int nFullIndex)
		{
			return GetCellRow(nFullIndex) % 3;
		}

		/// <summary>
		/// Gets the cell subgrid column.
		/// </summary>
		/// <param name="nFullIndex">Full index of the n.</param>
		/// <returns>System.Int32.</returns>
		public int GetCellSubgridColumn(int nFullIndex)
		{
			return GetCellColumn(nFullIndex) % 3;
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
			int nX, nY, row, col;
			sc = sudCell[nCellIndex];
			sc.Size = new Size(m_nCellWidth, m_nCellHeight);
			row = GetCellSubgridRow(nCellIndex);
			col = GetCellSubgridColumn(nCellIndex);
			nX = col * (c_nInnerCellSpacing + m_nCellWidth);
			nY = row * (c_nInnerCellSpacing + m_nCellHeight);
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
			// We want to ensure that the new size of the control
			// provides for clean integer sized results. 
			// We might make an adjustment to the size:
			int Ws1, Ws2, n, W1, W2;
			// This is the new subgrid size;
			Ws1 = (int)((Width - 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing)) / 3);
			// Ws = n * 3 + 2 * c_nInnerCellSpacing
			n = (int)((Ws1 - 2 * c_nInnerCellSpacing) / 3);
			Ws1 = n * 3 + 2 * c_nInnerCellSpacing;
			W1 = 3 * Ws1 + 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing);
			n++;
			Ws2 = n * 3 + 2 * c_nInnerCellSpacing;
			W2 = 3 * Ws2 + 2 * (c_nOuterBorderWidth + c_nInnerSubgridSpacing);
			if ((Width - W1) < (W2 - Width))
				Width = Height = W1;
			else
				Width = Height = W2;
			Invalidate();
		}
	}
}
