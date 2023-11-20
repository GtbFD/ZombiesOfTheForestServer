namespace app;

public class PacketManager
{
    private List<IPacket> packets;

    public PacketManager(List<IPacket> packets)
    {
        this.packets = packets;
    }

    public void Manager(string packetReceived)
    {
        foreach (var packet in packets)
        {
            packet.Handler(packetReceived);
        }
    }
}