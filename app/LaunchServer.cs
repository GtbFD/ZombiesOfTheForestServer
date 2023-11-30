using System.Text;
using app.server;
using app.utils.io;

class LaunchServer
{
    public static void Main(String[] args)
    {
        var serverApp = new ServerConfiguration();
        serverApp.Start();
        
        /*var builderPacket = new WritePacket();
        builderPacket.Write(1);
        builderPacket.Write(100);
        builderPacket.Write("GUGA");
        builderPacket.Write(float.Pi);
        var packet = builderPacket.BuildPacket();
        Console.WriteLine("Length of packet > " + packet.Length);
    
        var reader = new ReadPacket(packet);
        Console.WriteLine(reader.ReadInt());
        Console.WriteLine(reader.ReadInt());
        Console.WriteLine(reader.ReadString(4));
        Console.WriteLine(reader.ReadFloat());*/

    }

}
