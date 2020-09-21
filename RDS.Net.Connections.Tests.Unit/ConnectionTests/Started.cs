using Moq;
using RDS.Net.Connections.Receivers;
using RDS.Net.Connections.Senders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionTests
{
    [Trait("Category", "Connection")]
    public class Started
    {

        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReceiverFactory _receiverFactory = Mock.Of<IReceiverFactory>();
        ISenderFactory _senderFactory = Mock.Of<ISenderFactory>();
        Connection _connection;
        CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public Started()
        {
            _connection = new Connection(_connectionHandler, _receiverFactory, _senderFactory);
        }

        [Fact]
        public void ItExists()
        {
            _connection.Started += delegate { };
        }
    }
}
