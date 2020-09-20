using System;
using System.Threading.Tasks;

namespace RDS.Net.Connections.Wrappers
{
    interface ITask
    {
        Task Run(Action action);
    }
}