using System.Net;
using System.Net.Sockets;
using static app.server.ServerInfo;

namespace app.server;

public class UDPServer
{

    public UdpClient Config()
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Any, GetInstance().GetPortUDP());
        
        var udpClient = new UdpClient(ipEndPoint);

        return udpClient;
    }
    
}