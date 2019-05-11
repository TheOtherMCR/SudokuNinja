/*	
	SudokuCell
	
	Copyright Mark C. Roberts. 2019 Toronto
	Part of the new Sudoku Ninja Project.

	Description:
		This is a borderless popup form that facilitates the
		entry of numbers during puzzle setting and puzzle solving.
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuGridControl
{
	public partial class PopupNumberPanel : Form
	{
		int m_nReturnCode;

		const int c_nCellDim = 24;
		const int c_nCellOutMargin = 3;
		const int c_nInnerCellSpacing = 1;

		const double c_dMainFontScaleFactor = 0.55;
		const double c_dSubFontScaleFactor = 0.16;
		const double c_dMainNumberVerticalPositionPercent = 0.30;

		SudokuCell[] arrCells;

		Rectangle m_CellRect;
		Font m_SelectionFont;
		Font m_BoldFont;
		Font m_SubNumFont;

		/// <summary>
		/// Delegate CellKeyEvent
		/// </summary>
		/// <param name="khEvent">The kh event.</param>
		/// <param name="nCell">The n cell.</param>
		SudokuStandardGrid.CellKeyEvent HandlePopupKey;

		/// <summary>
		/// Initializes a new instance of the <see cref="PopupNumberPanel"/> class.
		/// </summary>
		public PopupNumberPanel()
		{
			InitializeComponent();
			m_nReturnCode = -1;
			arrCells = new SudokuCell[SudokuCell.Nine];
			arrCells[0] = new SudokuCell(0);
			arrCells[1] = new SudokuCell(1);
			arrCells[2] = new SudokuCell(2);
			arrCells[3] = new SudokuCell(3);
			arrCells[4] = new SudokuCell(4);
			arrCells[5] = new SudokuCell(5);
			arrCells[6] = new SudokuCell(6);
			arrCells[7] = new SudokuCell(7);
			arrCells[8] = new SudokuCell(8);

			FrameHLColor = Color.DarkGoldenrod;
			BackgroundColor = Color.White;
			FontColor = Color.Black;
			HoverColor = Color.DarkGoldenrod;
			HoverFontColor = HoverColor;
		}

		/// <summary>
		/// Gets the return code.
		/// </summary>
		/// <value>The return code.</value>
		public int ReturnCode
		{
			get { return m_nReturnCode; }
		}

		public Color FrameHLColor { get; set; }
		public Color BackgroundColor  { get; set; }
		public Color FontColor  { get; set; }
		public Color HoverColor  { get; set; }
		public Color HoverFontColor  { get; set; }

		/// <summary>
		/// Handles the Load event of the PopupNumberPanel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void PopupNumberPanel_Load(object sender, EventArgs e)
		{
			int i, nFntSize;

			HandlePopupKey = new SudokuStandardGrid.CellKeyEvent(PopupKeyEvent);

			Width = 2 * (c_nCellOutMargin + c_nInnerCellSpacing) + 3 * c_nCellDim;
			Height = 2 * c_nCellOutMargin + 3 * c_nInnerCellSpacing + 4 * c_nCellDim;

			// All cells use the same font.
			nFntSize = (int)(c_nCellDim * c_dMainFontScaleFactor);
			m_SelectionFont = new Font("Arial", nFntSize);
			m_BoldFont = new Font(m_SelectionFont, FontStyle.Bold);
			nFntSize = (int)(c_nCellDim * c_dSubFontScaleFactor);
			m_SubNumFont = new Font("Arial", nFntSize);

			m_CellRect = new Rectangle(0, 0, c_nCellDim, c_nCellDim);

			// Position all the selector cells:
			for (i = 0; i < SudokuCell.Nine; i++)
			{
				arrCells[i].SetCellMetrics(ref m_SelectionFont, ref m_BoldFont, ref m_SubNumFont,
								ref m_CellRect, c_dMainNumberVerticalPositionPercent);
				arrCells[i].Width = c_nCellDim;
				arrCells[i].Height = c_nCellDim;
				arrCells[i].Left = c_nCellOutMargin + (i % 3) * (c_nCellDim + c_nInnerCellSpacing);
				arrCells[i].Top = c_nCellOutMargin + (i / 3) * (c_nCellDim + c_nInnerCellSpacing);
				arrCells[i].SetGridKeyHandler(HandlePopupKey);

				arrCells[i].FrameOn = false;
				arrCells[i].MainSelection = i + 1;
				arrCells[i].MaximumSize = new Size(64, 64);
				arrCells[i].MinimumSize = new Size(16, 16);
				arrCells[i].Name = "scEnter" + (i + 1).ToString();
				arrCells[i].ShowPossible = false;

				arrCells[i].OutlineColor = FrameHLColor;
				arrCells[i].BackgroundColor = BackgroundColor;
				arrCells[i].MainNumberColor = FontColor;
				arrCells[i].HoverOutlineColor = HoverColor;
				arrCells[i].HoverFontColor = HoverFontColor;
				Controls.Add(arrCells[i]);
			}
			// Position the clear and X buttons
			btnClear.Width = 2 * c_nCellDim + c_nInnerCellSpacing;
			btnClear.Height = c_nCellDim;
			btnClear.Left = c_nCellOutMargin;
			btnClear.Top = c_nCellOutMargin + 3 * (c_nCellDim + c_nInnerCellSpacing);
			btnX.Width = c_nCellDim;
			btnX.Height = c_nCellDim;
			btnX.Left = c_nCellOutMargin + 2 * (c_nCellDim + c_nInnerCellSpacing);
			btnX.Top = c_nCellOutMargin + 3 * (c_nCellDim + c_nInnerCellSpacing);
		}

		/// <summary>
		/// Popups the key event.
		/// </summary>
		/// <param name="khEvent">The kh event.</param>
		/// <param name="nCell">The n cell.</param>
		protected void PopupKeyEvent(KeyHandling khEvent, int nCell)
		{
			// What action is taken depends on the Grid Mode:
			switch (khEvent)
			{
				case KeyHandling.KH_Escape:
					m_nReturnCode = -1;
					break;
				case KeyHandling.KH_LClick:
					SelectCell(nCell);
					break;
			}
			Invalidate(true);
		}

		/// <summary>
		/// Selects the cell.
		/// </summary>
		/// <param name="nCellNum">The n cell number.</param>
		void SelectCell(int nCellNum)
		{
			m_nReturnCode = arrCells[nCellNum].MainSelection;
			Close();
		}


		/// <summary>
		/// Handles the Click event of the btnX control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void btnX_Click(object sender, EventArgs e)
		{
			m_nReturnCode = -1;
			Close();
		}
	}
}
