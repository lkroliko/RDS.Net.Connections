using RDS.Net.Connections.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDS.Net.Connections.Pingers
{
    class Pinger
    {
        IConnection _connection;
        IThread _thread;
        ITask _task;
        string _value;
        int _milisecoundsInterval;

        public Pinger(ITask task, IThread thread, string value, int milisecoundsInterval)
        {
            _task = task;
            _thread = thread;
            _value = value;
            _milisecoundsInterval = milisecoundsInterval;
        }

        public void Start(object sender, ConnectionStartedEventArgs args)
        {
            if (sender is IConnection)
            {
                _connection = sender as IConnection;
                _task.Run(() => Ping(args.Token));
            }
        }

        private void Ping(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                _connection.Writer.WriteLine(_value);
                _thread.Sleep(_milisecoundsInterval);
            }
        }
    }
}
