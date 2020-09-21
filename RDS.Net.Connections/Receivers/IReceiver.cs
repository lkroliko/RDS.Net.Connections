using System;
using System.Threading;

namespace RDS.Net.Connections.Receivers
{
    public interface IReceiver
    {
        event EventHandler<ReceivedEventArgs> Received;

        void Start(CancellationToken token);
    }
}