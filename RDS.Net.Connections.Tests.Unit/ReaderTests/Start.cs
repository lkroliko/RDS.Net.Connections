using Castle.DynamicProxy.Contributors;
using Moq;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace RDS.Net.Connections.Tests.Unit.ReaderTests
{
    [Trait("Category", "Reader")]
    public class Start
    {
        IConnectionHandler _connection = Mock.Of<IConnectionHandler>();
        IStreamReader _streamReader = Mock.Of<IStreamReader>();
        Reader _reader;
        CancellationTokenSource _cancellationSource = new CancellationTokenSource();

        public Start()
        {
            Mock.Get(_connection).Setup(c => c.GetStreamReader()).Returns(_streamReader);
            _reader = new Reader(_connection);
        }

        [Fact]
        public void WhenCalledThenConnectionConnectCalled()
        {
            Mock.Get(_connection).Setup(c => c.Connect()).Callback(() =>
            {
                Mock.Get(_connection).Setup(c => c.IsConnected).Returns(true);
                _cancellationSource.Cancel();
            });

            _reader.Start(_cancellationSource.Token);

            Mock.Get(_connection).Verify(c => c.Connect(), Times.Once);
        }

        [Fact]
        public void WhenStreamReaderThrowsExceptionAndConnectionIsNotConnectedThenConnectionConnectCalled()
        {
            Mock.Get(_connection).Setup(c => c.IsConnected).Returns(true);
            Mock.Get(_streamReader).Setup(s => s.ReadLine()).Callback(() =>
            {
                Mock.Get(_connection).Setup(c => c.IsConnected).Returns(false);
                _cancellationSource.Cancel();
            }).Throws<Exception>();

            _reader.Start(_cancellationSource.Token);

            Mock.Get(_connection).Verify(c => c.Connect(), Times.Once);
        }

        [Fact]
        public void WhenStreamReaderThrowsEceptionThenConnectionGetStreamReaderCalled()
        {
            Mock.Get(_streamReader).Setup(s => s.ReadLine()).Callback(() =>
            {
                _cancellationSource.Cancel();
            }).Throws<Exception>();

            _reader.Start(_cancellationSource.Token);

            Mock.Get(_connection).Verify(c => c.GetStreamReader(), Times.Exactly(2));
        }
    }
}
