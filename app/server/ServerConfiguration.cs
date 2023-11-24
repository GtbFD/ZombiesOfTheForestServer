using System.Net.Sockets;
using app;
using app.server;

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
            var acceptedConnection = listenerSocket.Accept();

            PlayerList.GetInstance().AddPlayer(acceptedConnection);

            ListenToPackets(acceptedConnection);
        }
    }
    

    private void ListenToPackets(Socket connection)
    {
        var packetListener = new PacketListener(connection);

        var serverThread = new Thread(packetListener.ListenToPackets);
        serverThread.Start();
    }
}