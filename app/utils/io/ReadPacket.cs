using System.Globalization;
using System.Net.Sockets;
using System.Numerics;
using System.Text;

namespace app.utils.io;

public class ReadPacket
{
    private byte[] packet;
    public int offset;

    public ReadPacket(byte[] packet)
    {
        offset = 0;
        this.packet = packet;
    }
    
    public string ReadS(int length)
    {
        length -= 1;
        var bytes = new List<byte>();
        var data = "";
        while (offset <= (offset + length))
        {
            bytes.Add(packet[offset]);
            length--;
            offset++;
        }
        
        data = Encoding.ASCII.GetString(bytes.ToArray());
        
        return data;
    }

    public int ReadI(int length)
    {
        length -= 1;
        var hex = "";

        while (offset <= (offset + length))
        {
            hex += (char) packet[offset];//(int) Char.GetNumericValue((char)packet[offset]);
            length--;
            offset++;
        }
        var number = Convert.ToInt32(hex);
        //Console.WriteLine(number.ToString("X"));
        return number; //(int)decimal.Parse(hex, NumberStyles.HexNumber);
    }
}