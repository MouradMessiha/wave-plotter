using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WavePlotter
{

    public partial class ctlPlotter : UserControl
    {
        private Int16[] marrDisplayedArray;
        private Bitmap mobjFormBitmap;
        private Graphics mobjBitmapGraphics;
        private int mintFormWidth;
        private int mintFormHeight;
        private int mintDrawingAreaX1;
        private int mintDrawingAreaY1;
        private int mintDrawingAreaX2;
        private int mintDrawingAreaY2;
        private int mintDrawingAreaYCenter;
        private int mintStartPos;
        private int mintYMax;
        private int mintUnitWidth;

        public ctlPlotter()
        {
            InitializeComponent();
        }

        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void ctlPlotter_Load(object sender, EventArgs e)
        {
            mintFormWidth = this.Width;
            mintFormHeight = this.Height;
            mobjFormBitmap = new Bitmap(mintFormWidth, mintFormHeight, this.CreateGraphics());
            mobjBitmapGraphics = Graphics.FromImage(mobjFormBitmap);
            mintDrawingAreaX1 = 10;
            mintDrawingAreaY1 = 10;
            mintDrawingAreaX2 = mintFormWidth - 10;
            mintDrawingAreaY2 = mintFormHeight - 10;
            mintDrawingAreaYCenter = (mintDrawingAreaY1 + mintDrawingAreaY2) / 2;
            mintYMax = mintDrawingAreaYCenter - mintDrawingAreaY1 + 1;
            if (marrDisplayedArray == null)
                marrDisplayedArray = new Int16[] { };
            DrawPlot();
        }

        private void DrawPlot()
        {
            int intIndex;

            // white control
            mobjBitmapGraphics.FillRectangle(Brushes.White, 0, 0, mintFormWidth, mintFormHeight);

            // drawing area rectangle
            mobjBitmapGraphics.DrawRectangle(Pens.Black, mintDrawingAreaX1, mintDrawingAreaY1, mintDrawingAreaX2 - mintDrawingAreaX1 + 1, mintDrawingAreaY2 - mintDrawingAreaY1 + 1);

            // draw x-axis
            mobjBitmapGraphics.DrawLine(Pens.LightGray, mintDrawingAreaX1, mintDrawingAreaYCenter, mintDrawingAreaX2, mintDrawingAreaYCenter);

            // draw graph
            for (int intX = mintDrawingAreaX1; intX <= mintDrawingAreaX2; intX++)
            {
                intIndex = mintStartPos + intX - mintDrawingAreaX1;
                if (intIndex < marrDisplayedArray.Length - 1)
                {
                    mobjBitmapGraphics.DrawLine(Pens.Black, intX, mintDrawingAreaYCenter - (int)(((double)marrDisplayedArray[intIndex]) * ((double)mintYMax) / 32767), intX + 1, mintDrawingAreaYCenter - (int)(((double)marrDisplayedArray[intIndex + 1]) * ((double)mintYMax) / 32767));
                    if ((mintStartPos + intX - mintDrawingAreaX1) % mintUnitWidth == 0)
                        mobjBitmapGraphics.DrawLine(Pens.Green, intX, mintDrawingAreaY1, intX, mintDrawingAreaY1 + 20);
                }
            }
            this.Invalidate();
        }

        private void ctlPlotter_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(mobjFormBitmap,0,0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Do nothing
        }

        public int Position
        {
            get
            {
                return mintStartPos;
            }
            set
            {
                mintStartPos = value;
                DrawPlot();
            }
        }

        public Int16[] DisplayedArray
        {
            get
            {
                return marrDisplayedArray;
            }
            set
            {
                marrDisplayedArray = value;
                mintStartPos = 0;
                if (mobjFormBitmap != null)
                    DrawPlot();
            }
        }

        public int UnitWidth
        {
            get
            {
                return mintUnitWidth;
            }
            set
            {
                mintUnitWidth = value;
            }
        }
    }
}
