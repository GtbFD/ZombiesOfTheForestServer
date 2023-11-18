using System;

interface IPacket
{
    public void Read(String PacketReceived);
    public void Write();
}