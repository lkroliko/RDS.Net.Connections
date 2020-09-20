using System;
using System.Threading;

namespace RDS.Net.Connections.Readers
{
    public interface IReader
    {
        event EventHandler<ReadedEventArgs> Readed;

        void Start(CancellationToken token);
    }
}