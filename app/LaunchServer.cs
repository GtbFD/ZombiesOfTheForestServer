using System.Text;
using app.server;
using app.utils.io;

class LaunchServer
{
    public static void Main(String[] args)
    {
        var tcpServer = new TcpServerConfiguration();
        tcpServer.Start();

        var udpServer = new UdpServerConfiguration();
        udpServer.Start();
        
    }

}
