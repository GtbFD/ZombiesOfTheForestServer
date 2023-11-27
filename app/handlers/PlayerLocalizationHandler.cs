using System.Net.Sockets;
using app.packets.enums;
using app.packets.request;
using app.packets.response;
using app.utils.identifier;
using app.utils.io;

namespace app.handlers;

public class PlayerLocalizationHandler : IPacketHandler
{
    private Socket connection;

    public PlayerLocalizationHandler(Socket connection)
    {
        this.connection = connection;
    }

    public void Handler(string packetReceived)
    {
        Read(packetReceived);
    }

    public void Read(string packetReceived)
    {
        
        var opcode = PacketIdentifier.Opcode(packetReceived);

        if (opcode.Equals( (int)OpcodePackets.PLAYER_LOCALIZATION))
        {
            Write(packetReceived);
        }
    }

    public void Write(string packetReceived)
    {
        var playerLocalization = DeserializePacket.Deserialize<PlayerLocalizationPacket>(packetReceived);

        var playerLocalizationResponsePacket = new PlayerLocalizationResponsePacket()
        {
            opcode = (int)OpcodePackets.PLAYER_LOCALIZATION_RESPONSE,
            x = playerLocalization.x,
            y = playerLocalization.y,
            z = playerLocalization.z
        };

        new BroadcastingPacket(connection, PlayerList.GetInstance().GetList())
            .Send(playerLocalizationResponsePacket);
    }
}