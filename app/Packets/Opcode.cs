namespace app.Packets;

public class Opcode
{
    private String packet;

    public Opcode(String packet)
    {
        this.packet = packet;
    }

    public String GetOpcode()
    {
        return packet.Substring(0, packet.Length);
    }
}