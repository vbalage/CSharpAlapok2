using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading001
{
    class Data
    {
        public double Result { get; set; }
    }

    class Program
    {
        static void GenerateExtreme(object x)
        {
            Console.WriteLine("GenerateExtreme started...");
            double d = 0;
            for (int i = 0; i < 900000000; i++)
            {
                d += Math.Sqrt(i);
            }
            ((Data)x).Result = d;
            Console.WriteLine("GenerateExtreme done...");
        }

        static double GenerateExtreme2()
        {
            Console.WriteLine("GenerateExtreme2 started...");
            double d = 0;
            for (int i = 0; i < 900000000; i++)
            {
                d += Math.Sqrt(i);
            }
            
            Console.WriteLine("GenerateExtreme2 done...");
            return d;
        }

        private static void Main(string[] args)
        {

            Console.WriteLine("Starting...");

            #region Thread

            var d = new Data();

            Thread t = new Thread(new ThreadStart((() => GenerateExtreme(d))));

            t.Start();

            t.Join();

            Console.WriteLine(d.Result);

            //GenerateExtreme();

            Console.WriteLine("Done. Press a key...");

            Console.ReadLine();

            #endregion

            #region Threadpooling

            Console.WriteLine("Treadpooling...");

            Data d2 = new Data();

            ThreadPool.QueueUserWorkItem(GenerateExtreme, d2);

            Console.WriteLine("Threadppoling done...");

            #endregion

            #region Task

            // threadpoolt hasznal belul

            Task<double> t2 = new Task<double>(GenerateExtreme2);
            t2.Start();
            Console.WriteLine(t2.Status);
            Console.WriteLine(t2.Result);
            
            #endregion

            Console.ReadLine();
        }
    }
}
