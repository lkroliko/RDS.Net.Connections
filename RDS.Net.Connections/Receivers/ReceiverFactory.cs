using RDS.Logging;
using RDS.Net.Connections.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Receivers
{
    class ReceiverFactory : IReceiverFactory
    {
        ILogger _logger;

        public ReceiverFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IReceiver Get(IConnectionHandler connection)
        {
            return new Receiver(connection, _logger);
        }
    }
}
