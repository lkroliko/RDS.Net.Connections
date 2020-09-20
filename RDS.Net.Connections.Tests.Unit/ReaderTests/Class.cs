using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ReaderTests
{
    [Trait("Category", "Reader")]
    public class Class
    {
        IConnection _connection = Mock.Of<IConnection>();

        [Fact]
        public void ItExists()
        {
            Reader reader = new Reader(_connection);
        }
    }
}
