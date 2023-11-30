namespace app.utils.io;

public class PacketManager
{
    private List<IPacketHandler> packets;

    public PacketManager(List<IPacketHandler> packets)
    {
        this.packets = packets;
    }

    public void Manager(byte[] packetReceived)
    {
        /*var reader = new ReadPacket(packetReceived);
        Console.WriteLine("[@] <- PACKET_RECEIVED - ID: " + reader.ReadInt());*/
        foreach (var packet in packets)
        {
            packet.Handler(packetReceived);
        }
    }
}