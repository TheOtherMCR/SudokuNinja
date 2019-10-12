/*	
	SavedPuzzle
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		Pure data class that describes a Sudoku cells
		current state.
*/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using SudokuGridControl;

namespace SudokuNinja
{
	/// <summary>
	/// Class PuzzleCollection.
	/// A listing of saved puzzles.
	/// </summary>
	[Serializable]
	public class PuzzleCollection
	{
		public List<SavedPuzzle> PuzzleList;

		public PuzzleCollection()
		{
			PuzzleList = new List<SavedPuzzle>();
		}
	}

	/// <summary>
	/// Class SavedPuzzle.
	/// A single saved puzzle.
	/// </summary>
	[Serializable]
	public class SavedPuzzle
	{
		protected StandardCellData[] cellData;
		protected string m_strPuzzleName;

		/// <summary>
		/// Initializes a new instance of the <see cref="SavedPuzzle"/> class.
		/// </summary>
		public SavedPuzzle()
		{
			int i;
			cellData = new StandardCellData[SudokuCell.NineByNine];
			for (i = 0; i < SudokuCell.NineByNine; i++)
				cellData[i] = new StandardCellData(i);
		}

		/// <summary>
		/// Gets or sets the cell data.
		/// </summary>
		/// <value>The cell data.</value>
		[XmlArray("CellsInPuzzle")]
		public StandardCellData[] CellData
		{
			get { return cellData; }
			set { cellData = value; }
		}

		/// <summary>
		/// Gets or sets the name of the puzzle.
		/// </summary>
		/// <value>The name of the puzzle.</value>
		[XmlAttribute]
		public string PuzzleName
		{
			get { return m_strPuzzleName; }
			set { m_strPuzzleName = value; }
		}

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>The created.</value>
		[XmlAttribute]
		public DateTime Created
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the modified.
		/// </summary>
		/// <value>The modified date.</value>
		[XmlAttribute]
		public DateTime Modified
		{
			get; set;
		}
	}
}
