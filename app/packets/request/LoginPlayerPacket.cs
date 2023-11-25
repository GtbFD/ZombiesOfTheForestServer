using app.interfaces;

namespace app.packets.request;

public class LoginPlayerPacket : IPacket
{
    public int opcode { get; set; }
    public string user { get; set; }
    public string password { get; set; }
}