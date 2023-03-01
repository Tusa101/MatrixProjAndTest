using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace MatrixProjAndTest
{
    public class Randomizer
    {
        private static int seed = Environment.TickCount;
        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(()=>new Random(Interlocked.Increment(ref seed)));
        public static Random GetThreadRandom()
        {
            return randomWrapper.Value;
        }
    }
}
