/*	
	SudokuCell
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
	The control represents a single cell in a Sudoku Grid.
	There will be 81 of these cells in a grid.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuGridControl
{
    public partial class SudokuCell: UserControl
    {

		static Rectangle sm_CellRect;
		static Font sm_SelectionFont;
		static Font sm_BoldFont;
		static Font sm_SubNumFont;
		static int sm_nCount = 0;
		static int sm_nRefreshCount = 0;

		static int sm_nSubRectMargin;
		static int sm_nSubRecWidth;
		static int sm_nSubRectBot;
		static int sm_nSubRectPosIncr;

		public const int Nine = 9;

		const double c_dMainFontScaleFactor = 0.45;
		const double c_dSubFontScaleFactor = 0.16;

		int m_nCellSN;
		bool[] m_blSubNumON;

		/// <summary>
		/// Initializes a new instance of the <see cref="SudokuCell"/> class.
		/// </summary>
		public SudokuCell()
        {
			m_nCellSN = sm_nCount;
			sm_nCount++;

			InitializeComponent();

			BackgroundColor = Color.White;
			MainNumberColour = Color.DarkOliveGreen;
			SubNumberColor = Color.Gray;

			ClearCell();
        }

		/// <summary>
		/// Gets or sets the main selection.
		/// </summary>
		/// <value>The main selection.</value>
		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Data"),
		System.ComponentModel.Description("Set the initial value of the cell."),
		System.ComponentModel.DefaultValue(0)]
		public int MainSelection
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether [show possible].
		/// </summary>
		/// <value><c>true</c> if [show possible]; otherwise, <c>false</c>.</value>
		[System.ComponentModel.Browsable(true),
		System.ComponentModel.Category("Appearance"),
		System.ComponentModel.Description("Specify visible possible selections."),
		System.ComponentModel.DefaultValue(true)]
		public bool ShowPossible
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public Color BackgroundColor
		{
			set; get;
		}

		/// <summary>
		/// Gets or sets the main number colour.
		/// </summary>
		/// <value>The main number colour.</value>
		public Color MainNumberColour
		{
			set; get;
		}

		/// <summary>
		/// Gets or sets the color of the sub number.
		/// </summary>
		/// <value>The color of the sub number.</value>
		public Color SubNumberColor
		{
			set; get;
		}

		/// <summary>
		/// Clears the cell.
		/// </summary>
		public void ClearCell()
		{
			int i;
			m_blSubNumON = new bool[Nine];
			for (i = 0; i < Nine; i++)
				m_blSubNumON[i] = false;
		}

		/// <summary>
		/// Clears the refresh count.
		/// </summary>
		public static void ClearRefreshCount()
		{
			sm_nRefreshCount = 0;
		}

		/// <summary>
		/// Sets the state of the sub-number.
		/// </summary>
		/// <param name="nNum">Indicates the sub-number.</param>
		/// <param name="blState">Specify the state.</param>
		public void EnableSubNum(int nNum, bool blState)
		{
			if (nNum >= 1 && nNum <= Nine)
				m_blSubNumON[nNum-1] = blState;
		}

		/// <summary>
		/// Subs the number is enabled.
		/// </summary>
		/// <param name="nNum">The n number.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool SubNumIsEnabled(int nNum)
		{
			return m_blSubNumON[nNum - 1];
		}

		/// <summary>
		/// Handles the Paint event of the SudokuCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
		private void SudokuCell_Paint(object sender, PaintEventArgs e)
		{
			if (sm_nRefreshCount == 0)
			{
				// All of the items in here can be calculated once
				// for all cells present on the form.
				// They will share the results.
				int nFntSize;

				// Get the rectangle representing all cells.
				sm_CellRect = new Rectangle(new Point(0, 0), Size);

				// All cells use the same font.
				nFntSize = (int)(Size.Height * c_dMainFontScaleFactor);
				sm_SelectionFont = new Font("Arial", nFntSize);
				sm_BoldFont = new Font(sm_SelectionFont, FontStyle.Bold);
				nFntSize = (int)(Size.Height * c_dSubFontScaleFactor);
				sm_SubNumFont = new Font("Arial", nFntSize);
			}

			// Paint the background first.
			SolidBrush br = new SolidBrush(BackgroundColor);
			e.Graphics.FillRectangle(br, sm_CellRect);

			// Paint the number into the cell unless it is blank (0)
			if (MainSelection > 0)
			{
				Font fnt;
				SizeF sfFont;
				float fX, fY;
				string str;
				str = MainSelection.ToString();
				fnt = sm_SelectionFont;
				sfFont = e.Graphics.MeasureString(str, fnt);
				fX = ((float)Width - sfFont.Width) / 2;
				fY = ((float)Height - sfFont.Height) / 2;

				SolidBrush brFont = new SolidBrush(ForeColor);
				e.Graphics.DrawString(str, fnt, brFont, new PointF(fX, fY));
			}

			if (ShowPossible == true)
			{
				string str;
				int i, nHPos, k, nVPos;
				// Pre-calculate the static factors:
				if (sm_nRefreshCount == 0)
				{
					sm_nSubRectMargin = 1;
					sm_nSubRecWidth = (int)((double)Width / 4 + 0.5);
					sm_nSubRectBot = Height - sm_nSubRecWidth - sm_nSubRectMargin;
					sm_nSubRectPosIncr = sm_nSubRecWidth;
				}

				// Paint the background first.
				SolidBrush brFont = new SolidBrush(SubNumberColor);
				nHPos = sm_nSubRectMargin;
				nVPos = sm_nSubRectMargin;
				k = 0;
				for (i = 0; i < Nine; i++)
				{
					if (m_blSubNumON[i] == true)
					{
						if (k < 4)
							nHPos = sm_nSubRectMargin + k * sm_nSubRecWidth;
						else if (k >= 4)
						{
							nVPos = sm_nSubRectBot;
							nHPos = sm_nSubRectMargin + ((k - 4) * sm_nSubRecWidth);
						}
						str = (i + 1).ToString();
						e.Graphics.DrawString(str, sm_SubNumFont, brFont, new Point(nHPos, nVPos));
						k++;
					}
				}
			}

			sm_nRefreshCount++;
		}

		/// <summary>
		/// Handles the Resize event of the SudokuCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SudokuCell_Resize(object sender, EventArgs e)
		{
			Width = Height;
			Invalidate();
		}
	}
}
