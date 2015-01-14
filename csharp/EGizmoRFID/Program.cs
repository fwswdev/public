/*
 * Author: 7string
 * This code is for the demonstration of E-Gizmo RFID on C# using observer pattern
 * 
 * Note: Please change the    public const string COM_NAME = "COM4"    to the appropriate COM # on your PC
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFID_Console_App
{
    class Program : IRfidNotify
    {
        public const string COM_NAME = "COM4";

        static void Main(string[] args)
        {
            Program p = new Program();
            p.mainApp();
        }


        void mainApp()
        {
            EGizmoRfid egizmoRfid = new EGizmoRfid(COM_NAME,this);
            Console.WriteLine("Please Tap RFID...");
            Thread.Sleep(10000);
        }


        public void Notify(byte[] rfidData)
        {
            Console.WriteLine("RFID Detected!");
            foreach(byte b in rfidData)
                Console.Write(((int)b).ToString("X2")+ " ");
            Console.WriteLine(" ");
        }
    }
}
