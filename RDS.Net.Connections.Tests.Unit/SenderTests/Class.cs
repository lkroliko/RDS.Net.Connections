using Moq;
using RDS.Net.Connections.Senders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.SenderTests
{
    [Trait("Category", "Sender")]
    public class Class
    {
        IConnectionHandler _connection = Mock.Of<IConnectionHandler>();

        [Fact]
        public void ItExists()
        {
            new Sender(_connection);
        }

        [Fact]
        public void ItImoplementsIWriter()
        {
           Assert.IsAssignableFrom<ISender>(new Sender(_connection));
        }
    }
}
