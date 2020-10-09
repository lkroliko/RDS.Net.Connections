using Moq;
using RDS.Net.Connections.Wrappers;
using RDS.Net.Connections.Senders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RDS.Logging;

namespace RDS.Net.Connections.Tests.Unit.SenderTests
{
    [Trait("Category", "Sender")]
    public class WriteLine
    {
        IConnectionHandler _connection = Mock.Of<IConnectionHandler>();
        IStreamWriter _streamWriter = Mock.Of<IStreamWriter>();
        ILogger _logger = Mock.Of<ILogger>();
        Sender _sender;

        public WriteLine()
        {
            Mock.Get(_connection).Setup(c=>c.Connect()).Callback(() => 
            {
                Mock.Get(_connection).Setup(c => c.IsConnected).Returns(true);
            });
            Mock.Get(_connection).Setup(c => c.GetStreamWriter()).Returns(_streamWriter);
            _sender = new Sender(_connection, _logger);
        }

        [Fact]
        public void WhenConnectionNotConnectedThenConnectCalled()
        {
            _sender.SendLine("value");

            Mock.Get(_connection).Verify(c => c.Connect(), Times.Once);
        }

        [Fact]
        public void WhenCalledThenStreamWriterWriteLineCalled()
        {
            _sender.SendLine("value");

            Mock.Get(_streamWriter).Verify(s => s.WriteLine("value"), Times.Once);
        }

        [Fact]
        public void WhenNoExceptionThenTrueReturned()
        {
            var result = _sender.SendLine("value");

            Assert.True(result);
        }

        [Fact]
        public void WhenExceptionThenFalseReturned()
        {
            Mock.Get(_streamWriter).Setup(s => s.WriteLine(It.IsAny<string>())).Throws<Exception>();

            var result = _sender.SendLine("value");

            Assert.False(result);
        }

        [Fact]
        public void WhenCalledThenLoggerCalled()
        {
            _sender.SendLine("value");

            Mock.Get(_logger).Verify(l => l.Trace("Sended: value"), Times.Once);
        }
    }
}
