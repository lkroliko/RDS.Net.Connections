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
    public class Connected
    {
        ConnectionHandler _connectionHandler;
        ILogger _logger = Mock.Of<ILogger>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        public int ReconnectTime { get; }

        public Connected()
        {
            _connectionHandler = new ConnectionHandler(_logger, _dateTime, _thread, _netClient, ReconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            _connectionHandler.Connected += delegate { };
        }

        [Fact]
        public void WhenConnectedThenConnectedEventCalled()
        {
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
              {
                  Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
              });
            int fireCount = 0;
            _connectionHandler.Connected += (obj, args) => { fireCount++; };

            _connectionHandler.Connect();

            Assert.Equal(1, fireCount);
        }
    }
}
