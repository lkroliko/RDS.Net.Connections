using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RDS.Net.Connections.Wrappers
{
    internal class TaskWrapper : ITask
    {
        public void Run(Action action)
        {
           Task.Run(action);
        }

        public Task Run(Action action, CancellationToken cancellationToken)
        {
           return Task.Run(action,  cancellationToken);
        }
    }
}
