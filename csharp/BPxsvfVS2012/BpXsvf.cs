//https://github.com/jevinskie/bus-pirate/blob/master/scripts/BPXSVFPlayer/main.c

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.IO;

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
        protected IOutDisplay mIoutdisplay;
        //protected BpXsvfParameterData mParamData;

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


        //public void SetBpXsvfParameterData(BpXsvfParameterData paramData)
        //{
        //    mParamData = paramData;

        //}

        public abstract void DoProcess(BpXsvfParameterData p);

    }




    public class BpXsvfFork:AbstractBpXsvf
    {
        delegate void SerialPortDelegateWriteOneByte(byte data);

        public override void DoProcess(BpXsvfParameterData p)
        {
            var ioutdisp = mIoutdisplay;
            var paramdata = p;
            SerialPort serialport;
            var filePath=paramdata.XsvfFilePath;

            const int BUFF_SZ = 256;

            int res = 0;

            byte[] byteRead = null;

            if(String.IsNullOrEmpty(filePath)==false)
            {
                filePath=filePath.Trim();
                if(File.Exists(filePath)== true )
                {
                    // File process
                    byteRead = File.ReadAllBytes(filePath);
                    ioutdisp.Display("Number of bytes: " + byteRead.Length);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;

            }

            ioutdisp.Display("Opening COM");
            serialport = new SerialPort(paramdata.ComName, paramdata.ComBaudRate, Parity.None, 8, StopBits.One);


            try 
	        {
                serialport.ReadTimeout = 1000;

                if (serialport.IsOpen == false)
                    serialport.Open();

                Thread.Sleep(100);

		        SerialPortDelegateWriteOneByte serPortWriteByteDelegate = delegate(byte data)
                    {
                        serialport.DiscardInBuffer();
                        serialport.DiscardOutBuffer();
                        serialport.Write(new byte[] { data }, 0, 1);
                    };


                if (paramdata.ResetJtag)
                {
                    ioutdisp.Display("Reset");
                    serPortWriteByteDelegate(AbstractBpXsvf.JTAG_RESET);
                    Thread.Sleep(100);
                }

                 //ChainScan TODO
                if (paramdata.ChainScan)
                {
                    var chainreadbuffer = new byte[BUFF_SZ];
                    ioutdisp.Display("Chain Scan");
                    serPortWriteByteDelegate(AbstractBpXsvf.JTAG_CHAIN_SCAN);
                    res = serialport.Read(chainreadbuffer, 0, BUFF_SZ);
                    if (res != 0)
                    {
                        ioutdisp.Display("Got Reply");
                        for (int ctr = 0; ctr < res; ctr++)
                        {
                            ioutdisp.Display(String.Format("{0:d}", chainreadbuffer[ctr]));
                        }

                    }
                    else
                    {
                        ioutdisp.Display("No Reply");
                    }
                    Thread.Sleep(100);
                }

                //return;

                // XSVF Process
                ioutdisp.Display("Entering XSVF player mode");
                serPortWriteByteDelegate(AbstractBpXsvf.XSVF_PLAYER);

                Thread.Sleep(100);

                

                byte [] readbuffer=new byte[BUFF_SZ + 50];
                

                int fileSize=byteRead.Length;
                int readSize=BUFF_SZ;
                int bytePointer=0;
                int cnt = 0;

                while(true)
                {
                    try
                    {
                        res = serialport.Read(readbuffer, 0, BUFF_SZ);
                    }
                    catch(TimeoutException e)
                    {
                        ioutdisp.Display("Timeout! ");
                        return;
                    }
                    
                    if(res>0)
                    {
                        if(readbuffer[0]!=AbstractBpXsvf.XSVF_READY_FOR_DATA)
                        {
                            ioutdisp.Display("Error! Code: " + (int)readbuffer[0]);
                        }
                        else
                        {
                            if (fileSize == 0)
                                break;

                            if(fileSize < BUFF_SZ)
                            {
                                readSize = fileSize;
                            }

                            byte[] tempSend = new byte[2];
                            tempSend[0] = (byte)(readSize >> 8);
                            tempSend[1] = (byte)(readSize);
                            cnt += readSize;
                            //ioutdisp.Display(String.Format("Sending {0:d} Bytes {1:x04}", readSize, cnt));
                            ioutdisp.Display(String.Format("Sending {0:d} Bytes {1:d}", readSize, cnt));

                            Thread.Sleep(5);
                            serialport.DiscardInBuffer();
                            serialport.Write(tempSend, 0, 2);
                            serialport.Write(byteRead, bytePointer, readSize);

                            bytePointer += readSize;
                            fileSize -= readSize;

                            //Thread.Sleep(2000);

                        }




                    }
                    else
                    {
                        break;
                    }

                }
                ioutdisp.Display(" Thank you for playing!");


	        }
	        finally
	        {
                if (serialport.IsOpen)
                    serialport.Close();

                serialport.Dispose();
                 
	        }

        }



    }

}
