using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Senders
{
    class SenderFactory : ISenderFactory
    {
        public ISender Get(IConnectionHandler connection)
        {
            return new Sender(connection);
        }
    }
}
