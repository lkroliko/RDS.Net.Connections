using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
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
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        Connection _connection;

        public IsConnected()
        {
            _connection = new Connection(_connectionHandler, _readerFactory, _writerFactory);
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
