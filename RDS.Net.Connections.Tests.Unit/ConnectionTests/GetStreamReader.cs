using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Moq;
using RDS.Logging;
using RDS.Net.Connections;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Wrappers;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionTests
{
    [Trait("Category", "Connection")]
    public class GetStreamReader
    {
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        IStreamReader _streamReader = Mock.Of<IStreamReader>();
        Connection _connection;
        ILogger _logger = Mock.Of<ILogger>();
        int _reconnectTime = 1;

        public GetStreamReader()
        {
            Mock.Get(_netClient).Setup(c => c.GetStreamReader()).Returns(_streamReader);
            _connection = new Connection(_logger, _dateTime, _thread, _netClient, _reconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            _connection.GetStreamReader();
        }

        [Fact]
        public void WhenCalledThenStreamReaderReturned()
        {
            var result = _connection.GetStreamReader();

            Assert.Equal(_streamReader, result);
        }

        [Fact]
        public void WhenCalledThenTcpClientGetStreamReaderCalled()
        {
            var result = _connection.GetStreamReader();

            Mock.Get(_netClient).Verify(c => c.GetStreamReader(), Times.Once);
        }
    }
}
