using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Threading002
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose: \n1. ThreadTest\n2. TimeoutTest\n3. TaskCancellationTest\n4. ParallelProcessingTest");
                var choice = Console.ReadLine();
                switch (int.Parse(choice))
                {   
                    case 1:
                        ThreadTest.Run();
                        break;
                    case 2:
                        TimeoutTest.Run();
                        break;
                    case 3:
                        TaskCancellationTest.Run();
                        break;
                    case 4:
                        ParallelProcessingTest.Run();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}