using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Writers
{
    class WriterFactory : IWriterFactory
    {
        public IWriter Get(IConnectionHandler connection)
        {
            return new Writer(connection);
        }
    }
}
