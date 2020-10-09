using RDS.Logging;
using RDS.Net.Connections.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Senders
{
    class SenderFactory : ISenderFactory
    {
        ILogger _logger;

        public SenderFactory(ILogger logger)
        {
            _logger = logger;
        }

        public ISender Get(IConnectionHandler connection)
        {
            return new Sender(connection, _logger);
        }
    }
}
