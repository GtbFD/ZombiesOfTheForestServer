using System.Net;
using System.Net.Sockets;
using System.Text;
using app.models;
using app.utils.io;
using static app.server.ServerInfo;

namespace app.server;

class ServerConfiguration
{
    private int maxConnections = 5;

    public void Start()
    {
        Console.WriteLine("[STATUS]: STARTED");

        var threadTcp = new Thread(() => ConfigTCP());
        threadTcp.Start();
        
        var threadUdp = new Thread(() => ConfigUDP());
        threadUdp.Start();
        
    }

    private void ConfigUDP()
    {
        Console.WriteLine("[UDP]: OK");

        var udpServer = new UDPServer();

        var socketUdp = udpServer.Config();

        var endPoint = new IPEndPoint(IPEndPoint.Parse(ServerInfoInstance().GetHostUDP()).Address,
            ServerInfoInstance().GetPortUDP());

        WaitToConnectionsUDP(socketUdp, endPoint);
    }

    private void WaitToConnectionsUDP(UdpClient udpClient, IPEndPoint endPoint)
    {
        ListenToPacketsUDP(udpClient, endPoint);
    }

    private void ListenToPacketsUDP(UdpClient udpClient, IPEndPoint endPoint)
    {
        var packetListener = new PacketListener();
        packetListener.SetConnectionUDP(udpClient);
        
        var serverThread = new Thread(packetListener.ListenToPackets);
        serverThread.Start();
        /*while (true)
        {
            var messageReceived = udpClient.Receive(ref endPoint);

            var reader = new ReadPacket(messageReceived);
            var opcode = reader.ReadInt();
            var message = reader.ReadString(4);

            Console.WriteLine($"Opcode {opcode}, content {message}");
        }*/
    }

    private void ConfigTCP()
    {
        Console.WriteLine("[TCP]: OK");

        var connectionTCP = new TCPServer().Config();

        WaitToConnectionsTCP(connectionTCP);
    }

    private void WaitToConnectionsTCP(Socket listenerSocket)
    {
        while (true)
        {
            var acceptedConnection = listenerSocket.Accept();

            var player = new Player()
            {
                connection = acceptedConnection,
                localization = null
            };

            PlayerList.GetInstance().AddPlayer(player);
            Console.WriteLine("- {0} player(s) connected", PlayerList.GetInstance().GetList().Count);
            ListenToPacketsTCP(acceptedConnection);
        }
    }


    private void ListenToPacketsTCP(Socket connection)
    {
        var packetListener = new PacketListener();
        packetListener.SetConnectionTCP(connection);

        var serverThread = new Thread(packetListener.ListenToPackets);
        serverThread.Start();
    }
}