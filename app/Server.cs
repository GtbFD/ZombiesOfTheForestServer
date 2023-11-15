using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

class Server 
{
    private string HOST;
    private int PORT;
    private IPAddress IP_ADDRESS;

    private int MAX_CONNECTIONS = 100;
    public static List<Socket> ConnectedPlayers;

    public Server()
    {
        ConnectedPlayers = new List<Socket>();
        this.HOST = "localhost";
        this.PORT = 11000;
    }

    public void Start()
    {
        Socket SocketConnection = Config();

        WellcomeMessage();

        Listener(SocketConnection);
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
        HandlerPackets PacketsManager = new HandlerPackets(SocketConnection, ConnectedPlayers);

        Thread HandlerPacketsThread = new Thread(PacketsManager.Listening);
        HandlerPacketsThread.Start();
    }
}