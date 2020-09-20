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

namespace RDS.Net.Connections.Tests.Unit.ConnectionHandlerTests
{
    [Trait("Category", "ConnectionHandler")]
    public class GetStreamReader
    {
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        IStreamReader _streamReader = Mock.Of<IStreamReader>();
        ConnectionHandler _connectionHandler;
        ILogger _logger = Mock.Of<ILogger>();
        int _reconnectTime = 1;

        public GetStreamReader()
        {
            Mock.Get(_netClient).Setup(c => c.GetStreamReader()).Returns(_streamReader);
            _connectionHandler = new ConnectionHandler(_logger, _dateTime, _thread, _netClient, _reconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            _connectionHandler.GetStreamReader();
        }

        [Fact]
        public void WhenCalledThenStreamReaderReturned()
        {
            var result = _connectionHandler.GetStreamReader();

            Assert.Equal(_streamReader, result);
        }

        [Fact]
        public void WhenCalledThenTcpClientGetStreamReaderCalled()
        {
            var result = _connectionHandler.GetStreamReader();

            Mock.Get(_netClient).Verify(c => c.GetStreamReader(), Times.Once);
        }
    }
}
