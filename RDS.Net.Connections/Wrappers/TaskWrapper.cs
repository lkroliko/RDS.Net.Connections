using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDS.Net.Connections.Wrappers
{
    class TaskWrapper : ITask
    {
        public Task Run(Action action)
        {
            return Task.Run(action);
        }
    }
}
