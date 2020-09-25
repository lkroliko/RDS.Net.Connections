using System;
using System.Collections.Generic;
using System.Text;
using RDS.Logging;
using RDS.Net.Connections.Pingers;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Senders;
using RDS.Net.Connections.Wrappers;
using RDS.Net.Connections.Receivers;
using RDS.Net.Connections.Abstractions;

namespace RDS.Net.Connections
{
    public class ConnectionBuilder
    {
        string _hostname;
        int _port;
        int _millisecondsReconnectTime = 30000;
        Pinger _pinger;
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

        public ConnectionBuilder EnablePinger(string value, int millisecondsInterval)
        {
            _pinger = new Pinger(new TaskWrapper(), new ThreadWrapper(), value, millisecondsInterval);
            return this;
        }

        public IConnection Build()
        {
            if (string.IsNullOrEmpty(_hostname))
                throw new ArgumentNullException("hostname", "Hostname canot be empty");
            if (_port < 0 || _port > 655350)
                throw new ArgumentException("port", "Port must be in range 0 - 655350");
            ConnectionHandler connection = new ConnectionHandler(_logger, new DateTimeWrapper(), new ThreadWrapper(), new TcpClientProxy(_hostname, _port), _millisecondsReconnectTime);                     
            Connection  connectionManager = new Connection(connection, new ReceiverFactory(), new SenderFactory());
            if (_pinger != null)
                connectionManager.Started += _pinger.Start;
            return connectionManager;
        }

        public static ConnectionBuilder New { get { return new ConnectionBuilder(); } }
    }
}
