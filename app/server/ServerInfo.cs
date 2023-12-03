using System.Net;
using System.Net.Sockets;

namespace app.server;

public sealed class ServerInfo
{
    private string hostTCP;
    private string hostUDP;
    
    private int portTCP;
    private int portUDP;
    
    private Socket connection;
    private byte[] buffer;

    private static ServerInfo instance;

    public ServerInfo()
    {
        hostTCP = "127.0.0.1";
        hostUDP = "127.0.0.1";
        
        portTCP = 11000;
        portUDP = 11001;
        
        buffer = new byte[2 * 1024];
    }

    public static ServerInfo ServerInfoInstance()
    {
        if (instance == null)
        {
            instance = new ServerInfo();
        }

        return instance;
    }

    public string GetHostTCP()
    {
        return hostTCP;
    }

    public int GetPortTCP()
    {
        return portTCP;
    }
    
    public string GetHostUDP()
    {
        return hostUDP;
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