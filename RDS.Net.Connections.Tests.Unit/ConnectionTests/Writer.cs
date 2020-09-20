using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
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
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        IWriter _writer = Mock.Of<IWriter>();
        Connection _connection;

        public Writer()
        {
            Mock.Get(_writerFactory).Setup(f => f.Get(_connectionHandler)).Returns(_writer);
            _connection = new Connection(_connectionHandler, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItExists()
        {
            var reader = _connection.Writer;
        }

        [Fact]
        public void ItReturnsReader()
        {
            var result = _connection.Writer;

            Assert.Same(_writer, result);
        }

        [Fact]
        public void ItCallReaderFactory()
        {
            var result = _connection.Writer;
            result = _connection.Writer;
            result = _connection.Writer;

            Mock.Get(_writerFactory).Verify(f => f.Get(_connectionHandler), Times.Once);
        }
    }
}
