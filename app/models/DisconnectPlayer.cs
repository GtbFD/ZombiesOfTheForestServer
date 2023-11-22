using app.interfaces;

namespace app;

public class DisconnectPlayer : IPacket
{
    public int opcode { get; set; }
    
}