using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Receivers
{
    public class ReceivedEventArgs : EventArgs
    {
        public string Value { get; }

        public ReceivedEventArgs(string value)
        {
            Value = value;
        }
    }
}
