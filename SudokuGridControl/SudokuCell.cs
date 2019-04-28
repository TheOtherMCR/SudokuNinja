/*	
	SudokuCell
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
	The control represents a single cell in a Sudoku Grid.
	There will be 81 of these cells in a grid.
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuGridControl
{
	public partial class SudokuCell: UserControl
    {
		/// <summary>
		/// These static items will be shared by all 81 cells
		/// </summary>
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

		const double c_dMainFontScaleFactor = 0.50;
		const double c_dSubFontScaleFactor = 0.16;
		const double c_dMainNumberVerticalPositionPercent = 0.30;

		bool m_blHovering;

		int m_nCellSN;
		bool[] m_blSubNumON;

		// Will be assigned to the cell at initialization
		SudokuCellData cellData;

		/// <summary>
		/// Initializes a new instance of the <see cref="SudokuCell"/> class.
		/// </summary>
		public SudokuCell()
        {
			m_nCellSN = sm_nCount;
			sm_nCount++;

			InitializeComponent();

			ClearCell();
        }

		/// <summary>
		/// Attachs the cell data.
		/// </summary>
		/// <param name="scd">The SCD.</param>
		public void AttachCellData(SudokuCellData scd)
		{
			cellData = scd;
		}

		/// <summary>
		/// Gets the cell data.
		/// </summary>
		/// <value>The cell data.</value>
		public SudokuCellData CellData
		{
			get { return cellData; }
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
		/// Gets or sets the main number color.
		/// </summary>
		/// <value>The main number color.</value>
		public Color MainNumberColor
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
		/// Gets or sets the color of the outline.
		/// </summary>
		/// <value>The color of the outline.</value>
		public Color OutlineColor
		{
			set; get;
		}

		/// <summary>
		/// Gets or sets the color of the hover outline.
		/// </summary>
		/// <value>The color of the hover outline.</value>
		public Color HoverOutlineColor
		{
			set; get;
		}

		/// <summary></summary>
		public Color HoverFontColor
		{
			set; get;
		}

		/// <summary>
		/// Gets or sets the color of the hover sub number.
		/// </summary>
		/// <value>The color of the hover sub number.</value>
		public Color HoverSubNumberColor
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
			if (m_blHovering == true)
			{
				Brush brHover = new SolidBrush(HoverOutlineColor);
				Rectangle rct = new Rectangle(sm_CellRect.Location, sm_CellRect.Size);
				e.Graphics.FillRectangle(brHover, rct);
				rct.Inflate(-3, -3);
				e.Graphics.FillRectangle(br, rct);
			}
			else
			{
				e.Graphics.FillRectangle(br, sm_CellRect);
			}

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
				fY = (float)(Height - sfFont.Height);
				fY *= (float)(1.0 - c_dMainNumberVerticalPositionPercent);
				SolidBrush brFont;
				if (m_blHovering == true)
					brFont = new SolidBrush(HoverFontColor);
				else
					brFont = new SolidBrush(MainNumberColor);
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

				// Paint the subnumbers.
				SolidBrush brFont;
				if (m_blHovering == true)
					brFont = new SolidBrush(HoverSubNumberColor);
				else
					brFont = new SolidBrush(SubNumberColor);

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

		/// <summary>
		/// Handles the MouseHover event of the SudokuCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SudokuCell_MouseHover(object sender, EventArgs e)
		{
			m_blHovering = true;
			Invalidate();
		}

		private void SudokuCell_MouseLeave(object sender, EventArgs e)
		{
			m_blHovering = false;
			Invalidate();
		}
	}
}
