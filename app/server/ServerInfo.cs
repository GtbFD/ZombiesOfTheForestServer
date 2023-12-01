using System.Net;
using System.Net.Sockets;

namespace app.server;

public class ServerInfo
{
    private string host;
    private int port;
    private int portUDP;
    private Socket connection;
    private byte[] buffer;

    public ServerInfo()
    {
        host = "localhost";
        port = 11000;
        portUDP = 11001;
        buffer = new byte[4 * 1024];
    }

    public string GetHost()
    {
        return host;
    }

    public int GetPortTCP()
    {
        return port;
    }
    
    public int GetPortUDP()
    {
        return portUDP;
    }

    public byte[] GetBuffer()
    {
        return buffer;
    }

    public string RemoteEndPoint(Socket connection)
    {
        return connection.RemoteEndPoint.ToString();
    }

    public IPAddress IPAdress(string host)
    {
        return Dns.GetHostEntry(host).AddressList[0];
    }

    public AddressFamily AddressFamily(string host)
    {
        return IPAdress(host).AddressFamily;
    }
}