using app.interfaces;

namespace app.packets.response;

public class DisconnectPlayerResponsePacket : IPacket
{
    public int opcode { get; set; }
}