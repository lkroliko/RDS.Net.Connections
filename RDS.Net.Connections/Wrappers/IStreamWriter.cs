namespace RDS.Net.Connections.Wrappers
{
    public interface IStreamWriter
    {
        void WriteLine(string value);
        void Flush();
    }
}