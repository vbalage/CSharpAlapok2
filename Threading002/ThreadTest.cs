using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threading002
{
    public class ThreadTest
    {
        public static string LongRunning(double id, int sleep, bool throwEx = false)
        {
            Console.WriteLine($"LongRunning{id} started");
            Thread.Sleep(sleep);
            Console.WriteLine($"LongRunning{id} returning");
            if (throwEx)
                throw new Exception($"Async exception in task {id}!");

            var res = new Random().NextDouble();
            return $"TaskId: {id}, Runtime: {sleep}ms, Result: {res}";
        }

        public static void Run()
        {
            // WaitAll
            Console.WriteLine("***WaitAll");
            double x = 10;
            Task<string> t = new Task<string>(() => LongRunning(1, 1000));
            Task<string> t2 = new Task<string>(() => LongRunning(2, 2000));
            t.Start();
            t2.Start();
            Task.WaitAll(t, t2); // bevarja az osszes taskot
            Console.WriteLine($"All done. {t.Result}, {t2.Result}");

            // WaitAny
            Console.WriteLine("***WaitAny");
            Task<string> t3 = new Task<string>((() => LongRunning(3, 1000)));
            Task<string> t4 = new Task<string>((() => LongRunning(4, 2000)));
            t3.Start();
            t4.Start();
            int doneId = Task.WaitAny(t3, t4); // az argumentben megadott task poziciojat adja vissza
            Console.WriteLine($"done first: {doneId}");

            // WhenAll
            Console.WriteLine("***WhenAll");
            Task<string> t5 = new Task<string>((() => LongRunning(5, 1000)));
            Task<string> t6 = new Task<string>((() => LongRunning(6, 2000)));
            t5.Start();
            t6.Start();
            var parentTask = Task.WhenAll(t5, t6);
            parentTask.Wait();
            Console.WriteLine("All results:" + string.Join(",", parentTask.Result));

            // WhenAny
            Console.WriteLine("***WhenAny");
            Task<string> t7 = new Task<string>((() => LongRunning(7, 1000)));
            Task<string> t8 = new Task<string>((() => LongRunning(8, 2000)));
            t7.Start();
            t8.Start();
            var task = Task.WhenAny(t7, t8);
            Console.WriteLine("First result: " + task.Result.Result + " Status:" + task.Status);

            // ContinueWith
            Console.WriteLine("***ContinueWith");
            Task<string> t9 = new Task<string>(() => LongRunning(9, 1000, true));

            t9.ContinueWith(
                completedTask =>
                {
                    Console.WriteLine("Continued after " + completedTask.Result + ", Status:" + completedTask.Status);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            t9.ContinueWith(completedTask =>
            {
                Console.WriteLine($"Gebasz van! {completedTask.Exception}");
            }, TaskContinuationOptions.OnlyOnFaulted);

            t9.Start();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Console.WriteLine("Doing stuff in the meantime...");
            }

            // WhenAny hasznalat
            // Timeout-ra jol hasznalhato ()
            Console.WriteLine("***WhenAny hasznalata");
            Task<string> t10 = new Task<string>(() => LongRunning(10, 1000));
            Task<string> t11 = new Task<string>(() => LongRunning(11, 1000));
            t10.Start();
            t11.Start();
            var combinedTask = Task.WhenAny(t10, t11).ContinueWith(combined =>
            {
                Console.WriteLine("Faster task: " + combined.Result.Result);
            });

            // TaskFactory
            Console.WriteLine("***TaskFactory");
            var tf = new TaskFactory().StartNew(() => LongRunning(12, 1000));


            Console.WriteLine("Program ended...");
            Console.ReadLine();
        }
    }
}