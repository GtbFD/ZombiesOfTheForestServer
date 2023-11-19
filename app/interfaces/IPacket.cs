using System;

public interface IPacket
{
    public void Read(String packetReceived);
    public void Write();
    public void Handler(String packetReceived);
}