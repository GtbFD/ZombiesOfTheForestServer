using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

class ServerConfiguration
{
    private string HOST;
    private int PORT;
    private IPAddress IP_ADDRESS;

    private int MAX_CONNECTIONS = 100;
    public static List<Socket> ConnectedPlayers;

    public ServerConfiguration()
    {
        ConnectedPlayers = new List<Socket>();
        this.HOST = "localhost";
        this.PORT = 11000;
    }

    public void Start()
    {
        var socketConnection = Config();

        WellcomeMessage();

        Listener(socketConnection);
    }

    public Socket Config()
    {
        AddressFamily IPAdressFamily = GetAdressFamily();
        IPEndPoint IpEndPoint = prepareEndPoint();

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
        IPEndPoint EndPoint = new IPEndPoint(IP_ADDRESS, PORT);

        return EndPoint;
    }

    private IPAddress GetIpAddress()
    {

        IPHostEntry DNS = Dns.GetHostEntry(this.HOST);
        IPAddress ip = DNS.AddressList[0];

        return ip;
    }

    public void WellcomeMessage()
    {
        Console.WriteLine("[STATUS]: Working\n");
    }

    public void Listener(Socket listenerSocket)
    {
        while (RunForever())
        {
            Socket handler = listenerSocket.Accept();

            ConnectedPlayers.Add(handler);

            InvokeHandlerMessagesWithThread(handler);
        }
    }

    public bool RunForever()
    {
        return true;
    }

    public void InvokeHandlerMessagesWithThread(Socket SocketConnection)
    {
        var server = new Server(SocketConnection, ConnectedPlayers);

        Thread serverThread = new Thread(server.Listening);
        serverThread.Start();
    }
}