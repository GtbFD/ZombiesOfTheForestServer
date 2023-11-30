using System.Diagnostics;
using System.Net.Sockets;
using app.packets.enums;
using app.utils.io;

namespace app.handlers;

public class PlayerLocalizationHandler : IPacketHandler
{
    private Socket connection;

    public PlayerLocalizationHandler(Socket connection)
    {
        this.connection = connection;
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
            //Console.WriteLine("[LOCALIZATION] <- PACKET_RECEIVED - ID: " + opcode);
            Thread.Sleep(25);
            var writer = new WritePacket();
            writer.Write((int) OpcodePackets.PLAYER_LOCALIZATION_RESPONSE);
            writer.Write(reader.ReadFloat());
            writer.Write(reader.ReadFloat());
            writer.Write(reader.ReadFloat());

            var playerLocalizationPacket = writer.BuildPacket();
        
            connection.Send(playerLocalizationPacket);
        }
    }

    public void Write()
    {
        
    }
}