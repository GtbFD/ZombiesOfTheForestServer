using app.interfaces;
using app.packets.enums;

namespace app.packets.response;

public class LoginResponsePacket : IPacket
{
    public OpcodePackets opcode { get; set; }
    public int code { get; set; }
}