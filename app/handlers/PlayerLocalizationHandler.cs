using System.Diagnostics;
using System.Net.Sockets;
using app.packets.enums;
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
            Console.WriteLine($"x {reader.ReadFloat()}, y {reader.ReadFloat()}, y {reader.ReadFloat()}");
        }
    }

    public void Write()
    {
        
    }
}