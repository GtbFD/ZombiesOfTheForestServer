using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using app;

class ServerConfiguration
{
    private string HOST;
    private int PORT;
    private IPAddress IP_ADDRESS;

    private int MAX_CONNECTIONS = 100;
    private static ListPlayers connectedPlayers;

    public ServerConfiguration()
    {
        connectedPlayers = ListPlayers.GetInstance();
        this.HOST = "localhost";
        this.PORT = 11000;
    }

    public void Start()
    {
        var socketConnection = Config();

        WellcomeMessage();

        Listener(socketConnection);
    }

    private Socket Config()
    {
        var IPAdressFamily = GetAdressFamily();
        var IpEndPoint = prepareEndPoint();

        Socket SocketConnection
            = new Socket(IPAdressFamily, SocketType.Stream, ProtocolType.Tcp);

        SocketConnection.Bind(IpEndPoint);
        SocketConnection.Listen(MAX_CONNECTIONS);

        return SocketConnection;
    }

    private AddressFamily GetAdressFamily()
    {
        return GetIpAddress().AddressFamily;
    }

    private IPEndPoint prepareEndPoint()
    {
        IP_ADDRESS = GetIpAddress();
        var EndPoint = new IPEndPoint(IP_ADDRESS, PORT);

        return EndPoint;
    }

    private IPAddress GetIpAddress()
    {

        var DNS = Dns.GetHostEntry(this.HOST);
        var ip = DNS.AddressList[0];

        return ip;
    }

    private void WellcomeMessage()
    {
        Console.WriteLine("[STATUS]: Working\n");
    }

    private void Listener(Socket listenerSocket)
    {
        while (RunForever())
        {
            var handler = listenerSocket.Accept();

            connectedPlayers.AddPlayer(handler);

            InvokeHandlerMessagesWithThread(handler);
        }
    }

    private bool RunForever()
    {
        return true;
    }

    private void InvokeHandlerMessagesWithThread(Socket SocketConnection)
    {

        var server = new Server(SocketConnection);

        var serverThread = new Thread(server.Listening);
        serverThread.Start();
    }
}