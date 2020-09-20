using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
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
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        IWriter _writer = Mock.Of<IWriter>();
        Connection _connection;
        CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public Start()
        {
            _connection = new Connection(_connectionHandler, _readerFactory, _writerFactory);
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
