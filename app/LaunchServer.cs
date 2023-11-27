using System.Text;
using app.server;
using app.utils.io;

class LaunchServer
{
    public static void Main(String[] args)
    {
        /*var serverApp = new ServerConfiguration();
        serverApp.Start();*/

        var packet = "GUGA21";
        var packetEncoded = Encoding.ASCII.GetBytes(packet);
        var hexPacket = Convert.ToHexString(packetEncoded);
        Console.WriteLine(hexPacket);

        var readPacket = new ReadPacket(packetEncoded);
        Console.WriteLine(readPacket.ReadS(4));
    }

}
