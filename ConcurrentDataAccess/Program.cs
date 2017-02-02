using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentDataAccess
{
    class Program
    {
        // volatile miatt a foridto nem optimalizal, nem mozgatja el a helyerol, emiatt lefut release modban is a main method
        static volatile bool stopWorker = false;

        static void Worker()
        {
            int x = 0;
            while (!stopWorker)
            {
                x++;
            }
            Console.WriteLine($"Worker stopped at: {x}");
        }

        class M
        {
            private bool flag = false;
            private int value = 0;

            public void M1()
            {
                value = 5;
                flag = true;
            }

            public void M2()
            {
                if (flag)
                {
                    Console.WriteLine(value);
                }
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Starting worker");
            Task t = new Task(Worker);
            t.Start();
            Thread.Sleep(5000);
            stopWorker = true;
            Console.WriteLine("Stop signal sent...");
            t.Wait();
            Console.WriteLine("Task finished");
            Console.ReadLine();

        }
    }
}
