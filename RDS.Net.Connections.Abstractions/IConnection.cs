using System;
using System.Threading;

namespace RDS.Net.Connections.Abstractions
{
    public interface IConnection
    {
        bool IsConnected { get; }
        IReceiver Receiver { get; }
        ISender Sender { get; }
        void Start(CancellationToken token);
        event EventHandler<EventArgs> Connected;
    }
}