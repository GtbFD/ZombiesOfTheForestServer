using System;
using System.Text;

public abstract class Packet : IPacket
{
    private byte[] _BUFFER_LENGTH = new byte[1024];
    private int PacketBytesReceived;

    public abstract void Read(String PacketReceived);

    public abstract void Write();

    public abstract void Handler(String packetReceived);

    public abstract void PrintReceivedMessage();

    public abstract void PrintSendedMessage();
    
}