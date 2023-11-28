using System.Text;
using app.server;
using app.utils.io;

class LaunchServer
{
    public static void Main(String[] args)
    {
        /*var serverApp = new ServerConfiguration();
        serverApp.Start();*/

        var packet = new byte[]{0x30, 0x31, 0x32, 0x38};
        //var packetBytes = Encoding.ASCII.GetBytes(packet);

        var reader = new ReadPacket(packet);
        var packetStart = reader.ReadS(2);
        var opcode = reader.ReadI(2);
        
        Console.Write("STARTER > " + packetStart + " OPCODE > " + opcode);
    }

}
