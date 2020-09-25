using Moq;
using RDS.Net.Connections.Senders;
using RDS.Net.Connections.Receivers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RDS.Net.Connections.Abstractions;

namespace RDS.Net.Connections.Tests.Unit.ConnectionTests
{
    [Trait("Category", "Connection")]
    public class Class
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReceiverFactory _receiverFactory = Mock.Of<IReceiverFactory>();
        ISenderFactory _senderFactory = Mock.Of<ISenderFactory>();

        [Fact]
        public void ItExists()
        {
            new Connection(_connectionHandler, _receiverFactory, _senderFactory);
        }

        [Fact]
        public void ItImplementIConnectionManager()
        {
            Assert.IsAssignableFrom<IConnection>(new Connection(_connectionHandler, _receiverFactory, _senderFactory));
        }
    }
}
