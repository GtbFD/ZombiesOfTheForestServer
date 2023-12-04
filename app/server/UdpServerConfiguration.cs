using System.Net;
using System.Net.Sockets;
using static app.server.ServerInfo;

namespace app.server;

public class UdpServerConfiguration
{
    public void Start()
    {
        Console.WriteLine("[UDP SERVER]: STARTED");
        
        var threadUdp = new Thread(() => Config());
        threadUdp.Start();
    }
    
    private void Config()
    {
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
        var packetListener = new PacketListenerUdp();
        packetListener.SetConnectionUDP(udpClient, endPoint);
        
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
}