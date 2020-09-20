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
    public class Started
    {

        IConnectionHandler _connectionHandler = Mock.Of<IConnectionHandler>();
        IReaderFactory _readerFactory = Mock.Of<IReaderFactory>();
        IWriterFactory _writerFactory = Mock.Of<IWriterFactory>();
        Connection _connection;
        CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public Started()
        {
            _connection = new Connection(_connectionHandler, _readerFactory, _writerFactory);
        }

        [Fact]
        public void ItExists()
        {
            _connection.Started += delegate { };
        }
    }
}
