using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WavePlotter
{
    public partial class frmMain : Form
    {
        private string mstrFileName = "";
        private byte[] marrFileContents;
        private Int16[] marrChannel1;
        private Int16[] marrChannel2;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            OpenFileDialog objDialog = new OpenFileDialog();
            objDialog.InitialDirectory = "C:\\";
            objDialog.DefaultExt = ".wav";
            objDialog.Filter = "Wav files|*.wav";

            DialogResult result = objDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                mstrFileName = objDialog.FileName;

                // read the file into the byte array
                FileStream objFileStream = new FileStream(mstrFileName, FileMode.Open, FileAccess.Read);
                try
                {
                    int intLength = (int)objFileStream.Length;
                    int intAllocatedLength = AllocateMemory(ref marrFileContents, intLength);
                    if (intAllocatedLength != intLength)
                        MessageBox.Show("Full file cannot be displayed because of its size \nOnly " + Convert.ToString(intAllocatedLength) + " of " + Convert.ToString(intLength) + " will be displayed");
                    int intCount;
                    int intOffset = 0;
                    while ((intCount = objFileStream.Read(marrFileContents, intOffset, intAllocatedLength - intOffset)) > 0)
                        intOffset += intCount;
                }
                finally
                {
                    objFileStream.Close();
                }

                DisplayFile();
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                Application.Exit();
            }
        }

        private int AllocateMemory(ref byte[] arrFileContents, int intLength)
        {
            bool blnDone = false;
            int intAllocatedLength;
            int intIterations = 0;

            intAllocatedLength = intLength;

            while (!blnDone && intIterations < 100)
            {
                try
                {
                    arrFileContents = new byte[intAllocatedLength];
                    blnDone = true;
                }
                catch
                {
                    intIterations++;
                    intAllocatedLength = intAllocatedLength / 2;
                }
            }

            return intAllocatedLength;
        }

        private void DisplayFile()
        {
            int intSampleSize;

            // check audio format = 1 for PCM (byte 20)
            if (marrFileContents[20] != 1)
                MessageBox.Show("Audio format not equal to 1: " + marrFileContents[20].ToString());

            // check number of channels = 2, stereo (byte 22)
            if (marrFileContents[22] != 2)
                MessageBox.Show("Number of channels not equal to 2: " + marrFileContents[22].ToString());

            // Check bits per sample = 16 (byte 34)
            if (marrFileContents[34] != 16)
                MessageBox.Show("Bits per sample not equal 16: " + marrFileContents[34].ToString());

            // number of sample units
            intSampleSize = (((Int32)marrFileContents[40]) + (((Int32)marrFileContents[41]) << 8) + (((Int32)marrFileContents[42]) << 16) + (((Int32)marrFileContents[43]) << 24)) / 4;

            ctlPlotter1.UnitWidth = (((Int32)marrFileContents[24]) + (((Int32)marrFileContents[25]) << 8) + (((Int32)marrFileContents[26]) << 16) + (((Int32)marrFileContents[27]) << 24)) / 100;
            ctlPlotter2.UnitWidth = (((Int32)marrFileContents[24]) + (((Int32)marrFileContents[25]) << 8) + (((Int32)marrFileContents[26]) << 16) + (((Int32)marrFileContents[27]) << 24)) / 100;

            marrChannel1 = new Int16[intSampleSize];
            marrChannel2 = new Int16[intSampleSize];

            for (int intIndex = 0; intIndex < intSampleSize ; intIndex++)
            {
                marrChannel1[intIndex] = (Int16)(((int)marrFileContents[44 + intIndex * 4]) + (((int)marrFileContents[45 + intIndex * 4]) << 8));
                marrChannel2[intIndex] = (Int16)(((int)marrFileContents[46 + intIndex * 4]) + (((int)marrFileContents[47 + intIndex * 4]) << 8));
            }
            ctlPlotter1.DisplayedArray = marrChannel1;
            ctlPlotter2.DisplayedArray = marrChannel2;

            scrScrollbar.Minimum = 0;
            scrScrollbar.SmallChange = 10;
            scrScrollbar.LargeChange = ctlPlotter1.UnitWidth;
            scrScrollbar.Maximum = ctlPlotter1.DisplayedArray.Length + ctlPlotter1.UnitWidth - 2;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (ctlPlotter1.Position < ctlPlotter1.DisplayedArray.Length - 1)
                    {
                        ctlPlotter1.Position +=10;
                        ctlPlotter2.Position += 10;
                        scrScrollbar.Value = ctlPlotter1.Position;
                    }
                    break;

                case Keys.Left:
                    if (ctlPlotter2.Position >= 10)
                    {
                        ctlPlotter1.Position -= 10;
                        ctlPlotter2.Position -= 10;
                        scrScrollbar.Value = ctlPlotter1.Position;
                    }
                    else
                    {
                        ctlPlotter1.Position = 0;
                        ctlPlotter2.Position = 0;
                        scrScrollbar.Value = ctlPlotter1.Position;
                    }
                    break;
            }
        }

        private void scrScrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            ctlPlotter1.Position = scrScrollbar.Value;
            ctlPlotter2.Position = scrScrollbar.Value;
        }

    }
}
