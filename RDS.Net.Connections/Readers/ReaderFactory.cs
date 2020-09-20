using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Readers
{
    class ReaderFactory : IReaderFactory
    {
        public IReader Get(IConnection connection)
        {
            return new Reader(connection);
        }
    }
}
