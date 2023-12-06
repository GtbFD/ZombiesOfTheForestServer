using System.Net;
using System.Net.Sockets;
using app.interfaces;

namespace app.server;

public class PacketListener
{
    private IPacketListener listener;
    private IPEndPoint ipEndPoint;

    public void ConfigListener(string type, Socket connection)
    {
        if (type.Equals("tcp"))
        {
            CallTcpPackets(connection);
        }

        if (type.Equals("udp"))
        {
            CallUdpPackets(connection);
        }
    }

    private void CallTcpPackets(Socket connection)
    {
        
        var tcp = new PacketListenerTcp();
        tcp.SetConnection(connection);

        new Thread(tcp.ListenToPackets).Start();
    }
    
    private void CallUdpPackets(Socket connection)
    {
        var udp = new PacketListenerUdp();
        udp.SetConnection(connection);
        udp.SetIpEndPoint(ipEndPoint);

        new Thread(udp.ListenToPackets).Start();
    }

    public void SetIpEndPoint(IPEndPoint ipEndPoint)
    {
        this.ipEndPoint = ipEndPoint;
    }
}