namespace app.Packets;

public class Opcode
{
    private string packet;

    public Opcode(string packet)
    {
        this.packet = packet;
    }

    public string GetOpcode()
    {
        return packet.Substring(0, packet.Length);
    }
}