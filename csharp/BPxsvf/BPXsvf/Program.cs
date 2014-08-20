using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace BPXsvf
{
    class Program : IOutDisplay
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.MainProcess();
        }

        public void MainProcess()
        {
            BpXsvfParameterData bpParamData = new BpXsvfParameterData();
            bpParamData.ChainScan = true;
            bpParamData.ComBaudRate = 115200;
            bpParamData.ComName = "COM1";
            bpParamData.ResetJtag = true;
            bpParamData.XsvfFilePath = "c:\\out.txt";

            //SerialPort ser = new SerialPort();

            AbstractBpXsvf iXsvf = new BpXsvfFork();
            //iXsvf.SetBpXsvfParameterData(bpParamData);
            //iXsvf.SetSerialPort(ser);
            iXsvf.SetOutDisplay(this);
            iXsvf.Greet();
            iXsvf.DoProcess(bpParamData);

            

            Console.ReadKey();
        }

        // IOutDisplay interface
        public void Display(string str)
        {
            Console.WriteLine(str);
        }
    }
}
