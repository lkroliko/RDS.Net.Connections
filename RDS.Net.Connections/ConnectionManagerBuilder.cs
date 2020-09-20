using System;
using System.Collections.Generic;
using System.Text;
using RDS.Logging;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Wrappers;
using RDS.Net.Connections.Writers;

namespace RDS.Net.Connections
{
    public class ConnectionManagerBuilder
    {
        string _hostname;
        int _port;
        int _millisecondsReconnectTime = 30000;
        ILogger _logger;

        public ConnectionManagerBuilder SetDestination(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;
            return this;
        }

        public ConnectionManagerBuilder SetReconnectTime(int millisecondsReconnectTime)
        {
            _millisecondsReconnectTime = millisecondsReconnectTime;
            return this;
        }

        public ConnectionManagerBuilder SetLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public IConnectionManager Build()
        {
            if (string.IsNullOrEmpty(_hostname))
                throw new ArgumentNullException("hostname", "Hostname canot be empty");
            if (_port < 0 || _port > 655350)
                throw new ArgumentException("port", "Port must be in range 0 - 655350");

            Connection connection = new Connection(_logger, new DateTimeWrapper(), new ThreadWrapper(), new TcpClientProxy(_hostname, _port), _millisecondsReconnectTime);
            return new ConnectionManager(connection, new ReaderFactory(), new WriterFactory());
        }

        public static ConnectionManagerBuilder New { get { return new ConnectionManagerBuilder(); } }
    }
}
