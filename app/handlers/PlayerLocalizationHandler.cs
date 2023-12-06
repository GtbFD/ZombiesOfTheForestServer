using System.Diagnostics;
using System.Net.Sockets;
using app.packets.enums;
using app.server;
using app.utils.io;

namespace app.handlers;

public class PlayerLocalizationHandler : IPacketHandler
{
    private Socket connection;
    private UdpClient udpConnection;

    public PlayerLocalizationHandler(Socket connection)
    {
        this.connection = connection;
    }
    
    public PlayerLocalizationHandler(UdpClient udpConnection)
    {
        this.udpConnection = udpConnection;
    }

    public void Handler(byte[] packetReceived)
    {
        Read(packetReceived);
    }

    public void Read(byte[] packetReceived)
    {
        var reader = new ReadPacket(packetReceived);
        var opcode = reader.ReadInt();

        if (opcode == (int)OpcodePackets.PLAYER_LOCALIZATION)
        {

            var x = reader.ReadFloat();
            var y = reader.ReadFloat();
            var z = reader.ReadFloat();
            
            Console.WriteLine($"x {x}, y {y}, z {z}");
            
            var packetWriter = new WritePacket();
            packetWriter.Write((int) OpcodePackets.PLAYER_LOCALIZATION_RESPONSE);
            packetWriter.Write(x);
            packetWriter.Write(y);
            packetWriter.Write(z);
            var packet = packetWriter.BuildPacket();
            
            /*udpConnection.Send(packet, packet.Length, udpConnection.Client.RemoteEndPoint.ToString(), 
                ServerInfo.ServerInfoInstance().GetPortUDP());*/

        }
    }

    public void Write()
    {
        
    }
}