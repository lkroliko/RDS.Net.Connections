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
    public class Reader
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        ISenderFactory _senderFactory = Mock.Of<ISenderFactory>();
        IReceiverFactory _receiverFactory = Mock.Of<IReceiverFactory>();
        IReceiver _receiver = Mock.Of<IReceiver>();
        Connection _connection;

        public Reader()
        {
            Mock.Get(_receiverFactory).Setup(f => f.Get(_connectionHandler)).Returns(_receiver);
            _connection = new Connection(_connectionHandler, _receiverFactory, _senderFactory);
        }

        [Fact]
        public void ItExists()
        {
            var reader = _connection.Receiver;
        }

        [Fact]
        public void ItReturnsReader()
        {
            var result = _connection.Receiver;

            Assert.Same(_receiver, result);
        }

        [Fact]
        public void ItCallReaderFactory()
        {
            var result = _connection.Receiver;
            result = _connection.Receiver;
            result = _connection.Receiver;

            Mock.Get(_receiverFactory).Verify(f => f.Get(_connectionHandler), Times.Once);
        }
    }
}
