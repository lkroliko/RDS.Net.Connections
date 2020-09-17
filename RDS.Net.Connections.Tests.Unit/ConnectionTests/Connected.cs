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
    public class Connected
    {
        Connection _connection;
        ILogger _logger = Mock.Of<ILogger>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        public int ReconnectTime { get; }

        public Connected()
        {
            _connection = new Connection(_logger, _dateTime, _thread, _netClient, ReconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            _connection.Connected += delegate { };
        }

        [Fact]
        public void WhenConnectedThenConnectedEventCalled()
        {
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
              {
                  Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
              });
            int fireCount = 0;
            _connection.Connected += (obj, args) => { fireCount++; };

            _connection.Connect();

            Assert.Equal(1, fireCount);
        }
    }
}
