using System;
using app.interfaces;

public interface IPacketHandler
{
    public void Read(String packetReceived);
    public void Write();
    public void Handler(string packetReceived);
    
}