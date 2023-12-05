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
            var packetWriter = new WritePacket();
            packetWriter.Write((int) OpcodePackets.PLAYER_LOCALIZATION_RESPONSE);
            packetWriter.Write(reader.ReadFloat());
            packetWriter.Write(reader.ReadFloat());
            packetWriter.Write(reader.ReadFloat());
            var packet = packetWriter.BuildPacket();
            
            udpConnection.Send(packet, packet.Length, ServerInfo.ServerInfoInstance().GetHostUDP(), 
                ServerInfo.ServerInfoInstance().GetPortUDP());

        }
    }

    public void Write()
    {
        
    }
}