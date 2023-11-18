using System;
using System.Text;

abstract class Packet : IPacket
{
    private byte[] _BUFFER_LENGTH = new byte[1024];
    private int PacketBytesReceived;

    public abstract void Read(String PacketReceived);

    public abstract void Write();

    public abstract void PrintReceivedMessage();

    public abstract void PrintSendedMessage();
}