using System.Net;
using System.Net.Sockets;

class Server 
{
    public void config()
    {
        AddressFamily IPAdressFamily = getIP().AddressFamily;
        IPEndPoint IPEndPoint = prepareEndPoint();
        int MAX_CONNECTIONS = 10;

        Socket listenerSocket = new Socket(IPAdressFamily, SocketType.Stream, ProtocolType.Tcp);
        listenerSocket.Bind(IPEndPoint);

        listenerSocket.Listen(MAX_CONNECTIONS);
        Console.WriteLine("Server configured");

        Console.WriteLine("Waiting for connections");
        listener(listenerSocket);
    }

    private IPAddress getIP()
    {
        string HOST = "localhost";
        IPHostEntry DNS = Dns.GetHostEntry(HOST);
        IPAddress ip = DNS.AddressList[0];

        return ip;
    }

    private IPEndPoint prepareEndPoint()
    {
        int PORT = 11000;
        IPAddress HOST = getIP();
        IPEndPoint endPoint = new IPEndPoint(HOST, PORT);

        return endPoint;
    }

    public void listener(Socket listener)
    {
        Socket handler = listener.Accept();
        HandlerMessages handlerMessages = new HandlerMessages();
        handlerMessages.listening(handler);
    }


}