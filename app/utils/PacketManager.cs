using app.interfaces;

namespace app;

public class PacketManager
{
    private List<IPacketHandler> packets;

    public PacketManager(List<IPacketHandler> packets)
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