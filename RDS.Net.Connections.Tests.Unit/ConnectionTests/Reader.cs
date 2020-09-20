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
    public class Reader
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        IReader _reader = Mock.Of<IReader>();
        Connection _connection;

        public Reader()
        {
            Mock.Get(_readerFactory).Setup(f => f.Get(_connectionHandler)).Returns(_reader);
            _connection = new Connection(_connectionHandler, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItExists()
        {
            var reader = _connection.Reader;
        }

        [Fact]
        public void ItReturnsReader()
        {
            var result = _connection.Reader;

            Assert.Same(_reader, result);
        }

        [Fact]
        public void ItCallReaderFactory()
        {
            var result = _connection.Reader;
            result = _connection.Reader;
            result = _connection.Reader;

            Mock.Get(_readerFactory).Verify(f => f.Get(_connectionHandler), Times.Once);
        }
    }
}
