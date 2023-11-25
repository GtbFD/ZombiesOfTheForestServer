using System.Net.Sockets;
using app.interfaces;
using app.packets;
using app.packets.enums;
using app.packets.request;
using app.packets.response;
using app.utils;
using app.utils.identifier;
using app.utils.io;

namespace app.handlers;

public class LoginPlayerHandler : IPacketHandler
{

    private Socket connection;

    public LoginPlayerHandler(Socket connection)
    {
        this.connection = connection;
    }
    
    public void Read(string packetReceived)
    {
        var opcode = PacketIdentifier.Opcode(packetReceived);

        if (opcode == (int)OpcodePackets.LOGIN_PLAYER)
        {
            var loginPlayerPacket = DeserializePacket.Deserialize<LoginPlayerPacket>(packetReceived);
            Write();
        }
    }

    public void Write()
    {
        var loginResponse = new LoginResponsePacket()
        {
            opcode = OpcodePackets.LOGIN_PLAYER_RESPONSE_SUCCESS,
            code = 0
        };

        new IndividualPacket(connection).Send(loginResponse);
    }

    public void Handler(string packetReceived)
    {
        Read(packetReceived);
    }
}