using System;
using System.Threading;
using System.Threading.Tasks;

namespace RDS.Net.Connections.Wrappers
{
    internal interface ITask
    {
        void Run(Action action);
        Task Run(Action action, CancellationToken cancellationToken);
    }
}