using Moq;
using RDS.Net.Connections.Wrappers;
using RDS.Net.Connections.Writers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.WriterTests
{
    [Trait("Category", "Writer")]
    public class WriteLine
    {
        IConnectionHandler _connection = Mock.Of<IConnectionHandler>();
        IStreamWriter _streamWriter = Mock.Of<IStreamWriter>();
        Writer _writer;

        public WriteLine()
        {
            Mock.Get(_connection).Setup(c=>c.Connect()).Callback(() => 
            {
                Mock.Get(_connection).Setup(c => c.IsConnected).Returns(true);
            });
            Mock.Get(_connection).Setup(c => c.GetStreamWriter()).Returns(_streamWriter);
            _writer = new Writer(_connection);
        }

        [Fact]
        public void WhenConnectionNotConnectedThenConnectCalled()
        {
            _writer.WriteLine("value");

            Mock.Get(_connection).Verify(c => c.Connect(), Times.Once);
        }

        [Fact]
        public void WhenCalledThenStreamWriterWriteLineCalled()
        {
            _writer.WriteLine("value");

            Mock.Get(_streamWriter).Verify(s => s.WriteLine("value"), Times.Once);
        }

        [Fact]
        public void WhenNoExceptionThenTrueReturned()
        {
            var result = _writer.WriteLine("value");

            Assert.True(result);
        }

        [Fact]
        public void WhenExceptionThenFalseReturned()
        {
            Mock.Get(_streamWriter).Setup(s => s.WriteLine(It.IsAny<string>())).Throws<Exception>();

            var result = _writer.WriteLine("value");

            Assert.False(result);
        }
    }
}
