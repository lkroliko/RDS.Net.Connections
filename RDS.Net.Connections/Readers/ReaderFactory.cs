using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Readers
{
    class ReaderFactory : IReaderFactory
    {
        public IReader Get(IConnectionHandler connection)
        {
            return new Reader(connection);
        }
    }
}
