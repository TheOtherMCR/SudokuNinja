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
		[DataMember]
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
		public StandardCellData[] Cell;
		protected string m_strPuzzleName;

		public SavedPuzzle()
		{
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
		public DateTime Created
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the modified.
		/// </summary>
		/// <value>The modified.</value>
		public DateTime Modified
		{
			get; set;
		}
	}
}
