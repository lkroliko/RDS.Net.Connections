using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using RDS.Logging;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Wrappers;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ConnectionTests
{
    [Trait("Category", "Connection")]
    public class IsConnected
    {
        Connection _connection;
        ILogger _logger = Mock.Of<ILogger>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        public int ReconnectTime { get; }

        public IsConnected()
        {
            _connection = new Connection(_logger, _dateTime, _thread, _netClient, ReconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            var result = _connection.IsConnected;
        }

        [Fact]
        public void ItReturnTrue()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);

            var result = _connection.IsConnected;

            Assert.True(result);
        }

        [Fact]
        public void ItReturnFalse()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);

            var result = _connection.IsConnected;

            Assert.False(result);
        }
    }
}
