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
	public enum KeyHandling
	{
		KH_Blank,
		KH_One, KH_Two, KH_Three, KH_Four, KH_Five,
		KH_Six, KH_Seven, KH_Eight, KH_Nine, 
		KH_Up, KH_Down, KH_Left, KH_Right, 
		KH_Escape, KH_LClick, KH_RClick, KH_Null
	}

	public partial class SudokuCell: UserControl
    {
		public const int Nine = 9;
		public const int NineByNine = 81;

		/// <summary>
		/// These items will be the same for all cells in a grid
		/// so they will be calculated once from the outside and then
		/// assigned to each cell.
		/// </summary>
		Rectangle m_CellRect;
		Font m_SelectionFont;
		Font m_BoldFont;
		Font m_SubNumFont;
		double m_dMainNumberScaling;

		bool m_blHovering;

		int m_nCellSN;
		bool[] m_blSubNumON;

		SudokuStandardGrid.CellKeyEvent TellGridAboutKeyEvent;

		/// <summary>
		/// Initializes a new instance of the <see cref="SudokuCell"/> class.
		/// </summary>
		public SudokuCell(int nSN)
        {
			m_nCellSN = nSN;

			InitializeComponent();

			ClearCell();
        }

		/// <summary>
		/// Sets the grid key handler.
		/// </summary>
		/// <param name="cke">The cke.</param>
		public void SetGridKeyHandler(SudokuStandardGrid.CellKeyEvent cke)
		{
			TellGridAboutKeyEvent = cke;
		}

		/// <summary>
		/// Sets the cell metrics.
		/// </summary>
		/// <param name="MainFont">The main font.</param>
		/// <param name="BoldFont">The bold font.</param>
		/// <param name="SubFont">The sub font.</param>
		/// <param name="rctCell">The RCT cell.</param>
		public void SetCellMetrics(ref Font MainFont, ref Font BoldFont, 
									ref Font SubFont, ref Rectangle rctCell,
									double dMainScaling)
		{
			m_SelectionFont = MainFont;
			m_BoldFont = BoldFont;
			m_SubNumFont = SubFont;
			m_CellRect = rctCell;
			m_dMainNumberScaling = dMainScaling;
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
		/// Gets or sets a value indicating whether [frame on].
		/// </summary>
		/// <value><c>true</c> if [frame on]; otherwise, <c>false</c>.</value>
		public bool FrameOn
		{
			get; set;
		}

		public bool HoverHighlightOn
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
			FrameOn = false;
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
			// Paint the background first.
			SolidBrush br = new SolidBrush(BackgroundColor);
			if ((m_blHovering == true && HoverHighlightOn == true) || FrameOn == true)
			{
				Brush brOutline = new SolidBrush(FrameOn == true ? OutlineColor : HoverOutlineColor);
				Rectangle rct = new Rectangle(m_CellRect.Location, m_CellRect.Size);
				e.Graphics.FillRectangle(brOutline, rct);
				rct.Inflate(-3, -3);
				e.Graphics.FillRectangle(br, rct);
			}
			else
			{
				e.Graphics.FillRectangle(br, m_CellRect);
			}

			// Paint the number into the cell unless it is blank (0)
			if (MainSelection > 0)
			{
				SizeF sfFont;
				float fX, fY;
				string str;
				str = MainSelection.ToString();
				sfFont = e.Graphics.MeasureString(str, m_SelectionFont);
				fX = ((float)Width - sfFont.Width) / 2;
				fY = (float)(Height - sfFont.Height);
				fY *= (float)(1.0 - m_dMainNumberScaling);
				SolidBrush brFont;
				if (m_blHovering == true && HoverHighlightOn == true)
					brFont = new SolidBrush(HoverFontColor);
				else
					brFont = new SolidBrush(MainNumberColor);
				e.Graphics.DrawString(str, m_SelectionFont, brFont, new PointF(fX, fY));
			}

			if (ShowPossible == true)
			{
				string str;
				int i, nHPos, k, nVPos;
				int nSubRectMargin, nSubRecWidth, nSubRectBot;

				nSubRectMargin = 1;
				nSubRecWidth = (int)((double)Width / 4 + 0.5);
				nSubRectBot = Height - nSubRecWidth - nSubRectMargin;

				// Paint the subnumbers.
				SolidBrush brFont;
				if (m_blHovering == true)
					brFont = new SolidBrush(HoverSubNumberColor);
				else
					brFont = new SolidBrush(SubNumberColor);

				nHPos = nSubRectMargin;
				nVPos = nSubRectMargin;
				k = 0;
				for (i = 0; i < Nine; i++)
				{
					if (m_blSubNumON[i] == true)
					{
						if (k < 4)
							nHPos = nSubRectMargin + k * nSubRecWidth;
						else if (k >= 4)
						{
							nVPos = nSubRectBot;
							nHPos = nSubRectMargin + ((k - 4) * nSubRecWidth);
						}
						str = (i + 1).ToString();

						e.Graphics.DrawString(str, m_SubNumFont, brFont, new Point(nHPos, nVPos));
						k++;
					}
				}
			}
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

		#region Mouse Control
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

		/// <summary>
		/// Handles the MouseLeave event of the SudokuCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SudokuCell_MouseLeave(object sender, EventArgs e)
		{
			m_blHovering = false;
			Invalidate();
		}

		/// <summary>
		/// Handles the MouseClick event of the SudokuCell control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void SudokuCell_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				TellGridAboutKeyEvent(KeyHandling.KH_RClick, m_nCellSN);
			else if (e.Button == MouseButtons.Left)
				TellGridAboutKeyEvent(KeyHandling.KH_LClick, m_nCellSN);
		}

		#endregion

		#region Keyboard Handling

		/// <summary>
		/// Processes a command key.
		/// </summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns><see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			KeyHandling kh;
			switch (keyData)
			{
				case Keys.Up:
					kh = KeyHandling.KH_Up;
					break;
				case Keys.Down:
					kh = KeyHandling.KH_Down;
					break;
				case Keys.Left:
					kh = KeyHandling.KH_Left;
					break;
				case Keys.Right:
					kh = KeyHandling.KH_Right;
					break;
				case Keys.Escape:
					kh = KeyHandling.KH_Escape;
					break;
				case Keys.PageDown:
					kh = KeyHandling.KH_RClick;
					break;
				default:
					kh = KeyHandling.KH_Null;
					break;
			}
			if (kh != KeyHandling.KH_Null)
			{
				TellGridAboutKeyEvent(kh, m_nCellSN);
				return true;
			}
			else
				return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <summary>
		/// Handles the KeyPress event of the SudokuCell control.
		/// It will assigned a "KayHandling" value to the key and
		/// send it up to the parent grid via the delegate.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
		private void SudokuCell_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar >= '1' && e.KeyChar <= '9')
			{
				KeyHandling kh = (KeyHandling)(e.KeyChar - '0');
				TellGridAboutKeyEvent(kh, m_nCellSN);
			}
			else if (e.KeyChar == ' ' || e.KeyChar == '0')
			{
				KeyHandling kh = KeyHandling.KH_Blank;
				TellGridAboutKeyEvent(kh, m_nCellSN);
			}
		}

		#endregion

	}
}
