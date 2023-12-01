using System.Net;
using System.Net.Sockets;
using app.models;

namespace app.server;

class ServerConfiguration
{
    
    private int maxConnections = 5;

    public void Start()
    {
        Console.WriteLine("[STATUS]: STARTED");
        
        ConfigUDP();
        ConfigTCP();
        
    }
    
    private void ConfigTCP()
    {
        Console.WriteLine("[TCP]: OK");
        
        var serverInfo = new ServerInfo();
        
        var ipAdressFamily = serverInfo.AddressFamily(serverInfo.GetHost());
        var endPoint = new EndPointServer().Config(serverInfo.GetHost(), serverInfo.GetPortTCP());

        var connection = new Socket(ipAdressFamily, SocketType.Stream, ProtocolType.Tcp);

        connection.Bind(endPoint);
        connection.Listen(maxConnections);

        WaitToConnections(connection);
    }

    private void ConfigUDP()
    {
        Console.WriteLine("[UDP]: OK");
        
        var serverInfo = new ServerInfo();

        var ipAddressFamily = serverInfo.AddressFamily(serverInfo.GetHost());
        var endPoint = new EndPointServer().Config(serverInfo.GetHost(), serverInfo.GetPortUDP());
        
        var connectionUDP = new Socket(ipAddressFamily, SocketType.Dgram, ProtocolType.Udp);
        connectionUDP.Bind(endPoint);

        WaitToConnectionsUDP(connectionUDP);
    }

    private void WaitToConnectionsUDP(Socket connectionUDP)
    {
        var serverInfo = new ServerInfo();
        var udpConnection = new UdpClient(serverInfo.GetPortUDP());

        var remoteEp = (EndPoint) new EndPointServer().Config(serverInfo.GetHost(), serverInfo.GetPortUDP());
        var data = new byte[1024];
        Task.Run(() =>
        {
            while (true)
            {
                var res = connectionUDP.ReceiveFrom(data, ref remoteEp);
                
                Console.WriteLine("UDP Message: " + res.ToString());
            }
        });
    }

    private void WaitToConnections(Socket listenerSocket)
    {
        while (true)
        {
            var acceptedConnection = listenerSocket.AcceptAsync();

            var player = new Player()
            {
                connection = acceptedConnection.Result,
                localization = null
            };
            
            PlayerList.GetInstance().AddPlayer(player);
            Console.WriteLine("- {0} player(s) connected", PlayerList.GetInstance().GetList().Count);
            ListenToPackets(acceptedConnection);
        }
    }
    

    private void ListenToPackets(Task<Socket> connection)
    {
        var packetListener = new PacketListener(connection);

        var serverThread = new Thread(packetListener.ListenToPackets);
        serverThread.Start();
        
    }
}