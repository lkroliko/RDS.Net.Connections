using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionManagerTests
{
    [Trait("Category", "ConnectionManager")]
    public class Writer
    {
        IConnection _connection = Mock.Of<IConnection>();
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        IWriter _writer = Mock.Of<IWriter>();
        IConnectionManager _connectionManager;

        public Writer()
        {
            Mock.Get(_writerFactory).Setup(f => f.Get(_connection)).Returns(_writer);
            _connectionManager = new ConnectionManager(_connection, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItExists()
        {
            var reader = _connectionManager.Writer;
        }

        [Fact]
        public void ItReturnsReader()
        {
            var result = _connectionManager.Writer;

            Assert.Same(_writer, result);
        }

        [Fact]
        public void ItCallReaderFactory()
        {
            var result = _connectionManager.Writer;
            result = _connectionManager.Writer;
            result = _connectionManager.Writer;

            Mock.Get(_writerFactory).Verify(f => f.Get(_connection), Times.Once);
        }
    }
}
