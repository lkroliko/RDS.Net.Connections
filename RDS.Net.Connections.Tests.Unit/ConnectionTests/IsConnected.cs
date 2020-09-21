using Moq;
using RDS.Net.Connections.Receivers;
using RDS.Net.Connections.Senders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionTests
{
    [Trait("Category", "Connection")]
    public class IsConnected
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReceiverFactory _receivedFactory = Mock.Of<IReceiverFactory>();
        ISenderFactory _senderFactory = Mock.Of<ISenderFactory>();
        Connection _connection;

        public IsConnected()
        {
            _connection = new Connection(_connectionHandler, _receivedFactory, _senderFactory);
        }

        [Fact]
        public void ItExists()
        {
            var result = _connection.IsConnected;
        }

        [Fact]
        public void ItReturnsTrue()
        {
            Mock.Get(_connectionHandler).Setup(c => c.IsConnected).Returns(true);

            var result = _connection.IsConnected;

            Assert.True(result);
        }

        [Fact]
        public void ItReturnsFalse()
        {
            var result = _connection.IsConnected;

            Assert.False(result);
        }
    }
}
