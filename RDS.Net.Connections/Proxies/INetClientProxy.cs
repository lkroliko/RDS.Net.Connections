using RDS.Net.Connections.Wrappers;

namespace RDS.Net.Connections.Proxies
{
    internal interface INetClientProxy
    {
        bool IsConnected { get; }
        void Connect();
        IStreamReader GetStreamReader();
        IStreamWriter GetStreamWriter();
    }
}