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
    public class Class
    {
        INetClientProxy _netClient = Mock.Of<INetClientProxy>();
        IDateTime _dateTime = Mock.Of<IDateTime>();
        IThread _thread = Mock.Of<IThread>();
        ILogger _logger = Mock.Of<ILogger>();
        int _reconnectTime = 1;

        [Fact]
        public void ItExists()
        {
            new Connection(_logger, _dateTime, _thread, _netClient, _reconnectTime);
        }

        [Fact]
        public void ItImplementIConnection()
        {
            Assert.IsAssignableFrom<IConnection>(new Connection(_logger, _dateTime, _thread, _netClient, _reconnectTime));
        }
    }
}
