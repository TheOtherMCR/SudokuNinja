/*	
	SudokuCellData
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		Pure data class that describes a Sudoku cells
		current state.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGridControl
{
	public enum CellStatus
	{
		CS_Unsolved,	// The value for this cell is not established
		CS_Solved,		// The value in this cell has been determined
		CS_Fixed,		// The value in this cell is one of the cells given by the puzzle setter
		CS_Tentative	// The value in this cell has been guessed at but not determined
	}

	/// <summary>
	/// Class SudokuCellData.
	/// This is the base class that will represent all 
	/// Sudoku formats.
	/// </summary>
	public class SudokuCellData
	{
		// This is the overall cell index number.
		protected int m_nCellIndex;

		// Each cell will maintain an array or 9 boolean values, each
		// representing the "not" elimination status for number 1 - 9.
		// Example:
		//	If m_arrNotEliminated[4] == true, this means that the 
		//  the number 5 COULD go in this cell (it has NOT been eliminated).
		//  If the value is false, 5 has been eliminated as a candidate
		//  for that cell. 
		bool[] m_arrNotEliminated = new bool[SudokuCell.Nine];

		/*
		CellStatus cellStatus;

		// If a value, 1 - 9, has been established, it is stored
		// here. Otherwise it is 0.
		int m_nValue;

		// Each time a cell is solved or guessed, it will be
		// assigned an "order" indicating the order in which
		// this cell was determined. It's used in back-tracking.
		int m_nSolvedOrder;

		// If the puzzled is difficult and requires guessing and
		// possible back-tracking one or more times, this value
		// tracks the branch number.
		int m_nBranchOrder;
		*/

		public SudokuCellData()
		{
			int i;
			for (i = 0; i < SudokuCell.Nine; i++)
				m_arrNotEliminated[i] = false;
			/*
			m_nValue = 0;
			cellStatus = CellStatus.CS_Unsolved;
			m_nSolvedOrder = 0;
			m_nBranchOrder = 0;
			*/
		}

		/// <summary>
		/// Gets or sets the index of the cell.
		/// </summary>
		/// <value>The index of the cell.</value>
		public int CellIndex
		{
			get { return m_nCellIndex; }
			set { m_nCellIndex = value; }
		}
	}

	/// <summary>
	/// Class StandardCellData.
	/// Extends the "SudokuCellData" base class to a specific
	/// "standard" Sudoku format with 9 rows, 9 columns and 9 subgrids.
	/// </summary>
	/// <seealso cref="SudokuGridControl.SudokuCellData" />
	public class StandardCellData : SudokuCellData
	{
		public StandardCellData(int nIndex) : base()
		{
			CellIndex = nIndex;
		}

		/// <summary>
		/// Gets the row number of the cell.
		/// </summary>
		/// <value>The row.</value>
		public int Row
		{
			get { return CellIndex / SudokuCell.Nine; }
		}

		/// <summary>
		/// Gets the column number of the cell.
		/// </summary>
		/// <value>The column.</value>
		public int Column 
		{
			get { return CellIndex % SudokuCell.Nine; }
		}

		/// <summary>
		/// Gets the cell subgrid.
		/// </summary>
		/// <param name="nFullIndex">Full index of the cell.</param>
		/// <returns>System.Int32.</returns>
		public int Subgrid
		{
			get
			{
				int sg;
				sg = Column / 3;
				sg += (Row / 3) * 3;
				return sg;
			}
		}

		/// <summary>
		/// Gets the cell row number within it's subgrid.
		/// </summary>
		/// <value>The subgrid row.</value>
		public int SubgridRow
		{
			get
			{
				return (Row % 3);
			}
		}

		/// <summary>
		/// Gets the cell Column number within it's subgrid.
		/// </summary>
		/// <value>The subgrid row.</value>
		public int SubgridColumn
		{
			get
			{
				return (Column % 3);
			}
		}
	}
}
