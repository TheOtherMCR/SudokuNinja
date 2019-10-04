/*	
	SavedPuzzle
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		Program Attribute Template used for storing puzzles.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ProgramAttributeNS
{
	/// <summary>
	/// The class contains members and functions that will serialize, read and write 
	/// any class as long as it provides a default contructor.
	/// The "where T:new()" thing ensures that this class not be templated against a class
	/// that does not provide a default constructor.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ProgramAttribTemplate<T> where T:new()
	{
		protected bool m_blReadFromFileOK;
		protected string m_strAttribFileName;
		protected string m_strAttribGroupName;
		protected T m_ToSave;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProgramAttribTemplate{T}"/> class.
		/// Also creates an instance of the generic.
		/// </summary>
		public ProgramAttribTemplate()
		{
			m_ToSave = new T();
		}

		public T ToPersist 
		{
			get { return m_ToSave; }
		}

		/// <summary>
		/// Gets or sets the name of the Attribute File.
		/// </summary>
		/// <value>
		/// The name of the attribute file.
		/// </value>
		public string AttributeFileName
		{
			set { m_strAttribFileName = value; }
			get { return m_strAttribFileName; }
		}

		/// <summary>
		/// Gets or sets the name of the Attribute Group.
		/// </summary>
		/// <value>
		/// The name of the attribute group.
		/// </value>
		public string AttributeGroupName
		{
			set { m_strAttribGroupName = value; }
			get { return m_strAttribGroupName; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [read from file ok].
		/// </summary>
		/// <value>
		///   <c>true</c> if [read from file ok]; otherwise, <c>false</c>.
		/// </value>
		public bool ReadFromFileOK
		{
			set { m_blReadFromFileOK = value; }
			get { return m_blReadFromFileOK; }
		}

		/// <summary>
		/// Assembles a complete operation that can be used when the program loads.
		/// If the file does NOT exist, it is created and the settings are initialized to 
		/// the defaults specified by the class's default constructor.
		/// If the file does exist, it is read and the T class is initialized with the contents.
		/// </summary>
		/// <returns>true if file was found and successfully read.</returns>
		public bool CreateIfNoFile_ElseRead()
		{
			if (ReadAttributes() == true)
				return true;
			else
			{
				WriteAttributes();
				return false;
			}
		}

		/// <summary>
		/// Writes the attributes defined in T.
		/// </summary>
		public void WriteAttributes()
		{
			FileStream myStream;
			myStream = File.Create(m_strAttribFileName);
			XmlSerializer myXml = new XmlSerializer(m_ToSave.GetType(), m_strAttribGroupName);
			myXml.Serialize(myStream, m_ToSave);
			myStream.Close();
		}

		/// <summary>
		/// Reads the attributes defined in T.
		/// </summary>
		/// <returns>true if file was found and successfully read.</returns>
		public bool ReadAttributes()
		{
			ReadFromFileOK = false;
			if (File.Exists(m_strAttribFileName) == false)
			{
				return ReadFromFileOK;
			}
			FileStream myStream = null;
			try
			{
				myStream = File.OpenRead(m_strAttribFileName);
				XmlSerializer myXml = new XmlSerializer(m_ToSave.GetType(), m_strAttribGroupName);
				m_ToSave = (T)myXml.Deserialize(myStream);
				ReadFromFileOK = true;
			}
			catch (InvalidOperationException)
			{
				ReadFromFileOK = false;
			}
			finally
			{
				myStream.Close();
			}
			return ReadFromFileOK;
		}
	}
}
