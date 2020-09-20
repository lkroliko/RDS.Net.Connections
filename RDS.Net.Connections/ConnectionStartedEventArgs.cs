using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDS.Net.Connections
{
    class ConnectionStartedEventArgs :EventArgs
    {
        public CancellationToken Token { get; }

        public ConnectionStartedEventArgs(CancellationToken token)
        {
            Token = token;
        }
    }
}
