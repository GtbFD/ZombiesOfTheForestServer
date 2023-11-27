using System.Net.Sockets;
using app.models;

namespace app.server;

class ServerConfiguration
{
    
    private int maxConnections = 5;

    public void Start()
    {
        Console.WriteLine("Started server");
        Config();
    }
    
    private void Config()
    {
        var ipAdressFamily = new ServerInfo().AddressFamily();
        var endPoint = new EndPointServer().Config();

        var connection = new Socket(ipAdressFamily, SocketType.Stream, ProtocolType.Tcp);

        connection.Bind(endPoint);
        connection.Listen(maxConnections);
        
        WaitToConnections(connection);
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