using Moq;
using RDS.Net.Connections.Receivers;
using RDS.Net.Connections.Senders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionManagerTests
{
    [Trait("Category", "Connection")]
    public class Writer
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReceiverFactory _receiverFactory = Mock.Of<IReceiverFactory>();
        ISenderFactory _senderFactory = Mock.Of<ISenderFactory>();
        ISender _sender = Mock.Of<ISender>();
        Connection _connection;

        public Writer()
        {
            Mock.Get(_senderFactory).Setup(f => f.Get(_connectionHandler)).Returns(_sender);
            _connection = new Connection(_connectionHandler, _receiverFactory, _senderFactory);
        }

        [Fact]
        public void ItExists()
        {
            var reader = _connection.Sender;
        }

        [Fact]
        public void ItReturnsReader()
        {
            var result = _connection.Sender;

            Assert.Same(_sender, result);
        }

        [Fact]
        public void ItCallReaderFactory()
        {
            var result = _connection.Sender;
            result = _connection.Sender;
            result = _connection.Sender;

            Mock.Get(_senderFactory).Verify(f => f.Get(_connectionHandler), Times.Once);
        }
    }
}
