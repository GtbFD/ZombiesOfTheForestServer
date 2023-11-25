using app.interfaces;

namespace app.packets.request;

public class DisconnectPlayerPacket : IPacket
{
    public int opcode { get; set; }
    
}