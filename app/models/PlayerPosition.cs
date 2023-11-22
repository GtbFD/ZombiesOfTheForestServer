using System;
using app.interfaces;

public class PlayerPosition : IPacket
{
    public int opcode { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}