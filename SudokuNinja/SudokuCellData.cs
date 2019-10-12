/*	
	SudokuCellData
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		Pure data class that describes a Sudoku cells
		current state.
*/
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using SudokuGridControl;

namespace SudokuNinja
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
	[Serializable]
	public class SudokuCellData
	{
		// This is the overall cell index number.
		protected int m_nCellIndex;

		// Each cell will maintain an array or 9 boolean values, each
		// representing the "not" elimination status for number 1 - 9.
		// Example:
		//	If Eliminated[4] == true, this means that the 
		//  the number 5 has been eliminated as a candidate in this cell.
		//  If the value is true, 5 has not been eliminated as a candidate
		//  for that cell. To "solve" a cell, one should be able to eliminate
		//  8 of the 9 values that could go there, leaving only one.
		protected bool[] Eliminated = new bool[SudokuCell.Nine];

		protected CellStatus cellStatus;

		// If a value, 1 - 9, has been established, it is stored
		// here. Otherwise it is 0.
		protected int m_nValue;

		// Each time a cell is solved or guessed, it will be
		// assigned an "order" indicating the order in which
		// this cell was determined. It's used in back-tracking.
		protected int m_nSolvedOrder;

		// If the puzzle is difficult and requires guessing and
		// possible back-tracking one or more times, this value
		// tracks the branch number.
		// For example, if we have determined that there are still
		// 3 possible values that could go in a cell, we always start
		// with the lowest value number. Let's say the numbers are 2, 5 and 8.
		// If "m_nBranchOrder" is 0, no branching has been attempted.
		// If it is = 1, we have tried the lowest (2).
		// If it is = 2, we have tried 5
		protected int m_nBranchOrder;

		/// <summary>
		/// Gets or sets the index of the cell.
		/// </summary>
		/// <value>The index of the cell.</value>
		[XmlAttribute]
		public int Index
		{
			get { return m_nCellIndex; }
			set { m_nCellIndex = value; }
		}

		[XmlAttribute]
		public CellStatus Stat
		{
			get { return cellStatus; }
			set { cellStatus = value; }
		}

		[XmlAttribute]
		public int Asgmt
		{
			get { return m_nValue; }
			set { m_nValue = value; }
		}

		[XmlAttribute]
		public int SlvOr
		{
			get { return m_nSolvedOrder; }
			set { m_nSolvedOrder = value; }
		}

		[XmlAttribute]
		public int BrchOr
		{
			get { return m_nBranchOrder; }
			set { m_nBranchOrder = value; }
		}

		public SudokuCellData()
		{
			int i;
			for (i = 0; i < SudokuCell.Nine; i++)
				Eliminated[i] = false;

			m_nValue = 0;
			cellStatus = CellStatus.CS_Unsolved;
			m_nSolvedOrder = -1; 
			m_nBranchOrder = -1;
		}
	}

	/// <summary>
	/// Class StandardCellData.
	/// Extends the "SudokuCellData" base class to a specific
	/// "standard" Sudoku format with 9 rows, 9 columns and 9 subgrids.
	/// </summary>
	/// <seealso cref="SudokuGridControl.SudokuCellData" />
	[Serializable]
	public class StandardCellData : SudokuCellData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StandardCellData"/> class.
		/// We have to have a parameterless constructor for serialization.
		/// </summary>
		public StandardCellData()
		{
			ResetCell(-1);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StandardCellData"/> class.
		/// </summary>
		/// <param name="nIndex">Index of the n.</param>
		public StandardCellData(int nIndex) : base()
		{
			ResetCell(nIndex);
		}

		/// <summary>
		/// Resets the cell. All except the index.
		/// </summary>
		public void ResetCell(int nIndex)
		{
			Index = nIndex;
			Asgmt = 0;
			BrchOr = -1;
			SlvOr = -1;
			Stat = CellStatus.CS_Unsolved;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StandardCellData"/> class.
		/// </summary>
		/// <param name="sdc">The SDC.</param>
		public StandardCellData(StandardCellData sdc)
		{
			Index = sdc.Index;
			Asgmt = sdc.Asgmt;
			BrchOr = sdc.BrchOr;
			SlvOr = sdc.SlvOr;
			Stat = sdc.Stat;
		}

	}
}
