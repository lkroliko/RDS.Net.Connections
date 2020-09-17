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
    public class Connect
    {
        Connection _connection;
        ILogger _logger = Mock.Of<ILogger>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        int _reconnectTime = 100;

        public Connect()
        {
            _connection = new Connection(_logger, _dateTime,_thread, _netClient, _reconnectTime);
        }

        [Fact]
        public void ItExists()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);

            _connection.Connect();
        }

        [Fact]
        public void WhenNotConnectedThenTcpClientConnectCalled()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
            {
                Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
            });

            _connection.Connect();

            Mock.Get(_netClient).Verify(c => c.Connect(), Times.Once);
        }

        [Fact]
        public void WhenNotConnectedThenTcpClientConnectCalledThreeTimes()
        {
            int callbackCount = 0;
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
            {
                callbackCount++;
                if (callbackCount == 3)
                    Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);            
            });

            _connection.Connect();

            Mock.Get(_netClient).Verify(c => c.Connect(), Times.Exactly(3));
        }

        [Fact]
        public void WhenConnetedThenTcpClientConnectNotCalled()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);

            _connection.Connect();

            Mock.Get(_netClient).Verify(c => c.Connect(), Times.Never);
        }

        [Fact]
        public void WhenConnectedAtFirstAttemptThenLoggerCalled()
        {
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
            {
                Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
            });

            _connection.Connect();

            Mock.Get(_logger).Verify(l => l.Information("Connected"), Times.Once);
        }

        [Fact]
        public void WhenNotConnectedAtFirsthAttemptThenLoggerCalled()
        {
            int callbackCount = 0;
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
            {
                callbackCount++;
                switch (callbackCount)
                {
                    default:
                        throw new Exception();
                    case 2:
                        Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
                        break;
                }
                Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
            });

            _connection.Connect();

            Mock.Get(_logger).Verify(l => l.Debug("Unable to connect"), Times.Once);
        }

        [Fact]
        public void WhenConnectedAtSecondAttemptThenLoggerCalled()
        {
            int callbackCount = 0;
            Mock.Get(_dateTime).Setup(d => d.Now).Returns(new DateTime(2000, 1, 2, 8, 0, 0));
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
            {
                callbackCount++;
                switch (callbackCount)
                {
                    default:
                        throw new Exception();
                    case 2:
                        Mock.Get(_dateTime).Setup(d => d.Now).Returns(new DateTime(2000, 1, 2, 9, 0, 0));
                        Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
                        break;
                }
                Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
            });

            _connection.Connect();

            Mock.Get(_logger).Verify(l => l.Information("Connected after {0}", "0 01:00:00"), Times.Once);
        }

        [Fact]
        public void WhenNotConnectedThenThreadSleepCalled()
        {
            int callbackCount = 0;
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
            {
                callbackCount++;
                switch (callbackCount)
                {
                    default:
                        throw new Exception();
                    case 4:
                        Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
                        break;
                }
                Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
            });

            _connection.Connect();

            Mock.Get(_thread).Verify(t => t.Sleep(_reconnectTime), Times.Exactly(3));
        }

        [Fact]
        public void WhenDisconnectedAndNotConnectedAtFirsthAttemptThenLoggerCalled()
        {
            int callbackCount = 0;
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            Mock.Get(_netClient).Setup(c => c.Connect()).Callback(() =>
            {
                callbackCount++;
                switch (callbackCount)
                {
                    case 1:
                        Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
                        break;
                    case 2:
                        Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
                        break;
                }
                Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(true);
            });
            _connection.Connect();
            Mock.Get(_netClient).Setup(c => c.IsConnected).Returns(false);
            
            _connection.Connect();

            Mock.Get(_logger).Verify(l => l.Warning("Disconnection detected"), Times.Once);
        }
    }
}
