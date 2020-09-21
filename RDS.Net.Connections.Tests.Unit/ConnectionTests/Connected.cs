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
    public class Connected
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReceiverFactory _receiverFactory = Mock.Of<IReceiverFactory>();
        ISenderFactory _senderFactory = Mock.Of<ISenderFactory>();
        Connection _connection;

        public Connected()
        {
            _connection = new Connection(_connectionHandler, _receiverFactory, _senderFactory);
        }

        [Fact]
        public void ItExists()
        {
           _connection.Connected += delegate { };
        }

        [Fact]
        public void WhenConnectionHandlerConnectedRaisedThenEventConnectedCalled()
        {
            int calledCount = 0;
            _connection.Connected += (sender, args) => { calledCount++; };

            Mock.Get(_connectionHandler).Raise(c => c.Connected += null, new EventArgs());

            Assert.Equal(1, calledCount);
        }
    }
}
