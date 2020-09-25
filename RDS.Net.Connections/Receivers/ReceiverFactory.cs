using RDS.Net.Connections.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Receivers
{
    class ReceiverFactory : IReceiverFactory
    {
        public IReceiver Get(IConnectionHandler connection)
        {
            return new Receiver(connection);
        }
    }
}
