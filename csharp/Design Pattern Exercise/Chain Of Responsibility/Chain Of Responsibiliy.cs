/* =======================================================================
Chain Of Responsibility Pattern in C#
Version by 7string

- The Abstract class handles the if statement. I would like
to avoid having duplicated codes of if statements in the
classes that will inherit/implement the Handler abstract class
- Protected override void was implemented.
======================================================================= */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler h1 = new Add();
            Handler h2 = new Subtract();

            h1.ChangeOperationType(OperationType.Add);
            h2.ChangeOperationType(OperationType.Subtract);

            h1.SetNextHandler(h2);

            // Now Perform Operations

            h1.PerformOperation(10, 4, OperationType.Add);
            h1.PerformOperation(10, 4, OperationType.Subtract);
        }
    }



    public enum OperationType
    {
        Undefined,
        Add,
        Subtract
    }



    public abstract class Handler
    {
        private Handler nextHandler;
        private OperationType mOperationType=OperationType.Undefined;


        public void SetNextHandler(Handler h)
        {
            nextHandler = h;
        }


        public void PerformOperation(int x, int y, OperationType t)
        {
            if (t == mOperationType)
            {
                PerformOperationOnHandler(x, y);
            }
            else
            {
                nextHandler.PerformOperation(x, y, t);
            }
        }

        
        public void ChangeOperationType(OperationType o)
        {
            // we make sure this can be initiated once. We can put this into the constructor but this will be simpler
            if (mOperationType == OperationType.Undefined) 
            {
                mOperationType = o;
            }
        }

        protected abstract void PerformOperationOnHandler(int x, int y);
    }


    public class Add : Handler
    {
        protected override void PerformOperationOnHandler(int x, int y)
        {
            Console.WriteLine("Add " + (x+y));
        }
    }


    public class Subtract : Handler
    {
        protected override void PerformOperationOnHandler(int x, int y)
        {
            Console.WriteLine("Subtract " + (x-y));
        }
    }


}
