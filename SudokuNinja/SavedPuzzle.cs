/*	
	SavedPuzzle
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		Pure data class that describes a Sudoku cells
		current state.
*/
using System;
using System.Runtime.Serialization;

namespace SudokuNinja
{
	[DataContract]
	public class SavedPuzzle
	{
		public SavedPuzzle()
		{

		}

		[DataMember]
		public string PuzzleName
		{
			get; set;
		}

		[DataMember]
		public DateTime CreationDate
		{
			get; set;
		}

		[DataMember]
		public DateTime LastModifiedDate
		{
			get; set;
		}

	}
}
