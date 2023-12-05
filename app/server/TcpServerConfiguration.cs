using System.Net;
using System.Net.Sockets;
using System.Text;
using app.models;
using app.utils.io;
using static app.server.ServerInfo;

namespace app.server;

class TcpServerConfiguration
{
    private int maxConnections = 5;

    public void Start()
    {
        Console.WriteLine("[TCP SERVER]: STARTED");

        var threadTcp = new Thread(() => Config());
        threadTcp.Start();
        
    }

    private void Config()
    {
        var connectionTCP = new TCPServer().Config();

        WaitToConnections(connectionTCP);
    }

    private void WaitToConnections(Socket listenerSocket)
    {
        while (true)
        {
            var acceptedConnection = listenerSocket.Accept();

            /*var player = new Player()
            {
                tcpConnection = acceptedConnection,
                udpConnection = null,
                localization = null
            };
            
            PlayerList.GetInstance().AddPlayer(player);*/
            //Console.WriteLine("- {0} player(s) connected", PlayerList.GetInstance().GetList().Count);
            ListenToPackets(acceptedConnection);
        }
    }


    private void ListenToPackets(Socket connection)
    {
        var packetListener = new PacketListenerTcp();
        packetListener.SetConnectionTCP(connection);

        var serverThread = new Thread(packetListener.ListenToPackets);
        serverThread.Start();
    }
}