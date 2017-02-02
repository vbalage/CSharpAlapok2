using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threading002
{
    public class TimeoutTest
    {
        public static void Delay(int ms)
        {
            Console.WriteLine("Delay started at: " + DateTime.Now.Ticks);
            Thread.Sleep(ms);
        }

        public static void LongRunning2(int ms)
        {
            Console.WriteLine("LongRunning2 started at: " + DateTime.Now.Ticks);
            Thread.Sleep(ms);
        }

        public static void Run()
        {
            Console.WriteLine("Timeout test started");
            Task tActual = new Task(() => LongRunning2(2000));
            tActual.Start();

            Task tDelay = new Task(() => Delay(1000));
            tDelay.Start();

            Task timeoutTask = Task.WhenAny(tActual, tDelay).ContinueWith(completedTask =>
            {
                if (completedTask.Result == tDelay)
                {
                    Console.WriteLine("Timeout!");
                    // cancel if cancellable
                }
                else
                {
                    Console.WriteLine("Completed!");
                }
            });

            Task.WaitAny(tActual, tDelay);

            int maxt = 0;
            int completionport = 0;

            ThreadPool.GetMaxThreads(out maxt, out completionport);
            Console.WriteLine($"Max: {maxt}, Completionport: {completionport}");

            Console.WriteLine("Timeout test ended.");
            Console.ReadLine();
        }
    }
}