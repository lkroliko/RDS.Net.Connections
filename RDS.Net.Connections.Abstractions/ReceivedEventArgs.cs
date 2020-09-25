using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Net.Connections.Abstractions
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
