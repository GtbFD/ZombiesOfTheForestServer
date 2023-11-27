using System;
using app.interfaces;

public interface IPacketHandler
{
    public void Read(string packetReceived);
    public void Write(string packetReceived);
    public void Handler(string packetReceived);
    
}