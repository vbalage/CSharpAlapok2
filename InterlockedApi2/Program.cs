using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedApi2
{

    // ReaderWriterLockSlim l2 = new ReaderWriterLockSlim(); 
    // kulon kezeli az iro es olvaso 
    
    // reset, semaphore, mutex, slim verziok, etc.
    // user mode es kernel mode lockok 


    public class SpinLock
    {
        private int inUse;

        public void Enter()
        {
            uint waitedLoopCount = 0;

            while (true)
            {
                // int oldValue = inUse; inUse = 1; return oldValue
                if (Interlocked.Exchange(ref inUse, 1) == 0)
                {
                    Console.WriteLine("Looped " + waitedLoopCount + " times");
                    return;
                    

                }
                waitedLoopCount++;
            }
        }

        public void Exit()
        {
            Volatile.Write(ref inUse, 0);
        }
    }


    class Program
    {
        static SpinLock l = new SpinLock();
        static void Main(string[] args)
        {
            int i = 50;
            Task[] tasks = new Task[i];



            for (int i = 0; i < 50; i++)
            {
                Task.Run(() => SuperTask());
            }
            Console.ReadLine();
        }

        public static void SuperTask()
        {
            l.Enter();
            //Thread.Sleep(1);
            
            l.Exit();

        }
    }
}
