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
    public class Class
    {
        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();

        [Fact]
        public void ItExists()
        {
            new Connection(_connectionHandler, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItImplementIConnectionManager()
        {
            Assert.IsAssignableFrom<IConnection>(new Connection(_connectionHandler, _readerFactory, _writerFactory));
        }
    }
}
