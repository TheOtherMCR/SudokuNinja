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

namespace SudokuNinja
{
	public class JSonWriter
	{
		static private bool m_blComma;

		static public bool EndLinesWithComma
		{
			set { m_blComma = value; }
			get { return m_blComma; }
		}

		public static string WriteJSonLine(int nTabs, string strLine)
		{
			string str = WriteJSonTabs(nTabs);
			str += strLine;
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSonTabs(int nTabs)
		{
			int i;
			string str = "";
			for (i = 0; i < nTabs; i++)
			{
				str += '\t';
			}
			return str;
		}

		public static string WriteJSonStringVariable(int nTabs, string strVName, string strVVal)
		{
			string str = WriteJSonTabs(nTabs);
			str += "\"" + strVName + "\": \"" + strVVal + "\"";
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteCommaAndLineFeed()
		{
			string str;
			if (m_blComma == true)
				str = ",";
			else
				str = "";
			str += "\r\n";
			return str;
		}

		public static string WriteJSonIntegerVariable(int nTabs, string strVName, int nVal)
		{
			string str = WriteJSonTabs(nTabs);
			str += "\"" + strVName + "\": " + nVal.ToString();
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSonIntegerVariable(int nTabs, string strVName, uint nVal)
		{
			string str = WriteJSonTabs(nTabs);
			str += "\"" + strVName + "\": " + nVal.ToString();
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSonKeyIntegerPair(string strVName, int nVal)
		{
			string str;
			str = "\"" + strVName + "\": " + nVal.ToString();
			if (m_blComma == true)
				str += ",";
			return str;
		}

		public static string WriteJSonBoolVariable(int nTabs, string strVName, bool bl)
		{
			string str = WriteJSonTabs(nTabs);
			str += "\"" + strVName + "\": " + (bl == true ? "true" : "false");
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSonKeyBoolPair(string strVName, bool bl)
		{
			string str;
			str = "\"" + strVName + "\": " + (bl == true ? "true" : "false");
			if (m_blComma == true)
				str += ",";
			return str;
		}

		public static string WriteJSonDoubleVariable(int nTabs, string strVName, double dVal)
		{
			string str = WriteJSonTabs(nTabs);
			str += "\"" + strVName + "\": " + dVal.ToString();
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSonDoubleVariable(int nTabs, string strVName, double dVal, int nDecPlcs)
		{
			string str = WriteJSonTabs(nTabs);
			string strFmt = "F" + nDecPlcs.ToString();
			str += "\"" + strVName + "\": " + dVal.ToString(strFmt);
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSonFloatVariable(int nTabs, string strVName, float fVal)
		{
			string str = WriteJSonTabs(nTabs);
			str += "\"" + strVName + "\": " + fVal.ToString();
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSonFloatVariable(int nTabs, string strVName, float fVal, int nDecPlcs)
		{
			string str = WriteJSonTabs(nTabs);
			string strFmt = "F" + nDecPlcs.ToString();
			str += "\"" + strVName + "\": " + fVal.ToString(strFmt);
			str += WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSon_ClassPreamble(int nTabs, string strVName)
		{
			bool blOldComma = m_blComma;
			string str = WriteJSonTabs(nTabs);
			EndLinesWithComma = false;
			str += "\"" + strVName + "\":" + WriteCommaAndLineFeed();
			str += WriteJSonTabs(nTabs + 1);
			str += "{" + WriteCommaAndLineFeed();
			return str;
		}

		public static string WriteJSon_ClassPostamble(int nTabs, bool blComma)
		{
			string str;
			str = WriteJSonTabs(nTabs + 1);
			EndLinesWithComma = blComma;
			str += "}" + WriteCommaAndLineFeed();
			return str;
		}

		/// <summary>
		/// Produces the following DateTime format:
		///		"ProgDateTime":"\/Date(1470758833262-0400)\/"
		/// </summary>
		/// <param name="nTabs"></param>
		/// <param name="strVName"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string WriteJSon_DateTime(int nTabs, string strVName, DateTime dt)
		{
			string str = WriteJSonTabs(nTabs);

			str += "\"" + strVName + "\": " + "\"" +
				dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() +
				"  " + dt.ToString("h:mm:ss tt") + "\"";
			str += WriteCommaAndLineFeed();
			return str;
		}
	}
}
