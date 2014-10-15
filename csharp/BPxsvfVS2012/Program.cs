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
            bpParamData.ChainScan = false;
            bpParamData.ComBaudRate = 115200;
            bpParamData.ComName = "COM4";
            bpParamData.ResetJtag = true;
            //bpParamData.XsvfFilePath = @"C:\Temp\bpxsvf\a.xsvf";
            bpParamData.XsvfFilePath = @"C:\Temp\iselight\lightled\default.xsvf";
            //bpParamData.XsvfFilePath = @"C:\Temp\asd\asdd\a.xsvf";
            

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
