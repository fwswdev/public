using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace BPXsvf
{

    public interface IOutDisplay
    {
        void Display(string str);

    }


    public class BpXsvfParameterData
    {
        public int ComBaudRate { get; set; }
        public string ComName { get; set; }
        public bool ResetJtag { get; set; }
        public bool ChainScan { get; set; }
        public string XsvfFilePath { get; set; }
    }
    

    public abstract class AbstractBpXsvf
    {
        public const int JTAG_RESET= 0x01;
        public const int JTAG_CHAIN_SCAN= 0x02;
        public const int XSVF_PLAYER= 0x03;


        public const int XSVF_ERROR_NONE= 0x00;
        public const int XSVF_ERROR_UNKNOWN= 0x01;
        public const int XSVF_ERROR_TDOMISMATCH= 0x02;
        public const int XSVF_ERROR_MAXRETRIES= 0x03;
        public const int XSVF_ERROR_ILLEGALCMD= 0x04;
        public const int XSVF_ERROR_ILLEGALSTATE= 0x05;
        public const int XSVF_ERROR_DATAOVERFLOW= 0x06;
        public const int XSVF_ERROR_LAST= 0x07;
        public const int XSVF_READY_FOR_DATA = 0xFF;


        //protected SerialPort mSerial;
        IOutDisplay mIoutdisplay;
        protected BpXsvfParameterData mParamData;

        //public void SetSerialPort(SerialPort s)
        //{
        //    mSerial = s;
        //}

        public void SetOutDisplay(IOutDisplay i)
        {

            mIoutdisplay = i;
        }

        public void Greet()
        {
            mIoutdisplay.Display("BP XSVF in C# by 7");
        }


        public void SetBpXsvfParameterData(BpXsvfParameterData paramData)
        {
            mParamData = paramData;

        }

        public abstract void DoProcess();

    }




    public class BpXsvfFork:AbstractBpXsvf
    {

        public override void DoProcess()
        {
            BpXsvfParameterData paramdata = mParamData;
            SerialPort serialport = new SerialPort(paramdata.ComName,paramdata.ComBaudRate,Parity.None,8,StopBits.One);

            


        }



    }

}
