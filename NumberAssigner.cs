using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Test
{
    public static class NumberAssigner
    {
        static int _nextNumber = 0;

        public static int GetNextNumber()
        {
            return ++_nextNumber;
        }
    }
}
