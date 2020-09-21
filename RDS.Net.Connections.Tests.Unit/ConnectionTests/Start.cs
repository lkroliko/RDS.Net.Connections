using Moq;
using RDS.Net.Connections.Senders;
using RDS.Net.Connections.Receivers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionTests
{
    [Trait("Category", "Connection")]
    public class Start
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReceiverFactory _receiverFactory = Mock.Of<IReceiverFactory>();
        ISenderFactory _senderFactory = Mock.Of<ISenderFactory>();
        ISender _sender = Mock.Of<ISender>();
        Connection _connection;
        CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public Start()
        {
            _connection = new Connection(_connectionHandler, _receiverFactory, _senderFactory);
        }

        [Fact]
        public void ItExists()
        {
            _connection.Start(_tokenSource.Token);
        }

        [Fact]
        public void WhenCalledThenStartedEventRaised()
        {
            int calledCount = 0;
            _connection.Started += (sender, args) => { calledCount++; };

            _connection.Start(_tokenSource.Token);

            Assert.Equal(1, calledCount);
        }

        [Fact]
        public void WhenCalledThenStartedEventWithToken()
        {
            CancellationToken token;
            _connection.Started += (sender, args) => { token = args.Token; };

            _connection.Start(_tokenSource.Token);

            Assert.Equal(_tokenSource.Token, token);
        }
    }
}
