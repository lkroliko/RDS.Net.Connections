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
    public class Reader
    {
        IConnection _connection = Mock.Of<IConnection>();
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        IReader _reader = Mock.Of<IReader>();
        IConnectionManager _connectionManager;

        public Reader()
        {
            Mock.Get(_readerFactory).Setup(f => f.Get(_connection)).Returns(_reader);
            _connectionManager = new ConnectionManager(_connection, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItExists()
        {
            var reader = _connectionManager.Reader;
        }

        [Fact]
        public void ItReturnsReader()
        {
            var result = _connectionManager.Reader;

            Assert.Same(_reader, result);
        }

        [Fact]
        public void ItCallReaderFactory()
        {
            var result = _connectionManager.Reader;
            result = _connectionManager.Reader;
            result = _connectionManager.Reader;

            Mock.Get(_readerFactory).Verify(f => f.Get(_connection), Times.Once);
        }
    }
}
