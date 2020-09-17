using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDS.Net.Connections.Wrappers
{
    internal class ThreadWrapper : IThread
    {
        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }
    }
}
