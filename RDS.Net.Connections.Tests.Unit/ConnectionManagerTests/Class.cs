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
    public class Class
    {
        IConnection _connection = Mock.Of<IConnection>();
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();

        [Fact]
        public void ItExists()
        {
            new ConnectionManager(_connection, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItImplementIConnectionManager()
        {
            Assert.IsAssignableFrom<IConnectionManager>(new ConnectionManager(_connection, _readerFactory, _writerFactory));
        }
    }
}
