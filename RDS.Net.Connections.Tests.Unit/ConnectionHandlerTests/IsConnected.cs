using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using RDS.Logging;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Wrappers;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionHandlerTests
{
    [Trait("Category", "ConnectionHandler")]
    public class IsConnected
    {
        ConnectionHandler _connectionHandler;
        ILogger _logger = Mock.Of<ILogger>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        public int ReconnectTime { get; }

        public IsConnected()
        {
            _connectionHandler = new ConnectionHandler(_logger, _dateTime, _thread, _netClient, ReconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            var result = _connectionHandler.IsConnected;
        }

        [Fact]
        public void ItReturnTrue()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);

            var result = _connectionHandler.IsConnected;

            Assert.True(result);
        }

        [Fact]
        public void ItReturnFalse()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);

            var result = _connectionHandler.IsConnected;

            Assert.False(result);
        }
    }
}
