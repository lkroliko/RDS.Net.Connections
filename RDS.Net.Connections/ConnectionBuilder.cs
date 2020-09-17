using System;
using System.Collections.Generic;
using System.Text;
using RDS.Logging;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Wrappers;

namespace RDS.Net.Connections
{
    public class ConnectionBuilder
    {
        string _hostname;
        int _port;
        int _millisecondsReconnectTime = 30000;
        ILogger _logger;

        public ConnectionBuilder SetDestination(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;
            return this;
        }

        public ConnectionBuilder SetReconnectTime(int millisecondsReconnectTime)
        {
            _millisecondsReconnectTime = millisecondsReconnectTime;
            return this;
        }

        public ConnectionBuilder SetLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public Connection Build()
        {
            if (string.IsNullOrEmpty(_hostname))
                throw new ArgumentNullException("hostname", "Hostname canot be empty");
            if (_port < 0 || _port > 655350)
                throw new ArgumentException("port", "Port must be in range 0 - 655350");

            return new Connection(_logger, new DateTimeWrapper(), new ThreadWrapper(), new TcpClientProxy(_hostname, _port), _millisecondsReconnectTime);
        }

        public static ConnectionBuilder New { get { return new ConnectionBuilder(); } }

        public static implicit operator Connection(ConnectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
