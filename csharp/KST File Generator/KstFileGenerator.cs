/*
Sevenstring7.blogspot.com
Still in progress
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace KstFileGen
{
    public interface IKstFileGen
    {
        void CreateDummyIncrementalData();
        void CreateDummyRandomData();
        void CreateSinewaveData(double period);
        void RunContinousDataStream(string filenameCont);
        void StopContinuousDataStream();
    }

    public class KstFileGenerator : IKstFileGen
    {
        string mFileName;

        public KstFileGenerator(string filename)
        {
            mFileName = filename;
        }

        public void CreateDummyIncrementalData()
        {
            using(var sw = File.CreateText(mFileName))
            {
                sw.WriteLine("First Line");

                for(int ctr=0;ctr<100;ctr++)
                {
                sw.WriteLine(ctr.ToString());
                }
            }
        }



        public void CreateDummyRandomData()
        {
            Random r= new Random();
            using (var sw = File.CreateText(mFileName))
            {
                sw.WriteLine("X,Y");

                for (int ctr = 0; ctr < 100; ctr++)
                {
                    sw.WriteLine(String.Format ("{0},{1}",ctr,r.NextDouble()*100)  );
                }
            }
        }


        //public void CreateSinewaveData()
        //{
        //    Random r = new Random();
        //    using (var sw = File.CreateText(mFileName))
        //    {
        //        sw.WriteLine("Y");

        //        const double PERIOD = 0.5;
        //        //const double PERIOD = 0.1;

        //        int NUM_SAMPLES = 1000;

        //        for (int ctr = 0; ctr < NUM_SAMPLES * 1; ctr++)
        //        {
        //            double xdata = (PERIOD / NUM_SAMPLES) * ctr;
        //            double data = 5.0 * Math.Sin((2.0 * Math.PI * ctr) / NUM_SAMPLES);
        //            //data += 2.0 * r.NextDouble();
        //            //sw.WriteLine(String.Format("{0},{1}", xdata, data));
        //            //sw.WriteLine(String.Format("{0},{1}", ctr, data));
        //            sw.WriteLine(String.Format("{0}",data));
        //        }
        //    }
        //}



        public void CreateSinewaveData(double period)
        {
            Random r = new Random();
            using (var sw = File.CreateText(mFileName))
            {
                sw.WriteLine("X,MyTime,Y");

                double PERIOD = period;

                int NUM_SAMPLES = 1000;

                for (int ctr = 0; ctr <= NUM_SAMPLES; ctr++)
                {
                    double xdata = (PERIOD / NUM_SAMPLES) * ctr;
                    double data = 1000.0 * Math.Sin((2.0 * Math.PI * ctr) / NUM_SAMPLES);
                    sw.WriteLine(String.Format("{0},{1},{2}", ctr,xdata, data));
                }
            }
        }



        bool mbThreadRun;


        public void RunContinousDataStream(string filenameCont)
        {
            string fileNameStream;
            fileNameStream = filenameCont;

            if (mbThreadRun == true) return;

            //IDataContinuousStream iDataContinuousStream = new RandomDataStream();
            IDataContinuousStream iDataContinuousStream = new SineWaveData();

            Thread t = new Thread(delegate()
                {
                    mbThreadRun = true;
                    using (var sw = File.CreateText(fileNameStream))
                    {
                        Random r = new Random();
                        sw.WriteLine("X,Y");
                        int ctr = 0;
                        while (mbThreadRun)
                        {
                            sw.WriteLine(String.Format("{0},{1}", ctr, iDataContinuousStream.GetData() ));
                            sw.Flush();
                            ctr++;
                            Thread.Sleep(50);
                        }
                    }

                });
            t.IsBackground = true;
            t.Start();

        }

        public void StopContinuousDataStream()
        {
            mbThreadRun = false;
        }

    }




    public interface IDataContinuousStream
    {
        double GetData();
    }


    public class RandomDataStream : IDataContinuousStream
    {
        Random r = new Random();

        public double GetData()
        {
            return r.NextDouble() * 100;
        }
    }


    public class SineWaveData : IDataContinuousStream
    {
        uint mCtr =0;
        const double TWOBYMATHPI = 2.0 * Math.PI;

        public double GetData()
        {
            double data =  3.0 * Math.Sin (Math.PI * (mCtr / 6.0) );
            mCtr++;
            return data;
        }
    }
}
