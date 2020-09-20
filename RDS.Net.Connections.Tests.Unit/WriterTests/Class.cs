using Moq;
using RDS.Net.Connections.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.WriterTests
{
    [Trait("Category", "Writer")]
    public class Class
    {
        IConnection _connection = Mock.Of<IConnection>();

        [Fact]
        public void ItExists()
        {
            new Writer(_connection);
        }

        [Fact]
        public void ItImoplementsIWriter()
        {
           Assert.IsAssignableFrom<IWriter>(new Writer(_connection));
        }
    }
}
