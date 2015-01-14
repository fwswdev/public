/*
 * Author: 7string
 * This code is for the demonstration of E-Gizmo RFID on C# using observer pattern
 * */

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFID_Console_App
{

    // For observers
    public interface IRfidNotify
    {
        void Notify(byte[] rfidData);
    }



    // EGizmoRfid class
    public class EGizmoRfid
    {
        SerialPort mSerialPort;

        // Constructor
        public EGizmoRfid(string portName,IRfidNotify i)
        {
            RegisterObserver(i);
            mSerialPort = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            mSerialPort.Open();
            mSerialPort.ReadTimeout = 500;
            StartThread();
        }

        // Destructor
        ~EGizmoRfid()
        {
            stopThread();
        }



        // Observer Pattern
        IRfidNotify mIRfidNotify;

        void RegisterObserver(IRfidNotify i)
        {
            mIRfidNotify = i;
        }



        // Thread for catching the data
        Thread mThread;
        bool mIsThreadStopped = true;

        const int BUFFER_SZ = 10;

        private void internalThread()
        {
            while (mIsThreadStopped == false)
            {
                // sample data = 41 7d ff dc e8 e7 1b 87 fb 42
                if (mSerialPort.BytesToRead!=0) // there is data
                {
                    try
                    {
                        byte[] arrByte = new byte[BUFFER_SZ];

                        // get the array
                        for (int ctr = 0; ctr < BUFFER_SZ; ctr++)
                        {
                            arrByte[ctr] = (byte)mSerialPort.ReadByte();
                        }

                        // validation
                        if ((arrByte[0] == 0x41) && (arrByte[9] == 0x42))
                        {
                            mIRfidNotify.Notify(arrByte);
                        }
                    }
                    catch
                    {
                        // TODO: put exception handling here
                    }
                    finally
                    {
                        mSerialPort.DiscardInBuffer();
                    }
                }

            }

            mSerialPort.Dispose();
        }

        void stopThread()
        {
            mIsThreadStopped = true;
        }

        void StartThread()
        {
            mThread = new Thread(internalThread);
            mThread.IsBackground = true;
            mThread.Start();
            mIsThreadStopped = false;
        }

    }
}
