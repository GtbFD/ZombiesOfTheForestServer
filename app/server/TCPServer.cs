using System.Net.Sockets;
using static app.server.ServerInfo;

namespace app.server;

public class TCPServer
{
    public Socket Config()
    {
        var maxConnections = 5;

        var ipAdressFamily = ServerInfoInstance().AddressFamily(ServerInfoInstance().GetHostTCP());
        var endPoint = new EndPointServer().Config(ServerInfoInstance().GetHostTCP(), 
            ServerInfoInstance().GetPortTCP());

        var connectionTCP = new Socket(ipAdressFamily, SocketType.Stream, ProtocolType.Tcp);

        connectionTCP.Bind(endPoint);
        connectionTCP.Listen(maxConnections);

        return connectionTCP;
    }
    
}