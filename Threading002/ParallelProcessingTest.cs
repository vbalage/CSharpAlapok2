using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading002
{

    internal class ParallelProcessingTest
    {
        static long CalculateDirectorySize(string path, bool parallel = true)
        {
            long total = 0;
            object _lock = new object();

            IEnumerable<string> files;
            files = Directory.EnumerateFiles(path);

            if (parallel)
            {
                Parallel.ForEach(files,
                    () => 0L,
                    (file, _, __, taskTotal) =>
                    {
                        using (var fs = File.OpenRead(file))
                        {
                            return taskTotal + fs.Length;
                        }
                    },
                    taskTotal => Interlocked.Add(ref total, taskTotal)
                );
            }
            else
            {
                foreach (var file in files)
                {
                    using (var fs = File.OpenRead(file))
                    {
                        total += fs.Length;
                    }
                }
            }

            return total;
        }

        public static void Run()
        {
            var dir = @"C:\Jozsi Pendrive\Configurations\130504003 FPT-5 EDO\Configuration";
            for (int i = 0; i < 1; i++)
            {
                Stopwatch w = new Stopwatch();
                w.Start();
                var a = CalculateDirectorySize(dir, true);
                w.Stop();

                Console.WriteLine($"Done. Total: {a} bytes, time: {w.Elapsed}");
            }
        }

    }
}
