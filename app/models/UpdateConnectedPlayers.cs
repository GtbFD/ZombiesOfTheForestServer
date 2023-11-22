using app.interfaces;

namespace app;

public class UpdateConnectedPlayers : IPacket
{
    public int opcode { get; set; }
    public int quantity { get; set; }
}