/*
Sevenstring7.blogspot.com
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KstFileGen
{
    public partial class MainForm : Form
    {

        IKstFileGen mIKstFileGen;

        public MainForm()
        {
            const string FILENAME = "c:\\filekst.txt";
            InitializeComponent();
            mIKstFileGen = new KstFileGenerator(FILENAME);
        }

        private void btnGenerateFile_Click(object sender, EventArgs e)
        {
            mIKstFileGen.CreateDummyIncrementalData();
        }

        private void btnRandomData_Click(object sender, EventArgs e)
        {
            mIKstFileGen.CreateDummyRandomData();
        }

        private void btnStopContinuousStream_Click(object sender, EventArgs e)
        {
            mIKstFileGen.StopContinuousDataStream();
        }

        private void btnRunContinuousStream_Click(object sender, EventArgs e)
        {
            mIKstFileGen.RunContinousDataStream("c:\\ksttream.txt");
        }

        private void btnSinewaveData_Click(object sender, EventArgs e)
        {
            mIKstFileGen.CreateSinewaveData((double)numUpDownPeriod.Value);
        }
    }
}
