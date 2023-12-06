using System.Net.Sockets;

namespace app.interfaces;

public interface IConnectionType
{
    public Socket GetConnection();
}