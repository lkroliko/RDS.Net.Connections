using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Moq;
using RDS.Logging;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Wrappers;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionHandlerTests
{
    [Trait("Category", "ConnectionHandler")]
    public class GetStreamWriter
    {
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        IStreamWriter _streamWriter = Mock.Of<IStreamWriter>();
        ConnectionHandler _connectionHandler;
        ILogger _logger = Mock.Of<ILogger>();
        int _reconnectTime = 1;

        public GetStreamWriter()
        {
            Mock.Get(_netClient).Setup(c => c.GetStreamWriter()).Returns(_streamWriter);
            _connectionHandler = new ConnectionHandler(_logger, _dateTime, _thread, _netClient, _reconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            _connectionHandler.GetStreamWriter();
        }

        [Fact]
        public void WhenCalledThenStreamWriterReturned()
        {
            var result = _connectionHandler.GetStreamWriter();

            Assert.Equal(_streamWriter, result);
        }

        [Fact]
        public void WhenCalledThenTcpClientGetStreamWriterCalled()
        {
            var result = _connectionHandler.GetStreamWriter();

            Mock.Get(_netClient).Verify(c => c.GetStreamWriter(), Times.Once);
        }
    }
}
