using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
using System;
using System.Threading;

namespace RDS.Net.Connections
{
    public interface IConnection
    {
        bool IsConnected { get; }
        IReader Reader { get; }
        IWriter Writer { get; }
        void Start(CancellationToken token);
        event EventHandler<EventArgs> Connected;
    }
}