using RDS.Net.Connections.Senders;
using RDS.Net.Connections.Receivers;
using System;
using System.Threading;

namespace RDS.Net.Connections
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