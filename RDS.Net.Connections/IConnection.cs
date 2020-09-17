using System;
using RDS.Net.Connections.Wrappers;

namespace RDS.Net.Connections
{
    public interface IConnection
    {
        IStreamReader GetStreamReader();
        IStreamWriter GetStreamWriter();
        event EventHandler<EventArgs> Connected;
        void Connect();
        bool IsConnected { get; }
    }
}