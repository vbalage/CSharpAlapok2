using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threading002
{
    public class TaskCancellationTest
    {
        private static bool run = true;

        public static void LongRunning3()
        {
            double d = 0;
            int i = 0;
            for (; run && i < 1000000000; i++)
            {
                d += Math.Sqrt(i);
            }
            Console.WriteLine(i);
        }

        private static void CancellableLongRunning(CancellationToken token, bool throwIfCancelled = false)
        {
            Double d = 0;
            int i = 0;
            for (; i < 1000000000; i++)
            {
                d += Math.Sqrt(i);
                if (throwIfCancelled)
                {
                    // throws exception when cancelled
                    token.ThrowIfCancellationRequested();
                }
                else
                {
                    if (token.IsCancellationRequested)
                        break;
                }
            }
            Console.WriteLine(i);
        }


        public static void Run()
        {

            // Cooperative threading
            Console.WriteLine("***Cooperative threading");
            Task t = new Task((LongRunning3));
            t.Start();
            Thread.Sleep(1500);
            Console.WriteLine("Thread woke up");
            run = false;
            t.Wait();
            Console.WriteLine("Ended");


            // Cancellation token threading
            Console.WriteLine("***Token example");
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;
            Task t2 = new Task(() => CancellableLongRunning(token));
            t2.Start();
            Thread.Sleep(1500);
            Console.WriteLine("Thread woke up");
            cts.Cancel();
            t2.Wait();
            Console.WriteLine(t2.Status);
            Console.WriteLine("Ended");


            // better cancellation
            Console.WriteLine("***Token via Task");
            CancellationTokenSource cts2 = new CancellationTokenSource();
            var token2 = cts2.Token;

            // callbacket lehet megadni mi tortenjen cancelkor
            //cts2.Token.Register();

            // bizonyos ido utan cancel
            //cts2.CancelAfter(200);

            Task t3 = new Task(() => CancellableLongRunning(token), token2);
            t3.Start();
            Thread.Sleep(1500);
            Console.WriteLine("Thread woke up");
            cts2.Cancel(); // indefinite hogy mikor allitja le a szalat
            //t3.Wait();
            //Thread.Sleep(1500);
            Console.WriteLine(t3.Status);
            Console.WriteLine("Ended");


            Console.ReadLine();
        }
    }
}