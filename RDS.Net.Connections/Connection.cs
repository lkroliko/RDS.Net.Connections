using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RDS.Logging;
using RDS.Net.Connections.Proxies;
using RDS.Net.Connections.Wrappers;

namespace RDS.Net.Connections
{
    public class Connection : IConnection
    {
        public bool IsConnected { get { return _netClient.IsConnected; } }
        int _millisecondsReconnectTime;
        INetClientProxy _netClient;
        IDateTime _dateTime;
        IThread _thread;
        ILogger _logger;
        DateTime? _disconnectDateTime;
        DateTime? _connectDateTime;
        
        public event EventHandler<EventArgs> Connected = delegate { };
        protected virtual void OnConnected(EventArgs e) { Connected.Invoke(this, e); }

        internal Connection(ILogger logger, IDateTime dateTime, IThread thread, INetClientProxy netClient, int millisecondsReconnectTime)
        {
            _logger = logger;
            _dateTime = dateTime;
            _thread = thread;
            _netClient = netClient;
            _millisecondsReconnectTime = millisecondsReconnectTime;
        }

        public IStreamReader GetStreamReader()
        {
            return _netClient.GetStreamReader();
        }

        public IStreamWriter GetStreamWriter()
        {
            return _netClient.GetStreamWriter();
        }

        private void LogConnected()
        {
            _connectDateTime = _dateTime.Now;
            if (_disconnectDateTime == null)
            {
                _logger.Information("Connected");
            }
            else
            {
                var time = (_disconnectDateTime - _connectDateTime);
                _logger.Information("Connected after {0}", time.Value.ToString(@"d\ hh\:mm\:ss"));
            }
            _disconnectDateTime = null;
        }

        private void LogNotConnected()
        {
            if (_disconnectDateTime == null)
            {
                _logger.Debug("Unable to connect");
                _disconnectDateTime = _dateTime.Now;
                _connectDateTime = null;
            }
        }

        public void Connect()
        {
            if (_netClient.IsConnected == false && _connectDateTime != null)
                _logger.Warning("Disconnection detected");
            while (_netClient.IsConnected == false)
            {
                try
                {
                    _netClient.Connect();
                    LogConnected();
                    OnConnected(new EventArgs());
                }
                catch
                {
                    LogNotConnected();
                    _thread.Sleep(_millisecondsReconnectTime);
                }
            }
        }
    }
}
