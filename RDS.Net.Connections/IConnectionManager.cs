using RDS.Net.Connections.Readers;
using RDS.Net.Connections.Writers;

namespace RDS.Net.Connections
{
    public interface IConnectionManager
    {
        IConnection Connection { get; }
        IReader Reader { get; }
        IWriter Writer { get; }
    }
}