using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedApi
{
    class Program
    {
        private static long x;

        static void Main(string[] args)
        {
            // x = x + 8;
            Interlocked.Add(ref x, 8);

            //  int oldValue = x; x=15 (Volatile.Write(ref x, 15)); return oldValue
            Interlocked.Exchange(ref x, 15);

            // int oldValue = x (Volatile.Read); if(x == 0) x=15(Volatile.Write); return oldValue;
            Interlocked.CompareExchange(ref x, 15, 0);
        }


        // Interlocked anything
        public static int InterlockedMultiply(ref int target, int multiplier)
        {
            int currentVal = target;
            int startVal;
            int desiredVal;

            do
            {
                startVal = currentVal;
                desiredVal = startVal * multiplier;

                // int oldValue = target (Volatile.Read); if(x == startVal) x=desiredVal (Volatile.Write); return oldValue;
                currentVal = Interlocked.CompareExchange(ref target, desiredVal, startVal);
            } while (startVal != currentVal);

            return desiredVal;
        }
    }
}
